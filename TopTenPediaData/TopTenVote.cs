using System;
using System.Collections.Generic;
using System.Text;

namespace TopTenPediaData
{
    public class TopTenVote
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Option> Options { get; set; }
    }
}
