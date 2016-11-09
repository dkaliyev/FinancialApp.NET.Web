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
using System.Threading.Tasks;

namespace FinancialThing.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _repo;
        private readonly IRepository<Sector, Guid> _sectorsRepo;
        private readonly IRepository<Industry, Guid> _industryService;
        private readonly IRepository<StockExchange, Guid> _stockRepo;

        public CompanyController(ICompanyRepository repo, IRepository<StockExchange, Guid> stockRepo, IRepository<Sector, Guid> sectorRepo, IRepository<Industry, Guid> industryRepo)
        {
            _repo = repo;
            _stockRepo = stockRepo;
            _industryService = industryRepo;
            _sectorsRepo = sectorRepo;
        }
        //
        // GET: /Company/
        public async Task<ActionResult> Index()
        {
            if(Session["states"] == null)
            {
                Session["states"] = new Dictionary<string, bool>();
            }
            var states = Session["states"] as Dictionary<string, bool>;

            var companies = await _repo.GetQuery();

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
        public async Task<JsonResult> GetExchanges()
        {
            var exchanges = await _stockRepo.GetQuery();
            var data = exchanges.Select(e => new ExchangeViewModel() {DisplayName = e.DisplayName, Marker = e.Marker}).ToList();
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

        public async Task<ActionResult> AddCompany(CompanyViewModel companyVm)
        {
            if (companyVm != null)
            {
                var newComp = new Company();
                var sectors = await _sectorsRepo.GetQuery();
                var sector = sectors.FirstOrDefault(s => s.Code == companyVm.Sector.Code);
                var industries = await _industryService.GetQuery();
                var industry = industries.FirstOrDefault(i => i.Code == companyVm.Industry.Code);
                var exchanges = await _stockRepo.GetQuery();
                var exchange = exchanges.FirstOrDefault(e => e.Marker == companyVm.StockExchange.Marker);

                newComp.Sector = sector;
                newComp.Industry = industry;
                newComp.StockExchange = exchange;
                newComp.StockName = companyVm.DisplayName;

                newComp = await _repo.Add(newComp);
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