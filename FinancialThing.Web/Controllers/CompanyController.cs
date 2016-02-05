using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Filters;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Newtonsoft.Json;

namespace FinancialThing.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyServiceRepository _repo;
        private readonly ISectorServiceRepository _sectorsRepo;
        private readonly IIndustryServiceRepository _industryService;
        private readonly IStockExchangeServiceRepository _stockRepo;

        public CompanyController(ICompanyServiceRepository repo, IStockExchangeServiceRepository stockRepo, ISectorServiceRepository sectorRepo, IIndustryServiceRepository industryRepo)
        {
            _repo = repo;
            _stockRepo = stockRepo;
            _industryService = industryRepo;
            _sectorsRepo = sectorRepo;
        }
        //
        // GET: /Company/
        public ActionResult Index()
        {
            if(Session["states"] == null)
            {
                Session["states"] = new Dictionary<string, bool>();
            }
            var states = Session["states"] as Dictionary<string, bool>;

            var companies = _repo.GetQuery();

            var companyViewModels = new List<CompanyViewModel>();
            foreach (var company in companies)
            {
                var id = company.Id.ToString();
                states[id] = states.ContainsKey(id) && states[id];
                companyViewModels.Add(new CompanyViewModel()
                {
                    DisplayName = company.FullName,
                    Id = id,
                    Toggle = new Toggle()
                    {
                        Id = id,
                        State =  states[id]
                    }
                });
            }

            return View(companyViewModels);
        }

        [AllowJsonGet]
        public JsonResult GetExchanges()
        {
            var exchanges =
                _stockRepo.GetQuery()
                    .Select(e => new ExchangeViewModel() {DisplayName = e.DisplayName, Marker = e.Marker}).ToList();
            return new JsonResult() {Data = exchanges};
        }

        [HttpPost]
        public void Toggle(Toggle toggle)
        {
            var states = Session["states"] as Dictionary<string, bool>;

            states[toggle.Id] = !toggle.State;

            Session["states"] = states;
        }

        public void ToggleAll(int? id)
        {
            var toggle = id != 0;
            var states = Session["states"] as Dictionary<string, bool>;
            var ids = new List<string>();
            foreach (var key in states.Keys)
            {
                ids.Add(key);
            }

            foreach (var id1 in ids)
            {
                states[id1] = toggle;
            }

            Session["states"] = states;
        }

        public ActionResult AddCompany(CompanyViewModel companyVm)
        {
            if (companyVm != null)
            {
                var newComp = new Company();
                var sector = _sectorsRepo.GetQuery().FirstOrDefault(s => s.Code == companyVm.Sector.Code);
                var industry = _industryService.GetQuery().FirstOrDefault(i => i.Code == companyVm.Industry.Code);
                var exchange = _stockRepo.GetQuery().FirstOrDefault(e => e.Marker == companyVm.StockExchange.Marker);

                newComp.Sector = sector;
                newComp.Industry = industry;
                newComp.StockExchange = exchange;
                newComp.StockName = companyVm.DisplayName;

                newComp = _repo.Add(newComp);
                if (newComp != null)
                {
                    var states = Session["states"] as Dictionary<string, bool>;
                    states.Add(newComp.Id.ToString(), false);
                    Session["states"] = states;
                    var viewModel = new CompanyViewModel()
                    {
                        DisplayName = newComp.FullName,
                        Id = newComp.Id.ToString(),
                        Toggle = new Toggle()
                        {
                            Id = newComp.Id.ToString(),
                            State = false
                        }
                    };
                    return PartialView("Partial/_Company", viewModel);
                }
            }
            return View("Partial/_Company", null); ;
        }
	}
}