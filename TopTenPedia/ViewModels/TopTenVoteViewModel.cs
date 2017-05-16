using Microsoft.CodeAnalysis.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopTenPediaData;

namespace TopTenPedia.ViewModels
{
    public class TopTenVoteViewModel
    {
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<Option> Options { get; set; }
		public int SelectedOptionId { get; set; }
		public long TotalVotes { get; set; }
	}
}
