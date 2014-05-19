using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Vapour.Domain.Config;

namespace Vapour.Domain.DataAccess
{
    public class MongoDbSession
    {
        private readonly IConfig _config;
        private readonly MongoClient _mongoClient;

        public MongoDbSession() : this(new Config.Config()) { }

        public MongoDbSession(IConfig config)
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