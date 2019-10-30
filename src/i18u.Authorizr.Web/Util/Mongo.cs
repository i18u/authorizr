using System;
using i18u.Repositories.Mongo;
using i18u.Repositories.Mongo.Interop;

namespace i18u.Authorizr.Web.Util
{
    internal static class Mongo
    {
        public static IMongoClient GetClient()
        {
            var host = Environment.GetEnvironmentVariable("MONGO_HOST");
            var port = Environment.GetEnvironmentVariable("MONGO_PORT");
            var user = Environment.GetEnvironmentVariable("MONGO_USER");
            var pass = Environment.GetEnvironmentVariable("MONGO_PASS");

            var mongoClient = new MongoClient(host, int.Parse(port), user, pass);
            return mongoClient;
        }

        public static IMongoRepository<T> GetRepository<T>(string database, string collection) where T : IMongoModel
        {
            var client = GetClient();
            var repository = new MongoRepository<T>(client, database, collection);

            return repository;
        }
    }
}