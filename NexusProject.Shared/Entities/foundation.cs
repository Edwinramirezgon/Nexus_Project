using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NexusProject.Shared.Entities
{
    public class foundation
    {
        public int Id { get; set; }

      
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(400, ErrorMessage = "More than 400 charachters are not allowed")]
        public string Dscription { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(400, ErrorMessage = "More than 400 charachters are not allowed")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(400, ErrorMessage = "More than 400 charachters are not allowed")]
        public string Proyects { get; set; }

        [ForeignKey("YoungId")]
        [JsonIgnore]
        public Young young { get; set; }
        public int YoungId { get; set; }


        [ForeignKey("ActivityId")]
        [JsonIgnore]
        public Activity activity { get; set; }
        public int ActivityId { get; set; }
    }
}
