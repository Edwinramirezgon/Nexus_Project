using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NexusProject.Shared.Entities
{
    public class Message
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        public string content { get; set; }

        public DateTime DateandTime { get; set; }


        [JsonIgnore]
        public ICollection<MessageCollection> MessageCollections { get; set; }
    }
}
