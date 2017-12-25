using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Pixstock.Base.AppIf.Sdk;
using Pixstock.Base.Infra;
using Pixstock.Nc.Srv.Common.Exception;
using Pixstock.Nc.Srv.Ext;
using Pixstock.Nc.Srv.Infra.Model;
using Pixstock.Nc.Srv.Infra.Repository;
using Pixstock.Nc.Srv.Model;
using Pixstock.Nc.Srv.Models;

namespace Pixstock.Nc.Srv.Controllers
{
    [Route("aapi/[controller]")]
    public class CategoryController : Controller
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        ICategoryRepository categoryRepository;

        IContentRepository contentRepository;

        ExtentionManager extentionManager;

        public CategoryController(ExtentionManager extentionManager, ICategoryRepository categoryRepository, IContentRepository contentRepository)
        {
            this.categoryRepository = categoryRepository;
            this.extentionManager = extentionManager;
            this.contentRepository = contentRepository;
        }

        // GET api/category
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}


        /// <summary>
        /// カテゴリ情報取得
        /// </summary>
        /// <param name="id"></param>
        /// <param name="param"></param>
        /// <remarks>
        /// GET api/category/5
        /// </remarks>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ResponseAapi<ICategory> GetCategory(int id, [FromQuery]CategoryParam param)
        {
            var response = new ResponseAapi<ICategory>();
            var category = this.categoryRepository.Load(id);
            if (category != null)
            {
                response.Value = category;

                // "la"
                if (param.lla_order == CategoryParam.LLA_ORDER_NAME_ASC)
                {
                    response.Link.Add("la", category.GetContentList().OrderBy(prop => prop.Name).Select(prop => prop.Id).ToArray());
                }
                else if (param.lla_order == CategoryParam.LLA_ORDER_NAME_DESC)
                {
                    response.Link.Add("la", category.GetContentList().OrderByDescending(prop => prop.Name).Select(prop => prop.Id).ToArray());
                }
                else
                {
                    response.Link.Add("la", category.GetContentList().Select(prop => prop.Id).ToArray());
                }

                // "cc"
                var ccQuery = this.categoryRepository.FindChildren(category);
                response.Link.Add("cc", ccQuery.Select(prop => prop.Id).ToArray());
            }
            else
            {
                throw new InterfaceOperationException("カテゴリが見つかりません");
            }

            try
            {
                // 拡張機能の呼び出し
                this.extentionManager.Execute(ExtentionCutpointType.API_GET_CATEGORY, category);
            }
            catch (Exception expr)
            {
                _logger.Error(expr.Message);
                throw new InterfaceOperationException();
            }

            return response;
        }

        // 

        /// <summary>
        /// カテゴリ情報リンク取得
        /// </summary>
        /// <param name="id"></param>
        /// <param name="link_type"></param>
        /// <remarks>
        /// GET api/category/id/LINK_TYPE
        /// </remarks>
        /// <returns></returns>
        [HttpGet("{id}/{link_type}")]
        public ResponseAapi<ICollection<ICategory>> GetCategoryLink(int id, string link_type)
        {
            _logger.Info("REQUEST - {0}/{1}", id, link_type);

            var categoryList = new List<ICategory>();

            var response = new ResponseAapi<ICollection<ICategory>>();

            if (link_type == "pc")
            {
                var category = this.categoryRepository.Load(id);
                var parentCategory = this.categoryRepository.Load(category.GetParentCategory().Id);
                if (parentCategory != null) categoryList.Add(parentCategory);
            }
            else if (link_type == "cc")
            {
                var category = this.categoryRepository.Load(id);
                categoryList.AddRange(
                    this.categoryRepository.FindChildren(category).Take(1000000)
                );
            }

            response.Value = categoryList;
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        ///    GET api/category/{id}/albc/{link_id}
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="link_id"></param>
        /// <returns></returns>
        [HttpGet("{id}/albc/{link_id}")]
        public ResponseAapi<Category> GetCategoryLink_albc(int id, int link_id)
        {
            _logger.Info("REQUEST - {0}/albc/{1}", id, link_id);

            var response = new ResponseAapi<Category>();
            response.Value = new Category { Id = link_id, Name = "リンクカテゴリ " + link_id };
            return response;
        }

        /// <summary>
        /// カテゴリ情報とリンクしているアーティファクト情報を取得
        /// </summary>
        /// <remarks>
        ///    GET api/category/{id}/la/{link_id}
        ///    
        ///    コンテント情報取得と同じ情報量を返します。
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="la_id"></param>
        /// <returns></returns>
        [HttpGet("{id}/la/{la_id}")]
        public ResponseAapi<IContent> GetCategoryLink_la(int id, int la_id)
        {
            _logger.Info("REQUEST - {0}/la/{1}", id, la_id);

            var response = new ResponseAapi<IContent>();

            var content = contentRepository.Load(la_id);
            if (content != null)
            {
                if (content.GetCategory().Id != id)
                    throw new InterfaceOperationException();
                response.Value = content;
            }
            else
            {
                throw new InterfaceOperationException();
            }

            return response;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
