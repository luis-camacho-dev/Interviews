using Common;
using Data;
using ILogic;

namespace BusinessLogic
{
    public class ServiceRequestManager : IServiceRequestManager
    {
        private UnitOfWork _unitOfWork;
        public ServiceRequestManager(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(ServiceRequestDTO serviceRequestDTO)
        {
            //TODO add AutoMapper 
            _unitOfWork.ServiceModelRepository.Add(Parse(serviceRequestDTO));
            _unitOfWork.Save();
        }

        public void Delete(Guid id)
        {
            _unitOfWork.ServiceModelRepository.Delete(id);
            _unitOfWork.Save();
        }

        public ServiceRequestDTO Get(Guid id)
        {
            // TODO Add AutoMapper
            var serviceRequest = _unitOfWork.ServiceModelRepository.GetById(id);
            if (serviceRequest != null)
            {
                return Parse(serviceRequest);
            }
            return null;
        }

        public IEnumerable<ServiceRequestDTO> GetAll()
        {
            // TODO Add AutoMapper
            var allServiceRequests = _unitOfWork.ServiceModelRepository.GetAll();
            if (allServiceRequests != null && allServiceRequests.Any())
            {
                return allServiceRequests.Select(s => Parse(s)).ToList();
            }
            return null;
            
        }

        public void Update(ServiceRequestDTO serviceRequestDTO)
        {
            var oldServiceRequest = _unitOfWork.ServiceModelRepository.GetById(serviceRequestDTO.Id);
            if (oldServiceRequest != null)
            {
                oldServiceRequest.CreatedDate = serviceRequestDTO.CreatedDate;
                oldServiceRequest.Description = serviceRequestDTO.Description;
                oldServiceRequest.LastModifiedDate = serviceRequestDTO.LastModifiedDate;
                oldServiceRequest.CreatedBy = serviceRequestDTO.CreatedBy;
                oldServiceRequest.BuildingCode = serviceRequestDTO.BuildingCode;
                oldServiceRequest.CurrentStatus = serviceRequestDTO.CurrentStatus.ToString();
                oldServiceRequest.LastModifiedBy = serviceRequestDTO.LastModifiedBy;

                // TODO Add AutoMapper
                _unitOfWork.ServiceModelRepository.Update(oldServiceRequest);
                _unitOfWork.Save();
                return;
            }
            throw new InvalidDataException($"There is not Service Request with Identifier={serviceRequestDTO.Id}");
            
        }

        private ServiceRequestDTO Parse(ServiceModel source)
        {
            Enum.TryParse(source.CurrentStatus, out CurrentStatus myStatus);
            return new ServiceRequestDTO
            {
                Id = source.Id,
                BuildingCode = source.BuildingCode,
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                CurrentStatus = myStatus, // conver int to Enum
                Description = source.Description,                
                LastModifiedBy = source.LastModifiedBy,
                LastModifiedDate = source.LastModifiedDate
            };
        }

        private ServiceModel Parse(ServiceRequestDTO source)
        {
           
            return new ServiceModel
            {
                Id = source.Id,
                BuildingCode = source.BuildingCode,
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                CurrentStatus = source.CurrentStatus.ToString(),
                Description = source.Description,
                LastModifiedBy = source.LastModifiedBy,
                LastModifiedDate = source.LastModifiedDate
            };
        }
    }
}