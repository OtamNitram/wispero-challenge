using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wispero.Web.Helpers;
using Wispero.Web.Models;

namespace Wispero.Web.Controllers
{
    [NoCache]
    public class QuestionController : Controller
    {
        Core.Services.IDataService<Entities.KnowledgeBaseItem> KnowledgeData;
        Core.Services.IQueryService<Entities.KnowledgeBaseItem> KnowledgeQuery;

        public QuestionController(Core.Services.IDataService<Entities.KnowledgeBaseItem> knowledgeData, Core.Services.IQueryService<Entities.KnowledgeBaseItem> knowledgeQuery)
        {
            KnowledgeData = knowledgeData;
            KnowledgeQuery = knowledgeQuery;

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<QuesitonAndAnswerEditModel, Entities.KnowledgeBaseItem>()
                    .ForMember(kbi => kbi.Query, opt => opt.MapFrom(o => o.Question))
                ;
            });

        }
        // GET: Question
        public ActionResult Edit(int id)
        {
            Entities.KnowledgeBaseItem toEdit = KnowledgeQuery.Get(id);

            if (toEdit == null)
            {
                return new RedirectResult("~/Shared/Error");
            }

            QuesitonAndAnswerEditModel model = new QuesitonAndAnswerEditModel
            {
                Answer = toEdit.Answer,
                Id = toEdit.Id,
                Question = toEdit.Query,
                RowVersion = toEdit.RowVersion,
                Tags = toEdit.Tags,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuesitonAndAnswerEditModel model)
        {
            if (ModelState.IsValid)
            {

                var entity = AutoMapper.Mapper.Map<Entities.KnowledgeBaseItem>(model);
                try
                {
                    KnowledgeData.Edit(entity);
                    return new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { action = "Index", controller = "HomeController" }));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Concurrency", "Another user might have modified the same record you are trying to make updates. Please refresh and try again.");
                }
            }
            return View(model);
        }
    }
}