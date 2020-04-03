using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMakerV2.Model
{
    public class Event
    {
        public Event()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Name}, {Place}, {Date}\n\t{Description}";
        }
    }
}
