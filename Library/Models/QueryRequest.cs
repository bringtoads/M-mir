using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class QueryRequest
    {
        public string model { get; set; }
        public List<Message> messages { get; set; } = new List<Message>();       
    }
    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}
