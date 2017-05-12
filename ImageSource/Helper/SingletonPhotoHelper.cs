using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImageSource.Helper
{
    public class SingletonPhotoHelper : PhotoHelper
    {
        private static IQueryable<Photo> _localCache;
        private static object _lock = new object();

        public SingletonPhotoHelper(IRestClient restClient, ISerializer serializer)
            : base(restClient, serializer)
        {
        }

        public new IQueryable<Photo> GetAll()
        {
            if (_localCache != null)
            {
                return _localCache;
            }
            else
            {
                _localCache = base.GetAll();
                return _localCache;
            }
        }

        public new IQueryable<Photo> Match(Expression<Func<Photo, bool>> searchPattern)
        {
            var collection = this.GetAll();
            return collection.Where(searchPattern);
        }

        public new IQueryable<Photo> Match(Expression<Func<Photo, bool>> searchPattern, Expression<Func<Photo, object>> sorting)
        {
            return Match(searchPattern).OrderBy(sorting);
        }
    }
}
