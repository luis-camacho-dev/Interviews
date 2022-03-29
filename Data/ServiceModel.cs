using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class ServiceModel
    {
        [Key]
        public Guid Id { get; set; }

        public string BuildingCode { get; set; }    

        public string Description   { get; set; }

        public string CurrentStatus { get; set; } 

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}