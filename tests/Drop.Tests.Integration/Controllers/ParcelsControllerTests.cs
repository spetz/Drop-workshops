using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Drop.Api;
using Drop.Core.Entities;
using Drop.Core.Repositories;
using Drop.Core.ValueObjects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Drop.Tests.Integration.Controllers
{
    public class ParcelsControllerTests  : IClassFixture<WebApplicationFactory<Startup>>
    {
        [Fact]
        public async Task get_parcel_should_return_dto()
        {
            // Arrange
            var parcel = new Parcel(Guid.NewGuid(), ParcelSize.Large, "test");
            await _parcelsRepository.AddAsync(parcel);

            var response = await _client.GetAsync($"api/parcels/{parcel.Id}");
            response.EnsureSuccessStatusCode();
        }
        
        private readonly HttpClient _client;
        private readonly IParcelsRepository _parcelsRepository;

        public ParcelsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _parcelsRepository = new ParcelsRepository();
            _client = factory.WithWebHostBuilder(builder =>
                {
                    // builder.UseEnvironment("test");
                    builder.ConfigureServices(services => { services.AddSingleton(_parcelsRepository); });
                    // builder.UseStartup<TestStartup>();
                })
                .CreateClient();
        }

        private class ParcelsRepository : IParcelsRepository
        {
            private readonly List<Parcel> _parcels = new List<Parcel>();

            public async Task<Parcel> GetAsync(Guid id) => _parcels.SingleOrDefault(x => x.Id == id);
            
            public async Task AddAsync(Parcel parcel)
            {
                _parcels.Add(parcel);
            }

            public async Task UpdateAsync(Parcel parcel)
            {
            }
        }
    }

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureTestServices()
        {
            base.ConfigureTestServices();
        }
    }
}