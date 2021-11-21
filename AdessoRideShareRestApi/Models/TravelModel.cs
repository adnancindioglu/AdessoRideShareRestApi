using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShareRestApi.Models
{
    public class TravelModel
    {
        [Key]
        public Guid TravelId { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [Required]
        public string StartCity { get; set; }

        [Required]
        public string EndCity { get; set; }

        [Required]
        public DateTime TravelDate { get; set; }

        [Required]
        public int SeatingCapacity { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
