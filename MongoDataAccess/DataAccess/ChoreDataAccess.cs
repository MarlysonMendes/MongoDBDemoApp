using MongoDataAccess.Models;
using MongoDB.Driver;
namespace MongoDataAccess.DataAccess
{
    public class ChoreDataAccess
    {
        private const string ConnectionString = "mongodb://127.0.0.1:27017";
        private const string databaseName = "choredb";
        private const string ChoreCollection = "chore_chart";
        private const string UserCollecion = "users";
        private const string ChoreHistoryCollection = "chore_history";

        private IMongoCollection<T> ConnectionToMongo<T>(string collection)
        {
            var client = new MongoClient ();
            var db = client.GetDatabase (databaseName);
            return db.GetCollection<T>(collection);
        }
    
    }
}
