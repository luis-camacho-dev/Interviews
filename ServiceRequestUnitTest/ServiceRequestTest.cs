using ServiceRequest.Controllers;

using ILogic;
using System;
using Microsoft.EntityFrameworkCore;
using Data;
using BusinessLogic;

using Common;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace ServiceRequestUnitTest
{
    
    public class ServiceRequestTest
    {
        private DbContextOptions<ServiceRequestContext> dbContextOptions;
        private ServiceRequestContext serviceRequestContext;
        private UnitOfWork _unitOfWork;
        private IServiceRequestManager _serviceRequestManager;
        private ServiceRequestController _serviceRequestController;
        public ServiceRequestTest()
        {
            Setup();
        }
        
        private void Setup()
        {
            dbContextOptions = new DbContextOptionsBuilder<ServiceRequestContext>()
                    .UseInMemoryDatabase(databaseName: "PrimeDb")
                    .Options;
            serviceRequestContext = new ServiceRequestContext(dbContextOptions);
            _unitOfWork = new UnitOfWork(serviceRequestContext);
            _serviceRequestManager = new ServiceRequestManager(_unitOfWork);
            _serviceRequestController = new ServiceRequestController(_serviceRequestManager);
        }
        
        [Fact]
        public void SHouldGetServiceRequestByIdWhenThereIsAValidServiceRequest()
        {
            ServiceRequestDTO serviceRequestDTO = new ServiceRequestDTO()
            {
                Id = Guid.NewGuid(),
                BuildingCode = "test",
                CreatedBy = "Luis Campos",
                CreatedDate = DateTime.Now.AddDays(-15),
                CurrentStatus = CurrentStatus.Created,
                Description = "Description",
                LastModifiedBy = "Create a new Service Request",
                LastModifiedDate = DateTime.Now.AddDays(-16)
            };
            _serviceRequestController.Post(serviceRequestDTO);
            Assert.NotNull(_serviceRequestController.Get(serviceRequestDTO.Id).Value);
        }

        [Fact]
        public void ShouldUpdateServiceRequestWhenDataIsValid()
        {
            ServiceRequestDTO serviceRequestDTO = new ServiceRequestDTO()
            {
                Id = Guid.NewGuid(),
                BuildingCode = "TEST",
                CreatedBy = "Luis Campos",
                CreatedDate = DateTime.Now.AddDays(-15),
                CurrentStatus = CurrentStatus.Created,
                Description = "Description",
                LastModifiedBy = "Create a new Service Request",
                LastModifiedDate = DateTime.Now.AddDays(-16)
            };
            _serviceRequestController.Post(serviceRequestDTO);
            serviceRequestDTO.BuildingCode = "TEST 1";
            var updateResultRequest = _serviceRequestController.Put(serviceRequestDTO, serviceRequestDTO.Id);
            Assert.True(updateResultRequest.GetType() == typeof(OkResult));
        }

        [Fact]
        public void ShouldDeleteServiceRequestWhenDataExists()
        {
            ServiceRequestDTO serviceRequestDTO = new ServiceRequestDTO()
            {
                Id = Guid.NewGuid(),
                BuildingCode = "TEST",
                CreatedBy = "Luis Campos",
                CreatedDate = DateTime.Now.AddDays(-15),
                CurrentStatus = CurrentStatus.Created,
                Description = "Description",
                LastModifiedBy = "Create a new Service Request",
                LastModifiedDate = DateTime.Now.AddDays(-16)
            };
            _serviceRequestController.Post(serviceRequestDTO);
            Assert.True(_serviceRequestController.Delete(serviceRequestDTO.Id).GetType() == typeof(OkResult));
            Assert.Null(_serviceRequestController.Get(serviceRequestDTO.Id).Value);
        }
    }
}