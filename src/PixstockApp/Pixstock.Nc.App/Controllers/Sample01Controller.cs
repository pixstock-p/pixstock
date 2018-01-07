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
        static string BASEURL = "http://localhost:5080/aapi";

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
            string requestUrl = BASEURL + "/aapi/category/1";
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