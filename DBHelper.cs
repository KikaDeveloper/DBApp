using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Helper{
    public class Driver{

        private MongoClient Client { get; set; }
        private string CollectionName { get; set; }

        public Driver(string connection_string, string collection_name){
            Client = new MongoClient(connection_string);
            CollectionName = collection_name;
        }

        public async Task<List<Item>> GetAllDocuments(){
            var lib = Client.GetDatabase("library");
            var collection = lib.GetCollection<BsonDocument>(CollectionName); 
            var filter = new BsonDocument();
            var data = await collection.Find<BsonDocument>(filter).ToListAsync();
            return DeserializeList<Item>(data);
        }

        public async Task AddDocument(Item doc){
            var lib = Client.GetDatabase("library");
            var collection = lib.GetCollection<BsonDocument>(CollectionName);
            var filter = new BsonDocument();
            var temp = new BsonDocument(){
                {"Name", doc.Name},
                {"Url", doc.Url},
                {"Description", doc.Description}
            };
            await collection.InsertOneAsync(temp);
        }

        private List<T> DeserializeList<T>(List<BsonDocument> data){
            var list = new List<T>();
            foreach(var item in data){
                list.Add(BsonSerializer.Deserialize<T>(item));
            }
            return list;
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