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

        public async Task<List<UserModel>> GetAllUsers()
        {
            var usersCollection = ConnectionToMongo<UserModel>(UserCollecion);
            var results = await usersCollection.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<List<ChoreModel>> GetAllChores()
        {
            var choresCollection = ConnectionToMongo<ChoreModel>(ChoreCollection);
            var results = await choresCollection.FindAsync(_ => true);
            return results.ToList();
        }
        public async Task<List<ChoreModel>> GetAllChoresForUser(UserModel user)
        {
            var choresCollection = ConnectionToMongo<ChoreModel>(ChoreCollection);
            var results = await choresCollection.FindAsync(c => c.AssignedTo.Id == user.Id);
            return results.ToList();
        }
        
        public Task CreateUser (UserModel user)
        {
            var usersCollection = ConnectionToMongo<UserModel>(UserCollecion);
            return usersCollection.InsertOneAsync(user);
        }
        public Task CreateChore(ChoreModel chore)
        {
            var choresCollection = ConnectionToMongo<ChoreModel>(ChoreCollection);
            return choresCollection.InsertOneAsync(chore);
        }
        public Task UpdateChore (ChoreModel chore)
        {
            var choresCollection = ConnectionToMongo<ChoreModel>(ChoreCollection);
            var filter = Builders<ChoreModel>.Filter.Eq("Id", chore.Id);
            return choresCollection.ReplaceOneAsync(filter, chore, new ReplaceOptions { IsUpsert = true });
        }
        public Task DeleteChore(ChoreModel chore)
        {
            var choresCollection = ConnectionToMongo<ChoreModel>(ChoreCollection);
            return choresCollection.DeleteOneAsync(c =>c.Id == chore.Id);
        }
        public async Task CompleteChore(ChoreModel chore)
        {
            var choresCollection = ConnectionToMongo<ChoreModel> (ChoreCollection);
            var filter = Builders<ChoreModel>.Filter.Eq("Id", chore.Id);
            await choresCollection.ReplaceOneAsync (filter, chore);

            var choreHistoryCollection = ConnectionToMongo<ChoreHistoryModel>(ChoreHistoryCollection);
            await choreHistoryCollection.InsertOneAsync(new ChoreHistoryModel(chore));
        }

    }
}
