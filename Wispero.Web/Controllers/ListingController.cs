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

            //LastUpdateOn field is set with DateTime.Now and Tags field with lowercase.
            //Also create a map from TagItem to TagModel.
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<KnowledgeBaseItem, QuestionAndAnswerItemModel>();
                cfg.CreateMap<TagItem, TagModel>();
            });

        }

        [HttpGet]
        public ActionResult Index(string tag = "")
        {
            //Return an instance of ListingViewModel.
            List<KnowledgeBaseItem> _knowledgeBaseItemList = new List<KnowledgeBaseItem>();

            if (!String.IsNullOrEmpty(tag))
            {
                _knowledgeBaseItemList = KnowledgeQuery.GetByFilter(x => x.Tags.Contains(tag));
            }
            else
            {
                _knowledgeBaseItemList = KnowledgeQuery.GetAll();
            }

            List<QuestionAndAnswerItemModel> _questionAndAnswerItemModelList = new List<QuestionAndAnswerItemModel>();

            foreach (KnowledgeBaseItem item in _knowledgeBaseItemList)
            {
                _questionAndAnswerItemModelList.Add(new QuestionAndAnswerItemModel
                {
                    Answer = item.Answer,
                    Id = item.Id,
                    LastUpdateOn = item.LastUpdateOn.ToShortDateString(),
                    Question = item.Query,
                    Tags = item.Tags
                });
            }

            ListingViewModel model = new ListingViewModel()
            {
                Questions = _questionAndAnswerItemModelList,
                Tag = tag
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult ExportQnAMaker(string fileName, string folder)
        {
            var file = string.IsNullOrEmpty(fileName) ? System.Guid.NewGuid().ToString() + ".txt" : fileName;
            var path = string.IsNullOrEmpty(folder) ? AppDomain.CurrentDomain.BaseDirectory + @"\Export\" : folder;

            KnowledgeQnAExport.Export(KnowledgeQuery.GetAll(), new Export.Settings.QnAMakerSetting(path, file));
            return new FilePathResult(path, "application/text");
        }
    }
}