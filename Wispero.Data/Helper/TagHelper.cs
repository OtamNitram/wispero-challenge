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
            List<Entities.TagItem> result = new List<Entities.TagItem>();
            foreach (Entities.KnowledgeBaseItem item in items)
            {
                List<String> tags = item.Tags.Split(',').ToList();
                foreach (string t in tags)
                {
                    if (result.Where(x => x.Tag.Contains(t)).FirstOrDefault() != null)
                    {
                        result.Where(x => x.Tag.Contains(t)).FirstOrDefault().Count++;
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

            if (result.Count > 0)
            {
                tagMaxCount = result.OrderByDescending(x => x.Count).First().Count;
            }
            else
            {
                tagMaxCount = 0;
            }

            return result;
        }
    }
}
