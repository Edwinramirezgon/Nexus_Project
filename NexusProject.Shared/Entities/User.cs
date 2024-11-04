//User Entitie whit all the validators
using Microsoft.AspNetCore.Identity;
using NexusProject.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NexusProject.Shared.Entities
{
    //inheritance whit the IdentityUser
    public class User : IdentityUser
    {



        [MaxLength(20, ErrorMessage = "More than 20 charachters are not allowed")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Document { get; set; }


        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string FirstName { get; set; }



        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string LastName { get; set; }
     

        public UserType UserType { get; set; }

      
        public string FullName => $"{FirstName} {LastName}";



    }
}
