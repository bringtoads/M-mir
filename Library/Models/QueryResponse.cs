using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{

    public class Choice
    {
        public int Index { get; set; }
        public Message Message { get; set; }
        public object Logprobs { get; set; }
        public string Finish_reason { get; set; }
        public object Stop_reason { get; set; }
    }

    public class Usage
    {
        public int Prompt_tokens { get; set; }
        public int Total_tokens { get; set; }
        public int Completion_tokens { get; set; }
    }

    public class QueryResponse
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public long Created { get; set; }
        public string Model { get; set; }
        public List<Choice> Choices { get; set; }
        public Usage Usage { get; set; }
    }
}
