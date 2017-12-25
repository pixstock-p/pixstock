using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronNET.API;
using System.Net.Http;
using RestSharp;
using Pixstock.Base.AppIf.Sdk;
using Pixstock.Nc.App.Models;

namespace Pixstock.Nc.App.Controllers
{
    [Route("cli/[controller]")]
    public class Sample01Controller : Controller
    {
        /// <summary>
        /// バックエンドサーバURL
        /// </summary>
        static string BASEURL = "http://localhost:5000/aapi";

        /// <summary>
        /// サムネイル画像取得API
        /// </summary>
        static string FETCH_THUMBNAIL_URL = "thumbnail/{id}/thumb";

        [HttpGet]
        public ActionResult ShowAllAuthors()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowAuthorsByState()
        {
            return View();
        }

        /// <summary>
        /// Viewから、すべてのデータを取得する
        /// </summary>
        /// <returns></returns>
        public List<Author> GetAllAuthors()
        {
            List<Author> author = new List<Author>();
            for (int i = 0; i < 100; i++)
            {
                author.Add(new Author { Name = "Test" + i });
            }
            return author;
        }

        public void RequestCategory()
        {
            Console.WriteLine("バックエンドサーバにリクエストを送信する");

            // バックエンドサーバにリクエストを送信する
            string requestUrl = "http://localhost:5000/aapi/category/1";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(requestUrl);

            HttpResponseMessage response = client.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("{0}", dataObjects);

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// カテゴリ情報取得指示
        /// </summary>
        /// <remarks>
        /// サービスへカテゴリ情報取得APIを実行し、任意のカテゴリ情報を取得する。
        /// </remarks>
        /// <param name="category_id">取得したいカテゴリ情報キー</param>
        /// <returns>カテゴリ情報</returns>
        private Category LoadCategory(long category_id)
        {
            string requestUrl = "http://localhost:5000/aapi";
            var client = new RestClient(requestUrl);
            var request = new RestRequest("category/{id}", Method.GET);
            request.AddUrlSegment("id", category_id);
            request.AddQueryParameter("lla_order","NAME_ASC");

            var response = client.Execute<ResponseAapi<Category>>(request);
            if (!response.IsSuccessful)
            {
                Console.WriteLine("ErrorCode=" + response.StatusCode);
                Console.WriteLine("ErrorException=" + response.ErrorException);
                Console.WriteLine("ErrorMessage=" + response.ErrorMessage);
                Console.WriteLine("ContentError=" + response.Data.Error);
                return null;
            }
            var category = response.Data.Value;

            // リンク情報から、コンテント情報を取得する
            var contentList = new List<Content>();
            var link_la = response.Data.Link["la"] as List<object>;
            foreach (var content_id in link_la.Select(p => (long)p))
            {
                //Console.WriteLine("Request LinkType=la = " + category_id + ":" + content_id);
                var request_link_la = new RestRequest("category/{id}/la/{content_id}", Method.GET);
                request_link_la.AddUrlSegment("id", category_id);
                request_link_la.AddUrlSegment("content_id", content_id);

                var response_link_la = client.Execute<ResponseAapi<Content>>(request_link_la);
                if (response_link_la.IsSuccessful)
                {
                    //Console.WriteLine("Link[la]のコンテント読み込み=" + response_link_la.Data.Value);
                    contentList.Add(response_link_la.Data.Value);
                }
            }

            category.Contents = contentList;
            return category;
        }

        /// <summary>
        /// カテゴリ情報リンク取得指示(制限事項：ccのみ取得する)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet("RequestCategory2")]
        public ICollection<Category> RequestCategory2([FromQuery]CategoryParam param)
        {
            Console.WriteLine("Request Query Paramerer=" + param.CategoryId);

            // バックエンドサーバにリクエストを送信する
            string requestUrl = "http://localhost:5000/aapi";
            var client = new RestClient(requestUrl);
            var request = new RestRequest("category/{id}/cc", Method.GET); // TODO: 現時点では、リンク情報(cc)一覧のみ取得する
            request.AddUrlSegment("id", param.CategoryId);

            IRestResponse<ResponseAapi<List<Category>>> response = client.Execute<ResponseAapi<List<Category>>>(request);

            if (!response.IsSuccessful)
            {
                Console.WriteLine("ErrorCode=" + response.StatusCode);
                Console.WriteLine("ErrorException=" + response.ErrorException);
                Console.WriteLine("ErrorMessage=" + response.ErrorMessage);
                Console.WriteLine("ContentError=" + response.Data.Error);
                return null;
            }

            // リンク情報一覧から、カテゴリ情報を取得する
            List<Category> responseCategoryList = new List<Category>();
            foreach (var linked_Category in response.Data.Value)
            {
                var category = LoadCategory(linked_Category.Id);
                if (category != null)
                {
                    responseCategoryList.Add(category);
                }
            }

            return responseCategoryList;
        }

        /// <summary>
        /// 任意のカテゴリのサムネイル情報一覧を取得する
        /// </summary>
        /// <param name="param">ThumbnailListAPIのパラメータ</param>
        /// <returns>サムネイル情報コレクション</returns>
        [HttpGet("ThumbnailList")]
        public Thumbnail ThumbnailList([FromQuery]ThumbnailListParam param)
        {
            return new Thumbnail { ThumbnailSourceUri = "/cli/Sample01/ThumbnailImageFile/" + param.ThumbnailHash };
        }

        /// <summary>
        /// バックエンドからサムネイル画像のバイナリデータを取得
        /// </summary>
        /// <param name="thumbId">サムネイル情報キー</param>
        /// <returns>サムネイル画像のバイト列</returns>
        [HttpGet("ThumbnailImageFile/{thumbId}")]
        public IActionResult ThumbnailImageFile(string thumbId)
        {
            //Console.WriteLine("Execute ThumbnailImageFile  Id=" + thumbId);

            var client = new RestClient(BASEURL);
            var request = new RestRequest(FETCH_THUMBNAIL_URL, Method.GET);
            request.AddUrlSegment("id", thumbId);

            IRestResponse response = client.Execute(request);

            // note: 取得画像についての処理はここで行う

            return this.File(response.RawBytes, response.ContentType);
        }

        /// <summary>
        /// バックエンドからコンテントのバイナリデータを取得
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns>コンテントデータのバイト列</returns>
        [HttpGet("ContentImageFile/{contentId}")]
        public IActionResult ContentImageFile(string contentId)
        {
            Console.WriteLine("Execute ContentImageFile  Id=" + contentId);

            var client = new RestClient(BASEURL);
            var request = new RestRequest("artifact/{id}/data/{data_id}", Method.GET);
            request.AddUrlSegment("id", contentId);
            request.AddUrlSegment("data_id", 0);

            IRestResponse response = client.Execute(request);

            // note: 取得画像についての処理はここで行う

            return this.File(response.RawBytes, response.ContentType);
        }

        /// <summary>
        /// 
        /// </summary>
        public class CategoryParam
        {
            public long CategoryId { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class ThumbnailListParam
        {
            public string ThumbnailHash { get; set; }
        }
    }
}