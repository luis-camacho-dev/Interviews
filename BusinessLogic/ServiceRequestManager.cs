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
            return Parse(serviceRequest);
        }

        public IEnumerable<ServiceRequestDTO> GetAll()
        {
            // TODO Add AutoMapper
            return _unitOfWork.ServiceModelRepository.GetAll().Select( s => Parse(s)).ToList();
        }

        public void Update(ServiceRequestDTO serviceRequestDTO)
        {
            // TODO Add AutoMapper
            _unitOfWork.ServiceModelRepository.Update(Parse(serviceRequestDTO));
            _unitOfWork.Save();
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