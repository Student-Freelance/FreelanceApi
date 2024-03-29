﻿using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Freelance_Api.Extensions
{
    public static class MongoQueryableFullTextExtensions
    {
        public static IMongoQueryable<T> WhereText<T>(this IMongoQueryable<T> query, string search)
        {
            var filter = Builders<T>.Filter.Text(search);
            return query.Where(_ => filter.Inject());
        }
    }
}