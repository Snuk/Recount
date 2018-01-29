using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Recount.Core.Functions;
using Recount.Core.Lexemes;
using Recount.DataAccess.Options;

namespace Recount.DataAccess.Providers
{
    public class MongoFunctionsProvider : IFunctionsProvider
    {
        private const string CollectionName = "functions";
        private readonly IMongoCollection<Function> _functionsCollection;

        public MongoFunctionsProvider(IOptions<MongoOptions> mongoOptions)
        {
            _functionsCollection = new MongoClient(new MongoUrl(mongoOptions.Value.ConnectionString))
                .GetDatabase(mongoOptions.Value.DatabaseName).GetCollection<Function>(CollectionName);
        }

        public void Add(Function function)
        {
            _functionsCollection.ReplaceOne(f => f.Name == function.Name, function, new UpdateOptions { IsUpsert = true });
        }

        public Function Get(string name)
        {
            return _functionsCollection.Find(f => f.Name == name).Project<Function>(Builders<Function>.Projection.Exclude("_id"))
                .First();
        }

        public List<Function> GetAll()
        {
            return _functionsCollection.Find(FilterDefinition<Function>.Empty)
                .Project<Function>(Builders<Function>.Projection.Exclude("_id")).ToList();
        }

        public void Delete(string name)
        {
            _functionsCollection.DeleteOneAsync(f => f.Name == name);
        }
    }
}
