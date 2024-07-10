using MongoDB.Driver;
using e_commerce_pp.Models;

namespace e_commerce_pp.Data;
public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("MongoDbSettings:ConnectionString").Value;
        var databaseName = configuration.GetSection("MongoDbSettings:DatabaseName").Value;

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }
    public IMongoCollection<Product> Products => _database.GetCollection<Product>("TodoItems");
    // Maybe this can be implemented somewhere else, in here it might become incosistent structure
}