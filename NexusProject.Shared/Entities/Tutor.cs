using NexusProject.Shared.DTOs;
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
    public class Tutor
    { 

        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        public string Speciality { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        public string Profession { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        public string Availability { get; set; }

        public string UserDocument { get; set; }
        public User? Users { get; set; }

        

    }
}
