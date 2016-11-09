using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Filters;
using FinancialThing.Models;
using WebGrease.Css.Extensions;
using System.Threading.Tasks;

namespace FinancialThing.Controllers
{
    public class SummaryController : Controller
    {
        private readonly IRepository<Company, Guid> _repo;
        private readonly IRepository<Ratio, Guid> _ratioRepo;
        private readonly IRepository<RatioValue, Guid> _ratiovalueRepo;

        public SummaryController(IRepository<Company, Guid> repo, IRepository<Ratio, Guid> ratioRepo, IRepository<RatioValue, Guid> ratioValueRepository)
        {
            _repo = repo;
            _ratioRepo = ratioRepo;
            _ratiovalueRepo = ratioValueRepository;
        }

        [AllowJsonGet]
        public async Task<JsonResult> GetHeaders()
        {
            var headers = new List<object>();
            headers.Add(new { DisplayName = "Company name" });
            headers.Add(new { DisplayName = "Sector" });
            headers.Add(new { DisplayName = "Industry" });

            var ratios = await _ratioRepo.GetQuery();

            headers.AddRange(ratios.OrderBy(r => r.Id.ToString()).Select(r => new { DisplayName = r.Name }));

            return new JsonResult() { Data = headers };
        }

        [AllowJsonGet]
        public async Task<JsonResult> GetRatios()
        {
            var data = new List<object>();
            var ratios = await _ratiovalueRepo.GetQuery();
            var companies = await _repo.GetQuery();
            foreach (var ratio in ratios.GroupBy(r => r.CompanyId))
            {
                var company = companies.FirstOrDefault(c => c.Id.ToString() == ratio.Key);
                data.Add(new
                {
                    CompanyName = company.FullName,
                    SectorName = company.Sector.DisplayName,
                    IndustryName = company.Industry.DisplayName,
                    //ratios = ratio.OrderBy(r=>r.RatioId.ToString()).Select(r=>r.Value).ToList()
                });
            };
            return new JsonResult() { Data = data };
        }

        [AllowJsonGet]
        public JsonResult GetGroupedRatios()
        {
            //var data = new List<object>();
            //var ratios = _ratiovalueRepo.GetQuery().GroupBy(r => r.RatioId);
            return null;
        }

        //
        // GET: /Summary/
        public async Task<ActionResult> Index()
        {
            var vm = new Dictionary<string, string>();
            vm.Add("CompanyName", "Company Name");
            vm.Add("IndustryName", "Industry Name");
            vm.Add("SectorName", "Sector Name");
            vm.Add("Exc", "Stock Exchange");

            var ratios = await _ratioRepo.GetQuery();
            
            foreach(var ratio in ratios)
            {
                vm.Add(ratio.Id.ToString(), ratio.Name);
            }

            return View(vm);
        }

        [AllowJsonGet]
        public async Task<JsonResult> GetData()
        {
            var data = new List<Dictionary<string, string>>();
            
            var companies = await _repo.GetQuery();
            foreach (var company in companies)
            {
                var row = new Dictionary<string, string>();

                row.Add("CompanyName", company.FullName);
                row.Add("IndustryName", company.Industry.DisplayName);
                row.Add("SectorName", company.Sector.DisplayName);
                row.Add("Exc", company.StockExchange.DisplayName);

                var ratios = await _ratiovalueRepo.GetQuery();
                ratios = ratios.Where(r => r.CompanyId == company.Id.ToString());

                foreach(var ratio in ratios)
                {
                    row.Add(ratio.RatioId, ratio.Value);
                }

                data.Add(row);
            };
            return new JsonResult() { Data = data };
        }
    }
}