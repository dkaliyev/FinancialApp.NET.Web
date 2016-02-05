using System.Collections.Generic;

namespace FinancialThing.Models
{
    public class CompanyCategoriesViewModel
    {
        public IList<CompanyViewModel> CompanyViewModels { get; set; }
        public IEnumerable<CategoryEntry> Sectors { get; set; }
        public IEnumerable<CategoryEntry> Industries { get; set; } 
    }
}