using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialThing.Models
{
    public class SectorsAndIndustriesViewModel
    {
        public IList<SectorViewModel> Sectors { get; set; }
        public IList<IndustriesViewModel> Industries { get; set; } 
    }
}