using System.Collections.Generic;
using MongoDB.Driver;
using Recount.Core.Functions;
using Recount.Core.Lexemes;
using Recount.Core.Variables;

namespace Recount.DataAccess.Providers
{
    public class MongoFunctionsProvider : IFunctionsProvider
    {
        //mongodb-1-servers-vm-0:27017
        private readonly IMongoCollection<Function> _functionsCollection;

        public MongoFunctionsProvider()
        {
            _functionsCollection = new MongoClient(new MongoUrl("mongodb://localhost:27017")).GetDatabase("recount")
                .GetCollection<Function>("functions");
        }

        public void Add(Function function)
        {
            _functionsCollection.ReplaceOne(f => f.Name.Body == function.Name.Body, function, new UpdateOptions { IsUpsert = true });
        }

        public Function Get(Variable name)
        {
            return _functionsCollection.Find(f => f.Name.Body == name.Body).Project<Function>(Builders<Function>.Projection.Exclude("_id"))
                .First();
        }

        public List<Function> GetAll()
        {
            return _functionsCollection.Find(FilterDefinition<Function>.Empty)
                .Project<Function>(Builders<Function>.Projection.Exclude("_id")).ToList();
        }

        public void Delete(Variable name)
        {
            _functionsCollection.DeleteOneAsync(f => f.Name.Body == name.Body);
        }
    }
}
