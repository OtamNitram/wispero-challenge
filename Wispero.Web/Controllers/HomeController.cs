using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wispero.Data;
using Wispero.Entities;
using Wispero.Web.Helpers;
using Wispero.Web.Models;

namespace Wispero.Web.Controllers
{
    [NoCache]
    public class HomeController : Controller
    {
        Core.Services.IDataService<KnowledgeBaseItem> KnowledgeBaseData;
        Core.Services.IQueryService<KnowledgeBaseItem> KnowledgeBaseQuery;

        public HomeController(Core.Services.IDataService<KnowledgeBaseItem> dataService, Core.Services.IQueryService<KnowledgeBaseItem> queryService)
        {
            KnowledgeBaseData = dataService;
            KnowledgeBaseQuery = queryService;

            AutoMapper.Mapper.Initialize(cfg =>
             {
                 cfg.CreateMap<KnowledgeBaseItem, QuestionAndAnswerItemModel>()
                     .ForMember(kbi => kbi.LastUpdateOn, opt => opt.MapFrom(o => DateTime.Now.ToShortDateString()))
                     .BeforeMap((s, d) => d.Tags.ToLower())
                 ;
             });

        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Entry()
        {
            QuestionAndAnswerModel model = new QuestionAndAnswerModel();
            return PartialView("~/Shared/Entry", model);
        }

        [ChildActionOnly]
        [HttpGet]
        public ActionResult TagCloud()
        {
            //You need to call TagHelper.Process as shown below.
            int tagMaxCount;
            var tagCloud = TagHelper.Process(KnowledgeBaseQuery, out tagMaxCount);

            List<TagModel> _tags = new List<TagModel>();

            foreach (TagItem item in tagCloud)
            {
                _tags.Add(new TagModel { Count = item.Count, Tag = item.Tag });
            }

            TagCloudViewModel model = new TagCloudViewModel()
            {
                Tags = _tags,
                MaxCount = tagMaxCount
            };
            return PartialView("~/Shared/TagCloud", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(QuestionAndAnswerModel model)
        {
            //If model is valid then persists the new entry on DB. Make sure  data changes are committed.

            if (ModelState.IsValid)
            {
                KnowledgeBaseData.Add(new KnowledgeBaseItem
                {
                    Answer = model.Answer,
                    Tags = model.Tags
                });
                model.Answer = string.Empty;
                model.Question = string.Empty;
                model.Tags = string.Empty;
                KnowledgeBaseData.CommitChanges();
            }

            return PartialView("~/Shared/Entry", model as QuestionAndAnswerModel);
        }
    }
}