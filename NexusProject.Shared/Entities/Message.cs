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
    public class Message
    {

        [Key]  
     
        public string MessageId { get; set; } = Guid.NewGuid().ToString();
        public string ChatId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime DateandTime { get; set; }
    }
}
