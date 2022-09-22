using System;
namespace jober.Models
{
    public class posts
    {
        public posts()
        {

        }

        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
    }
}

