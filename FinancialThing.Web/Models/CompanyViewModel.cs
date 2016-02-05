using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialThing.Models
{
    public class CompanyViewModel
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }

        public Toggle Toggle { get; set; }
        public string Active { get; set; }

        public ExchangeViewModel StockExchange { get; set; }
        public SectorViewModel Sector { get; set; }
        public IndustriesViewModel Industry { get; set; }

    }
}