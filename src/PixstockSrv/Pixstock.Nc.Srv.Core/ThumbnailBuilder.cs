using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Katalib.Nc.Standard.String;
using NLog;
using Pixstock.Base.Infra;
using Pixstock.Base.Infra.Attributes;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Infra.Core;
using Pixstock.Nc.Srv.Infra.Repository;
using SixLabors.ImageSharp;

namespace Pixstock.Nc.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ThumbnailBuilder : IThumbnailBuilder
    {
        private static NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        IThumbnailRepository thumbnailRepository;

        public ThumbnailBuilder(IThumbnailRepository thumbnailRepository)
        {
            this.thumbnailRepository = thumbnailRepository;
        }

        private byte[] CreateImage(byte[] rawImage, int decodePixelWidth, int decodePixelHeight)
        {
            using (Image<Rgba32> image = Image.Load(rawImage))
            {
                image.Mutate(x => x.Resize(decodePixelWidth, decodePixelHeight));
                return image.SavePixelData();
            }
        }

        /// <summary>
        /// 任意のファイルのデータをバイナリ列で取得する
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        byte[] LoadImageBytes(string filePath)
        {
            // チェックはしていないが、画像ファイル限定。
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] imageBytes = br.ReadBytes((int)fs.Length);
                return imageBytes;
            }
        }

        /// <summary>
        /// サムネイル作成
        /// </summary>
        /// <param name="thumbnailhash">既存のサムネイルを、baseImageFilePathで生成しなおしたい場合、
        /// 既存のサムネイル情報を示すキーを指定します。それ以外は、NULLを指定します。</param>
        /// <param name="baseImageFilePath">サムネイル生成元の画像ファイルパス</param>
        /// <returns></returns>
        public string BuildThumbnail(string thumbnailhash, string baseImageFilePath)
        {
            _logger.Debug("サムネイルの作成を開始します");

            string _ThumbnailKey = null;
            Byte[] imageByte = null;

            imageByte = LoadImageBytes(baseImageFilePath);

            if (imageByte == null) throw new ApplicationException(); // 画像ファイルを取得できなかった。

            //　サムネイル種類別にすべてのサムネイルを生成する
            foreach (ThumbnailType thmbType in Enum.GetValues(typeof(ThumbnailType)))
            {
                ThumbnailInfoAttribute[] infos = (ThumbnailInfoAttribute[])thmbType.GetType().GetField(thmbType.ToString()).GetCustomAttributes(typeof(ThumbnailInfoAttribute), false);
                if (infos.Length > 0)
                {
                    var @attr = infos[0];

                    var resizedImage = CreateImage(imageByte, @attr.Width, @attr.Height); // 生成するサムネイル画像の大きさは「300」(TODO?)
                    using (Image<Rgba32> image = Image.Load(imageByte))
                    {
                        image.Mutate(x => x.Resize(@attr.Width, @attr.Height));

                        var invoker = new ThumbnailEncodingInvoker(thumbnailhash, image, thmbType, thumbnailRepository);
                        invoker.Do();

                        _ThumbnailKey = invoker.ThumbnailKey;
                        thumbnailhash = _ThumbnailKey;
                    }
                }
            }
            thumbnailRepository.Save();

            _logger.Debug("サムネイルの作成を完了します");
            return _ThumbnailKey;
        }

        public bool RemoveThumbnail(string thumbnailhash)
        {
            bool bResult = false;

            var thumbs = thumbnailRepository.FindByKey(thumbnailhash);

            foreach (var prop in thumbs)
            {
                thumbnailRepository.Delete(prop);
            }
            bResult = true;

            return bResult;
        }

        /// <summary>
        /// サムネイルファイルの出力をバックグラウンドスレッドで行うためのラップクラス
        /// </summary>
        class ThumbnailEncodingInvoker
        {
            private readonly Image<Rgba32> _ImageSource;

            private readonly string _rebuildThumbnailKey;

            private readonly ThumbnailType _ThumbnailType;

            private string _ThumbnailKey;

            private readonly IThumbnailRepository _thumbnailRepository;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="thumbnailKey">リビルド対象のサムネイルキー</param>
            /// <param name="imageSource">出力するビットマップ画像</param>
            public ThumbnailEncodingInvoker(string thumbnailKey,
                Image<Rgba32> imageSource,
                ThumbnailType thumbnailType,
                IThumbnailRepository repository)
            {
                _ImageSource = imageSource;
                _rebuildThumbnailKey = thumbnailKey;
                _ThumbnailType = thumbnailType;
                _thumbnailRepository = repository;
            }

            public string ThumbnailKey
            {
                get { return _ThumbnailKey; }
            }

            /// <summary>
            /// サムネイルを生成します
            /// </summary>
            public void Do()
            {
                try
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        this._ImageSource.SaveAsPng(memoryStream);
                        string mimeType = "image/png";

                        if (string.IsNullOrEmpty(_rebuildThumbnailKey))
                        {
                            string key = null;
                            while (key == null)
                            {
                                var tal = RandomAlphameric.RandomAlphanumeric(20);
                                var r = _thumbnailRepository.FindByKey(tal);
                                if (r.Count() == 0) key = tal;
                                foreach (var p in r)
                                {
                                    if (p.ThumbnailType != _ThumbnailType) key = tal;
                                }
                            }

                            var thumbnail = _thumbnailRepository.New();
                            thumbnail.ThumbnailKey = key;
                            thumbnail.ThumbnailType = _ThumbnailType;
                            thumbnail.MimeType = mimeType;
                            thumbnail.BitmapBytes = memoryStream.ToArray();

                            _ThumbnailKey = key;
                        }
                        else
                        {
                            var thumbnailQue = _thumbnailRepository.FindByKey(_rebuildThumbnailKey);

                            // サムネイルタイプのエンティティが存在する場合、trueをセットする。
                            bool isThumbnailSave = false;
                            foreach (var prop in thumbnailQue)
                            {
                                if (prop.ThumbnailType == _ThumbnailType)
                                {
                                    prop.BitmapBytes = memoryStream.ToArray();
                                    isThumbnailSave = true;
                                }
                            }

                            if (!isThumbnailSave)
                            {
                                // 指定したサムネイルタイプのエンティティを、
                                // 新規作成する。
                                var thumbnail_NewThumbnailType = _thumbnailRepository.New();
                                thumbnail_NewThumbnailType.ThumbnailKey = _rebuildThumbnailKey;
                                thumbnail_NewThumbnailType.ThumbnailType = _ThumbnailType;
                                thumbnail_NewThumbnailType.BitmapBytes = memoryStream.ToArray();
                                _ThumbnailKey = _rebuildThumbnailKey;
                            }
                            else
                            {
                                _ThumbnailKey = _rebuildThumbnailKey;
                            }
                        }
                    }
                }
                catch (NotSupportedException expr)
                {
                    _logger.Warn(expr.Message);
                }
            }
        }
    }
}