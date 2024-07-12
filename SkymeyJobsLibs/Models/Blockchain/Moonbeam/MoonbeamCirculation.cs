using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkymeyJobsLibs.Models.Blockchain.Moonbeam
{
    public class MoonbeamCirculation
    {
        public int circulateSupply { get; set; }
        public int totalHolders { get; set; }
        public int finalizedBlocks { get; set; }
    }
}
