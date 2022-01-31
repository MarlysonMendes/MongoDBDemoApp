using MongoDB.Driver;
using MongoDBDemo;

string connectionString = "mongodb://127.0.0.1:27017";
string databaseName = "simple_db";
string collectionName = "people";

var client = new MongoClient(connectionString);
var db = client.GetDatabase(databaseName);
var collection = db.GetCollection<PersonModel>(collectionName);

var person = new PersonModel { FirstName = "Matheus", LastName = "Mendes" };

await collection.InsertOneAsync(person);

var results = await collection.FindAsync(_=>true);

foreach (var result in results.ToList())
{
    Console.WriteLine($"{result.Id}: {result.FirstName} {result.LastName}");
}