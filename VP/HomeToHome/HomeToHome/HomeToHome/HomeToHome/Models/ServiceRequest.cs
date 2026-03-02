using System.ComponentModel.DataAnnotations;

namespace HomeToHome.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }

        [Required]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        public string WorkerEmail { get; set; } =string.Empty;

        [Required]
        public string ServiceType { get; set; } =string.Empty ;

        public string Description { get; set; } = string.Empty ;

        public DateTime? PreferredDate { get; set; }

        public string PreferredTime { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string FullAddress { get; set; } = string.Empty;

        public string RequestStatus { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
