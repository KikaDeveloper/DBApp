using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Helper{
    public class Driver{

        private MongoClient _client { get; set; }
        private string _collectionName { get; set; }

        public Driver(string connectionString, string collectionName)
        {
            _client = new MongoClient(connectionString);
            _collectionName = collectionName;
        }

        public async Task<List<Item>> GetAllDocuments()
        {
            IMongoCollection<BsonDocument> collection = GetCollection();
            var filter = new BsonDocument();
            var list = await collection.Find<BsonDocument>(filter).ToListAsync();
            return DeserializeList<Item>(list);
        }

        public async Task AddDocument(Item document)
        {
            IMongoCollection<BsonDocument> collection = GetCollection();
            var newDocument = new BsonDocument(){
                {"Name", document.Name},
                {"Url", document.Url},
                {"Description", document.Description}
            };
            await collection.InsertOneAsync(newDocument);
        }

        private IMongoCollection<BsonDocument> GetCollection()
        {
            IMongoDatabase library = _client.GetDatabase("library");
            IMongoCollection<BsonDocument> collection = library.GetCollection<BsonDocument>(_collectionName);
            return collection; 
        }

        private List<T> DeserializeList<T>(List<BsonDocument> list)
        {
            var resultList = new List<T>();

            foreach(var item in list){
                resultList.Add(BsonSerializer.Deserialize<T>(item));
            }

            return resultList;
        }
    }

    public class Item{
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

        public Item(string name, string url, string description){
            Name = name;
            Url = url;
            Description = description; 
        }
    }

}