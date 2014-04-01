using MongoDB.Driver;

namespace Vapour.Domain
{
    public interface IDatabaseSession
    {
        MongoCollection GetCollection<T>(string collectionName);
    }

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


        public MongoCollection GetCollection<T>(string collectionName)
        {
            var server = _mongoClient.GetServer();
            var database = server.GetDatabase(_config.DatabaseName);

            return database.GetCollection<T>(collectionName);
        }
    }
}