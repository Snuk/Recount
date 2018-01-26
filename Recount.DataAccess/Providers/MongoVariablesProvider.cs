using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Recount.Core.Lexemes;
using Recount.Core.Numbers;
using Recount.Core.Variables;

namespace Recount.DataAccess.Providers
{
    public class MongoVariablesProvider : IVariablesProvider
    {
        //mongodb-1-servers-vm-0:27017
        private readonly IMongoCollection<VariableEntity> _variablesCollection;

        public MongoVariablesProvider()
        {
            _variablesCollection = new MongoClient(new MongoUrl("mongodb://localhost:27017")).GetDatabase("recount")
                .GetCollection<VariableEntity>("variables");
        }

        public void Add(Variable name, Number value)
        {
            _variablesCollection.ReplaceOne(
                v => v.Name.Body == name.Body,
                new VariableEntity { Name = name, Value = value },
                new UpdateOptions { IsUpsert = true });
        }

        public Number Get(Variable name)
        {
            return _variablesCollection.Find(v => v.Name.Body == name.Body)
                .Project<VariableEntity>(Builders<VariableEntity>.Projection.Exclude("_id")).First().Value;
        }

        public Dictionary<Variable, Number> GetAll()
        {
            return _variablesCollection.Find(FilterDefinition<VariableEntity>.Empty)
                .Project<VariableEntity>(Builders<VariableEntity>.Projection.Exclude("_id")).ToEnumerable()
                .ToDictionary(v => v.Name, v => v.Value);
        }

        public void Delete(Variable name)
        {
            _variablesCollection.DeleteOne(v => v.Name.Body == name.Body);
        }

        private class VariableEntity
        {
            public Variable Name { get; set; }

            public Number Value { get; set; }
        }
    }
}
