using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkymeyJobsLibs.Models.Crypto.Tokens
{
    public class TokenListViewModel
    {
        public List<API_TOKEN> TokenLists { get; set; }
        public List<CurrentPrices> CurrentPricess { get; set; }
    }
}
