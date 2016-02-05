using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialThing.Models
{
    public class RatioViewModel
    {
        public IEnumerable<Ratio> Ratios { get; set; }
        public IEnumerable<Dictionary> Dictionary { get; set; }
    }
}