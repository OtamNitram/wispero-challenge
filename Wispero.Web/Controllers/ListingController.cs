using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wispero.Entities;
using Wispero.Web.Helpers;
using Wispero.Web.Models;

namespace Wispero.Web.Controllers
{
    [NoCache]
    public class ListingController : Controller
    {
        Core.Services.IQueryService<KnowledgeBaseItem> KnowledgeQuery;
        Core.Services.IExportService<Wispero.Export.Settings.QnAMakerSetting> KnowledgeQnAExport;

        public ListingController(Core.Services.IQueryService<KnowledgeBaseItem> queryService, Core.Services.IExportService<Wispero.Export.Settings.QnAMakerSetting> exportService)
        {
            KnowledgeQuery = queryService;
            KnowledgeQnAExport = exportService;

            //TODO: Implement mapping from Entities.KnowledgeBaseItem to QuestionAndAnswerItemModel.
            //LastUpdateOn field is set with DateTime.Now and Tags field with lowercase.
            //Also create a map from TagItem to TagModel.
            throw new NotImplementedException();
            
        }

        [HttpGet]
        public ActionResult Index(string tag = "")
        {
            //TODO: Implement the corresponding call to get all items or filtered by tag.
            //Return an instance of ListingViewModel.
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult ExportQnAMaker(string fileName, string folder)
        {
            var file = string.IsNullOrEmpty(fileName) ? System.Guid.NewGuid().ToString() + ".txt" : fileName;
            var path = string.IsNullOrEmpty(folder) ? AppDomain.CurrentDomain.BaseDirectory + @"\Export\" : folder;

            //TODO: Get all elements and then call the Export method in order to create a text file.
            //Then read and return the file content to the client.
            throw new NotImplementedException();
        }
    }
}