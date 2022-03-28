using Common;

namespace ILogic
{
    public interface IServiceRequestManager
    {
        IEnumerable<ServiceRequestDTO> GetAll();

        ServiceRequestDTO Get(Guid id);

        void Update(ServiceRequestDTO serviceRequestDTO);

        void Create(ServiceRequestDTO serviceRequestDTO);

        void Delete(Guid id);
    }
}