using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork
    {
        private readonly ServiceRequestContext _dbContext;

        private IRepository<ServiceModel> _serviceModelRepository = null;
        public UnitOfWork(ServiceRequestContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e) 
            {
                throw;
            }
        }

        public IRepository<ServiceModel> ServiceModelRepository
        {
            get
            {
                _serviceModelRepository = _serviceModelRepository ?? new Repository<ServiceModel>(_dbContext);
                return _serviceModelRepository;
            }
        }
    
    }
}
