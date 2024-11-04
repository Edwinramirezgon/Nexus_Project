using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NexusProject.Shared.Entities
{
    public class Follow
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]      
        public int punctuation { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(200, ErrorMessage = "More than 200 charachters are not allowed")]
        public string Remarks { get; set; }


        [JsonIgnore]
        public ICollection<FollowCollection> FollowCollections { get; set; }
    }
}
