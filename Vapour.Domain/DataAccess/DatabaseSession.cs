using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Vapour.Domain.Interfaces;

namespace Vapour.Domain.DataAccess
{
    public class DatabaseSession : IDatabaseSession
    {
        private readonly IConfig _config;
        private static MongoClient _mongoClient;

        public DatabaseSession() : this(new Config()) { }

        public DatabaseSession(IConfig config)
        {
            _config = config;
            _mongoClient = new MongoClient(config.ConnectionString);
        }

        public T Save<T>(object objectToInsert, string collectionName)
        {
            GetCollection<T>(collectionName).Save(objectToInsert);

            return (T) objectToInsert;
        }
        
        public MongoCollection GetCollection<T>(string collectionName)
        {
            var server = _mongoClient.GetServer();
            var database = server.GetDatabase(_config.DatabaseName);

            return database.GetCollection<T>(collectionName);
        }

        public MongoCursor<T> RunQuery<T>(object queryObject, string collectionName)
        {
            var collection = GetCollection<T>(collectionName);

            var queryDoc = new QueryDocument(queryObject.ToBsonDocument());
            return collection.FindAs<T>(queryDoc);
        }
    }
}