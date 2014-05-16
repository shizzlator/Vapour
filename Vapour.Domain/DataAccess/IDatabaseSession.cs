using MongoDB.Driver;

namespace Vapour.Domain.Interfaces
{
    public interface IDatabaseSession
    {
        MongoCollection GetCollection<T>(string collectionName);
        MongoCursor<T> RunQuery<T>(object queryObject, string collectionName);
        T Save<T>(object objectToInsert, string collectionName);
    }
}