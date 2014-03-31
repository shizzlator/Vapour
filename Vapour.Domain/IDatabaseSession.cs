using System.Configuration;
using MongoDB.Driver;

namespace Vapour.Domain
{
    public interface IDatabaseSession
    {
        MongoCollection GetCollection<T>(string collectionName);
    }

    public class DatabaseSession : IDatabaseSession
    {
        private static MongoClient _mongoClient;

        public DatabaseSession(string connectionString)
        {
            _mongoClient = new MongoClient(connectionString);
        }

        public MongoCollection GetCollection<T>(string collectionName)
        {
            var server = _mongoClient.GetServer();
            var database = server.GetDatabase(ConfigurationManager.AppSettings["DatabaseName"]);

            return database.GetCollection<T>(collectionName);
        }
    }
}