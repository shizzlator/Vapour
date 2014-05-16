using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Vapour.Domain.Interfaces;

namespace Vapour.Domain.DataAccess
{
    public class MongoDBSession
    {
        private readonly IConfig _config;
        private readonly MongoClient _mongoClient;

        public MongoDBSession() : this(new Config()) { }

        public MongoDBSession(IConfig config)
        {
            _config = config;
            _mongoClient = new MongoClient(config.ConnectionString);
        }

        public T Save<T>(object objectToInsert)
        {
			MongoCollection<T> collection = GetCollection<T>();
			collection.Save(objectToInsert);

            return (T) objectToInsert;
        }
        
        public MongoCollection<T> GetCollection<T>()
        {
			MongoServer server = _mongoClient.GetServer();
			MongoDatabase database = server.GetDatabase(_config.DatabaseName);

	        return database.GetCollection<T>(typeof(T).Name);
        }

		public IQueryable<T> Find<T>()
        {
			return GetCollection<T>().AsQueryable<T>();
        }
    }
}