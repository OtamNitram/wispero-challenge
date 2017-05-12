using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wispero.Data
{
    public static class TagHelper
    {
        public static List<Entities.TagItem> Process(Core.Services.IQueryService<Entities.KnowledgeBaseItem> knowledgeService, out int tagMaxCount)
        {
            var sourceItems = knowledgeService.GetAll();
            return Process(sourceItems, out tagMaxCount);
        }

        public static List<Entities.TagItem> Process(List<Entities.KnowledgeBaseItem> items, out int tagMaxCount)
        {
            //TODO: Based on the list of items you need to rank tags dynamically. Also, return the max value that will be use for Tag Could control.
            List<Entities.TagItem> result = new List<Entities.TagItem>();
            foreach (Entities.KnowledgeBaseItem item in items)
            {
                List<String> tags = item.Tags.Split(',').ToList();
                foreach (string t in tags)
                {
                    if (result.Where(x => x.Tag == t).FirstOrDefault() != null)
                    {
                        result.Where(x => x.Tag == t).FirstOrDefault().Count++;
                    }
                    else
                    {
                        result.Add(new Entities.TagItem
                        {
                            Tag = t,
                            Count = 1
                        });
                    }
                }
            }

            tagMaxCount = result.OrderBy(x => x.Count).First().Count;

            return result;
        }
    }
}
