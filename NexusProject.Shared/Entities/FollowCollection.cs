using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NexusProject.Shared.Entities
{
    public class FollowCollection
    {
        public int Id { get; set; }
       
        [ForeignKey("YoungsId")]
        [JsonIgnore]
        public Young Youngs { get; set; }
        public int Youngid { get; set; }

        [ForeignKey("TutorsId")]
        [JsonIgnore]
        public Tutor Tutors { get; set; }
        public int TutorsId { get; set; }

        [ForeignKey("FollowsId")]
        [JsonIgnore]
        public Follow Follows { get; set; }
        public int FollowsId { get; set; }
    }
}
