using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NLog;
using Pixstock.Base.AppIf.Sdk;
using Pixstock.Nc.Srv.Common.Exception;
using Pixstock.Nc.Srv.Infra.Model;
using Pixstock.Nc.Srv.Infra.Repository;

namespace Pixstock.Nc.Srv.Controllers
{
    [Route("aapi/[controller]")]
    public class ArtifactController : Controller
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        IContentRepository contentRepository;

        IFileMappingInfoRepository fileMappingInfoRepository;

        public ArtifactController(IContentRepository contentRepository, IFileMappingInfoRepository fileMappingInfoRepository)
        {
            this.contentRepository = contentRepository;
            this.fileMappingInfoRepository = fileMappingInfoRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseAapi<IContent> GetContent(int id)
        {
            var response = new ResponseAapi<IContent>();
            var content = contentRepository.Load(id);
            if (content != null)
            {
                response.Value = content;
            }
            else
            {
                throw new InterfaceOperationException();
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data_id"></param>
        /// <returns></returns>
        [HttpGet("{id}/data/{data_id}")]
        public IActionResult GetContent_Data(int id, int data_id)
        {
            _logger.Debug("開始");
            var content = contentRepository.Load(id);
            if (content == null) throw new InterfaceOperationException("コンテント情報が見つかりません");

            var fmi = content.GetFileMappingInfo();
            if (fmi == null) throw new InterfaceOperationException("ファイルマッピング情報が見つかりません1");

            var efmi = fileMappingInfoRepository.Load(fmi.Id);
            if (efmi == null) throw new InterfaceOperationException("ファイルマッピング情報が見つかりません2");

            // NOTE: リソースの有効期限等を決定する
            DateTimeOffset now = DateTime.Now;
            var etag = new EntityTagHeaderValue("\"" + Guid.NewGuid().ToString() + "\"");
            string filePath = Path.Combine(efmi.GetWorkspace().PhysicalPath, efmi.MappingFilePath);
            var file = PhysicalFile(
                Path.Combine(efmi.GetWorkspace().PhysicalPath, efmi.MappingFilePath)
                , efmi.Mimetype, now, etag);

            _logger.Debug("終了");
            return file;
        }
    }
}