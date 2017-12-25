using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Pixstock.Nc.Srv.Infra.Model;
using Pixstock.Nc.Srv.Infra.Repository;
using Pixstock.Nc.Srv.Model;

namespace Pixstock.Nc.Srv.Controllers
{
    /// <summary>
    /// サムネイル操作関連APIコントローラ
    /// </summary>
    [Route("aapi/[controller]")]
    public class ThumbnailController : Controller
    {
        IThumbnailRepository thumbnailRepository;

        public ThumbnailController(IThumbnailRepository thumbnailRepository)
        {
            this.thumbnailRepository = thumbnailRepository;
        }

        /// <summary>
        /// サムネイル画像データを取得する
        /// </summary>
        /// <param name="thumbnailKey">サムネイルハッシュ、またはサムネイル情報キー</param>
        /// <returns>サムネイル画像のバイナリデータ</returns>
        [HttpGet("{thumbnailKey}/thumb")]
        public IActionResult FetchThumbnail(string thumbnailKey)
        {
            long thumbnailId = 0L;
            IThumbnail thumbnail = null;
            if (long.TryParse(thumbnailKey, out thumbnailId))
            {
                thumbnail = thumbnailRepository.Load(thumbnailId);
            }
            else
            {
                thumbnail = thumbnailRepository.FindByKey(thumbnailKey).FirstOrDefault();
            }
            if (thumbnail == null) throw new ApplicationException(string.Format("サムネイル画像({0})が見つかりません", thumbnailKey));

            // リソースの有効期限等を決定する
            //DateTimeOffset now = DateTime.Now;
            //var etag = new EntityTagHeaderValue("\"" + Guid.NewGuid().ToString() + "\"");

            return new FileContentResult(thumbnail.BitmapBytes, thumbnail.MimeType);
        }
    }
}