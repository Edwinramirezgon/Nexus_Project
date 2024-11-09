using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NexusProject.Shared.Entities
{
    public class Activity
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        public string Title{ get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(200, ErrorMessage = "More than 200 charachters are not allowed")]
        public string Description { get; set; }
        public DateTime DateandTime { get; set; }

        public int Percentage { get; set; }

        public string FileTask { get; set; }



        [JsonIgnore]
        public ICollection<ActivityColection> ActivityColections { get; set; }
    }
}

