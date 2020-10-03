using aspnet_mongodb.Config;
using aspnet_mongodb.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aspnet_mongodb.Services
{
    public class LivrariaService
    {
        private readonly IMongoCollection<Livro> _livros;

        public LivrariaService(ILibrariaDbConfig settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _livros = database.GetCollection<Livro>(settings.BooksCollectionName);
        }

        public async Task<IEnumerable<Livro>> Todos() =>
            await _livros.Find(livro => true).ToListAsync();

        public async Task<Livro> PorId(string id) =>
            await _livros.Find<Livro>(livro => livro.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Livro>> PorCategoria(string categoria) =>
           await _livros.Find<Livro>(livro => livro.Categoria == categoria).ToListAsync();

        public async Task<Livro> Cadastrar(Livro livro)
        {
            await _livros.InsertOneAsync(livro);
            return livro;
        }

        public async Task Atualizar(Livro livro, string id) =>
            await _livros.ReplaceOneAsync(book => book.Id == id, livro);

        public async Task Remover(string id) =>
            await _livros.DeleteOneAsync(livro => livro.Id == id);
    }
}
