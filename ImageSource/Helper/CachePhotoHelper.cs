using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ImageSource.Helper
{
    public class CachePhotoHelper : PhotoHelper
    {
        private static volatile MemoryCache _localCache;
        private static object _lock = new object();

        public CachePhotoHelper(IRestClient restClient, ISerializer serializer) 
            : base(restClient, serializer)
        {
            _localCache = MemoryCache.Default;
        }

        public new IQueryable<Photo> GetAll()
        {
            IQueryable<Photo> result = base.GetAll();
            CacheItemPolicy policy = new CacheItemPolicy {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddSeconds(20))
            };
            CacheItem item = new CacheItem("cache", result);

            _localCache.AddOrGetExisting(item, policy);

            return _localCache["cache"] as IQueryable<Photo>;
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
