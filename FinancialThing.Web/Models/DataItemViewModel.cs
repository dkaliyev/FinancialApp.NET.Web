using System.Collections.Generic;

namespace FinancialThing.Models
{
    public class DataItemViewModel
    {
        /*blic CompanyViewModel CompanyViewModel { get; set; }

        public Financials Financials { get; set; }*/

        public DataItemViewModel()
        {
            ValueViewModels = new List<ValueViewModel>();
        }

        public string Name { get; set; }

        public List<ValueViewModel> ValueViewModels { get; set; }
    }
}