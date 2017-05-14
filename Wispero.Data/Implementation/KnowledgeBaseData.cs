using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wispero.Entities;

namespace Wispero.Data
{
    public class KnowledgeBaseData : Core.Services.IDataService<KnowledgeBaseItem>, Core.Services.IQueryService<KnowledgeBaseItem>
    {
        WisperoDbContext _context;

        #region Constructors
        public KnowledgeBaseData(WisperoDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods
        public void Add(KnowledgeBaseItem entity)
        {
            _context.KnowledgeBaseItems.Add(entity);

        }

        public void CommitChanges()
        {
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.KnowledgeBaseItems.Remove(_context.KnowledgeBaseItems.Where(x => x.Id == id).FirstOrDefault());
        }

        public void Edit(KnowledgeBaseItem entity)
        {
            //This need to handle concurrency. As long as rowversions are the same then persist changes.
            var original = this.Get(entity.Id);

            if (original != null)
            {
                original.Answer = entity.Answer;
                original.LastUpdateOn = entity.LastUpdateOn;
                original.Query = entity.Query;
                original.Tags = entity.Tags;
            }
            
            _context.Entry<KnowledgeBaseItem>(original).OriginalValues["RowVersion"] = entity.RowVersion;

        }

        public KnowledgeBaseItem Get(int id)
        {
            return _context.KnowledgeBaseItems.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<KnowledgeBaseItem> GetAll()
        {
            return _context.KnowledgeBaseItems.ToList();
        }

        public List<KnowledgeBaseItem> GetByFilter(Expression<Func<KnowledgeBaseItem, bool>> expression)
        {
            return _context.KnowledgeBaseItems.Where(expression).ToList();
        }

        #endregion
    }
}
