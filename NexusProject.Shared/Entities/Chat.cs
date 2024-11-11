using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusProject.Shared.Entities
{
    public class Chat
    {
        [Key]  
  
        public string ChatId { get; set; }
        public string User1Id { get; set; }
        public string User2Id { get; set; }
        public List<string> MessageHistory { get; set; } = new List<string>();
        public string LastMessage { get; set; }
        public DateTime LastMessageTime { get; set; }
    }
}