using System.Collections.Generic;

namespace FinancialThing.Models
{
    public class StatementViewModel
    {
        public StatementViewModel()
        {
            DataViewModels = new List<DataItemViewModel>();
        }

        public string Name { get; set; }

        public List<int> Years { get; set; }

        public List<DataItemViewModel> DataViewModels { get; set; }
    }
}