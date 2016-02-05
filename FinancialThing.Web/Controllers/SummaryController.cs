using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Filters;
using FinancialThing.Models;
using WebGrease.Css.Extensions;

namespace FinancialThing.Controllers
{
    public class SummaryController : Controller
    {
        private readonly ICompanyServiceRepository _repo;
        private readonly IRatioServiceRepository _ratioRepo;
        private readonly IRatioValueServiceRepository _ratiovalueRepo;

        public SummaryController(ICompanyServiceRepository repo, IRatioServiceRepository ratioRepo, IRatioValueServiceRepository ratioValueRepository)
        {
            _repo = repo;
            _ratioRepo = ratioRepo;
            _ratiovalueRepo = ratioValueRepository;
        }

        [AllowJsonGet]
        public JsonResult GetHeaders()
        {
            var headers = new List<object>();
            headers.Add(new { DisplayName = "Company name" });
            headers.Add(new { DisplayName = "Sector" });
            headers.Add(new { DisplayName = "Industry" });

            headers.AddRange(_ratioRepo.GetQuery().OrderBy(r=>r.Id.ToString()).Select(r => new {DisplayName = r.Name}));

            return new JsonResult() {Data = headers};
        }

        [AllowJsonGet]
        public JsonResult GetRatios()
        {
            var data = new List<object>();
            var ratios = _ratiovalueRepo.GetQuery().GroupBy(r=>r.CompanyId);
            var companies = _repo.GetQuery();
            foreach (var ratio in ratios)
            {
                var company = companies.FirstOrDefault(c => c.Id.ToString() == ratio.Key);
                data.Add(new
                {
                    CompanyName = company.FullName,
                    SectorName = company.Sector.DisplayName,
                    IndustryName = company.Industry.DisplayName,
                    ratios = ratio.OrderBy(r=>r.RatioId.ToString()).Select(r=>r.Value).ToList()
                });
            };
            return new JsonResult() {Data = data};
        }

        //
        // GET: /Summary/
        public ActionResult Index()
        {
            var vm = new SummaryViewModel();
            vm.Companies = new List<Company>()
            {
                new Company()
                {
                    FullName = "Company 1",
                    Industry = new Industry()
                    {
                        DisplayName = "Industry 1"
                    },
                    Sector = new Sector()
                    {
                        DisplayName = "Sector 1"
                    }
                },
                new Company()
                {
                    FullName = "Company 2",
                    Industry = new Industry()
                    {
                        DisplayName = "Industry 2"
                    },
                    Sector = new Sector()
                    {
                        DisplayName = "Sector 2"
                    }
                },
                new Company()
                {
                    FullName = "Company 3",
                    Industry = new Industry()
                    {
                        DisplayName = "Industry 3"
                    },
                    Sector = new Sector()
                    {
                        DisplayName = "Sector 3"
                    }
                },
                new Company()
                {
                    FullName = "Company 4",
                    Industry = new Industry()
                    {
                        DisplayName = "Industry 4"
                    },
                    Sector = new Sector()
                    {
                        DisplayName = "Sector 4"
                    }
                }
            };
            //var companies = _repo.GetQuery();
            return View(vm);
        }
	}
}