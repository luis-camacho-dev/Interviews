using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ServiceRequestContext :DbContext
    {
        public virtual DbSet<ServiceModel> Services { get; set; }
        public ServiceRequestContext(DbContextOptions<ServiceRequestContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        { 
            
        }
    }
}
