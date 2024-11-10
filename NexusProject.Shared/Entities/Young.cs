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
public class Young 
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(300, ErrorMessage = "More than 300 charachters are not allowed")]
        public string Interests { get; set; }

        public string UserDocument { get; set; }
        public User? Users { get; set; }

       
    }
}
