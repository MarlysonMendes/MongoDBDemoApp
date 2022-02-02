using MongoDB.Driver;
using MongoDBDemo;
using MongoDataAccess.DataAccess;
using MongoDataAccess.Models;

ChoreDataAccess  db = new ChoreDataAccess();

await db.CreateUser(new UserModel() { FirstName = "Marco", LastName = "Botao"});

var users = await db.GetAllUsers();

var chore = new ChoreModel() { AssignedTo = users.First(), ChoreText = "Mow the Lawn", FrequencyInDays=7};
await db.CreateChore(chore);