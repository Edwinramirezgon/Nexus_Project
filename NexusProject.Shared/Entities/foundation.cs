using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
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
    }
}
