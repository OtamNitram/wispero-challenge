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

            //TODO: Implement mapping as needed.
            throw new NotImplementedException();
            
        }
        // GET: Question
        public ActionResult Edit(int id)
        {
            //TODO: Implement this method to retrieve and present data for Edition.
            throw new NotImplementedException();
      
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
                    //TODO: Implement this part of code to persist changes into database.
                    throw new NotImplementedException();
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