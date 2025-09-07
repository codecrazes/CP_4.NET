using MongoDB.Driver;
using BibliotecaMongo.Models;

namespace BibliotecaMongo.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            _database = client.GetDatabase("BibliotecaDB");
        }

        public IMongoCollection<Livro> Livros
        {
            get { return _database.GetCollection<Livro>("Livros"); }
        }
    }
}
