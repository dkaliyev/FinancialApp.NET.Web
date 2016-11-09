using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Filters;
using FinancialThing.Models;
using System.Threading.Tasks;

namespace FinancialThing.Controllers
{
    public class SectorsAndIndustriesController : Controller
    {
        private IRepository<Sector, Guid> _sectorRepo;
        private IRepository<Industry, Guid> _industryRepo;

        public SectorsAndIndustriesController(IRepository<Sector, Guid> sectorRepo,
            IRepository<Industry, Guid> industryRepo)
        {
            _sectorRepo = sectorRepo;
            _industryRepo = industryRepo;
        }

        //
        // GET: /SectorsAndIndustries/
        public ActionResult Index()
        {
            return View();
        }

        [AllowJsonGet]
        public async Task<JsonResult> GetIndustries()
        {
            var industries = await _industryRepo.GetQuery();
            var Industries = industries.Select(i => new IndustriesViewModel() {DisplayName = i.DisplayName, Code = i.Code}).ToList();
            return new JsonResult() {Data = Industries};
        }

        [AllowJsonGet]
        public async Task<JsonResult> GetIndustriesBySector(string sector)
        {
            var industries = await _industryRepo.GetQuery();
            var inds = industries.Where(i=>i.Sector.Code == sector || sector == "");
            var Industries = inds.Select(i => new IndustriesViewModel() { DisplayName = i.DisplayName, Code = i.Code }).ToList();
            return new JsonResult() { Data = Industries };
        }

        [AllowJsonGet]
        public async Task<JsonResult> GetSectors()
        {
            var sectors = await _sectorRepo.GetQuery();
            var Sectors = sectors.Select(i => new SectorViewModel() { DisplayName = i.DisplayName, Code = i.Code }).ToList();
            return new JsonResult() {Data = Sectors};
        }

        public async Task<JsonResult> SaveSector(SectorViewModel sector)
        {
            await _sectorRepo.Add(new Sector() { Code = sector.Code, DisplayName = sector.DisplayName});
            return new JsonResult() {Data = sector};
        }

        [HttpPost]
        public async Task<JsonResult> SaveIndustry(IndustriesViewModel industry)
        {
            var sector = await _sectorRepo.GetQuery();
            var sec = sector.FirstOrDefault(s => s.Code == industry.SectorCode);
             await _industryRepo.Add(new Industry(){Code = industry.Code, DisplayName = industry.DisplayName, Sector = new Sector() {Id = sec.Id}});
            return new JsonResult() {Data = industry};
        }

	}
}