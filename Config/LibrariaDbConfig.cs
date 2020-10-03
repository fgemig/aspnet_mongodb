namespace aspnet_mongodb.Config
{
    public class LibrariaDbConfig : ILibrariaDbConfig
    {
        public string BooksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ILibrariaDbConfig
    {
        string BooksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
