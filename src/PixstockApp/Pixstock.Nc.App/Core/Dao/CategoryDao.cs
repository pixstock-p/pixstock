using System;
using System.Linq;
using System.Collections.Generic;
using Pixstock.Base.AppIf.Sdk;
using Pixstock.Nc.App.Models;
using RestSharp;

namespace Pixstock.Nc.App.Core.Dao {
    public class CategoryDao {

        static string BASEURL = "http://localhost:5080/aapi";

        public Category LoadCategory(long categoryId)
        {
            Console.WriteLine("[CategoryDao][LoadCategory] : IN");

            string requestUrl = BASEURL;
            var client = new RestClient(requestUrl);
            var request = new RestRequest("category/{id}", Method.GET);
            request.AddUrlSegment("id", categoryId);
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
                request_link_la.AddUrlSegment("id", categoryId);
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

         public ICollection<Category> GetSubCategory(long categoryId)
        {
            Console.WriteLine("[CategoryDao][GetSubCategory] : IN");
            Console.WriteLine($"categoryId = {categoryId}");

            // バックエンドサーバにリクエストを送信する
            string requestUrl = BASEURL;
            var client = new RestClient(requestUrl);
            var request = new RestRequest("category/{id}/cc", Method.GET); // TODO: 現時点では、リンク情報(cc)一覧のみ取得する
            request.AddUrlSegment("id", categoryId);

            Console.WriteLine("リクエストを開始します = " + requestUrl);

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
    }
}