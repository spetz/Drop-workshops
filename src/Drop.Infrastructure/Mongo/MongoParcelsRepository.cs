using System;
using System.Threading.Tasks;
using Drop.Core.Entities;
using Drop.Core.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Drop.Infrastructure.Mongo
{
    internal class MongoParcelsRepository : IParcelsRepository
    {
        private readonly IMongoDatabase _database;

        public MongoParcelsRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Parcel> GetAsync(Guid id)
        {
            var parcelDocument = await Collection.AsQueryable()
                .SingleOrDefaultAsync(p => p.Id == id);
            
            return parcelDocument?.ToEntity();
        }

        public Task AddAsync(Parcel parcel) => Collection.InsertOneAsync(new ParcelDocument(parcel));

        public Task UpdateAsync(Parcel parcel)
            => Collection.ReplaceOneAsync(p => p.Id == parcel.Id, new ParcelDocument(parcel));

        private IMongoCollection<ParcelDocument> Collection
            => _database.GetCollection<ParcelDocument>("parcels");
    }
}