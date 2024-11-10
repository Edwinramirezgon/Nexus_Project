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

        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        public List<string> Content { get; set; }

        public string LastMessage { get; set; }



        public DateTime DateandTime { get; set; }


        [JsonIgnore]
        public ICollection<MessageCollection> MessageCollections { get; set; }
    }
}
