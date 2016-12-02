using System.Collections.Generic;

namespace FinancialThing.Models
{
    public class DataCompanyViewModel
    {
        public string Id { get; set; }
        public List<StatementViewModel> StatementViewModels { get; set; }
    }
}