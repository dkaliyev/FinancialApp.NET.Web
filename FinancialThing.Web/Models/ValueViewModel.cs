using System.Collections.Generic;

namespace FinancialThing.Models
{
    public class ValueViewModel
    {
        public ValueViewModel()
        {
            Values = new List<decimal>();
        }
        public string Name { get; set; }
        public List<decimal> Values { get; set; }
    }
}