using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Recount.Core.Lexemes;

namespace Recount.DataAccess.Providers
{
    public class MongoVariablesProvider : IVariablesProvider
    {
        //mongodb-1-servers-vm-0:27017
        private readonly IMongoCollection<VariableEntity> _variablesCollection;

        public MongoVariablesProvider()
        {
            _variablesCollection = new MongoClient(new MongoUrl("mongodb://mongodb-1-servers-vm-0:27017")).GetDatabase("recount")
                .GetCollection<VariableEntity>("variables");
        }

        public void Add(string name, double value)
        {
            _variablesCollection.ReplaceOne(
                v => v.Name == name,
                new VariableEntity { Name = name, Value = value },
                new UpdateOptions { IsUpsert = true });
        }

        public double Get(string name)
        {
            return _variablesCollection.Find(v => v.Name == name)
                .Project<VariableEntity>(Builders<VariableEntity>.Projection.Exclude("_id")).First().Value;
        }

        public Dictionary<string, double> GetAll()
        {
            return _variablesCollection.Find(FilterDefinition<VariableEntity>.Empty)
                .Project<VariableEntity>(Builders<VariableEntity>.Projection.Exclude("_id")).ToEnumerable()
                .ToDictionary(v => v.Name, v => v.Value);
        }

        public void Delete(string name)
        {
            _variablesCollection.DeleteOne(v => v.Name == name);
        }

        private class VariableEntity
        {
            public string Name { get; set; }

            public double Value { get; set; }
        }
    }
}
