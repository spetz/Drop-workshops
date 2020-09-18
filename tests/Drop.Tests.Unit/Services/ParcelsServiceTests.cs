using System;
using System.Threading.Tasks;
using Drop.Application.Commands;
using Drop.Application.Services;
using Drop.Core.Entities;
using Drop.Core.Repositories;
using Drop.Core.ValueObjects;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Drop.Tests.Unit.Services
{
    public class ParcelsServiceTests
    {
        [Fact]
        public async Task get_async_should_return_parcel_dto()
        {
            // Arrange
            var parcel = new Parcel(Guid.NewGuid(), ParcelSize.Large, "test");
            _parcelsRepository.GetAsync(parcel.Id).Returns(parcel);
            
            // Act
            var parcelDto = await _parcelsService.GetAsync(parcel.Id);
            
            // Assert
            parcelDto.ShouldNotBeNull();
            parcelDto.Id.ShouldBe(parcel.Id);
            await _parcelsRepository.Received().GetAsync(parcel.Id);
        }
        
        [Fact]
        public async Task add_parcel_should_succeed_given_valid_size_and_address()
        {
            var command = new AddParcel("test", "large");
            
            await _parcelsService.AddAsync(command);

            await _parcelsRepository.Received().AddAsync(Arg.Is<Parcel>(x =>
                x.Id == command.Id &&
                x.Address == command.Address &&
                x.Size == ParcelSize.Large &&
                x.State == ParcelState.New));
        }

        private readonly IParcelsService _parcelsService;
        private readonly IParcelsRepository _parcelsRepository;
        
        public ParcelsServiceTests()
        {
            _parcelsRepository = Substitute.For<IParcelsRepository>();
            _parcelsService = new ParcelsService(_parcelsRepository);
        }
    }
}