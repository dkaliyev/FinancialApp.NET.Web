using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Filters;
using FinancialThing.Models;

namespace FinancialThing.Controllers
{
    public class SectorsAndIndustriesController : Controller
    {
        private ISectorServiceRepository _sectorRepo;
        private IIndustryServiceRepository _industryRepo;

        public SectorsAndIndustriesController(ISectorServiceRepository sectorRepo,
            IIndustryServiceRepository industryRepo)
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
        public JsonResult GetIndustries()
        {
            var industries = _industryRepo.GetQuery();
            var Industries = industries.Select(i => new IndustriesViewModel() {DisplayName = i.DisplayName, Code = i.Code}).ToList();
            return new JsonResult() {Data = Industries};
        }

        [AllowJsonGet]
        public JsonResult GetIndustriesBySector(string sector)
        {
            var industries = _industryRepo.GetQuery().Where(i=>i.Sector.Code == sector || sector == "");
            var Industries = industries.Select(i => new IndustriesViewModel() { DisplayName = i.DisplayName, Code = i.Code }).ToList();
            return new JsonResult() { Data = Industries };
        }

        [AllowJsonGet]
        public JsonResult GetSectors()
        {
            var sectors = _sectorRepo.GetQuery();
            var Sectors = sectors.Select(i => new SectorViewModel() { DisplayName = i.DisplayName, Code = i.Code }).ToList();
            return new JsonResult() {Data = Sectors};
        }

        public JsonResult SaveSector(SectorViewModel sector)
        {
            _sectorRepo.Add(new Sector() { Code = sector.Code, DisplayName = sector.DisplayName});
            return new JsonResult() {Data = sector};
        }

        [HttpPost]
        public JsonResult SaveIndustry(IndustriesViewModel industry)
        {
            var sector = _sectorRepo.GetQuery().FirstOrDefault(s => s.Code == industry.SectorCode);
            _industryRepo.Add(new Industry(){Code = industry.Code, DisplayName = industry.DisplayName, Sector = new Sector() {Id = sector.Id}});
            return new JsonResult() {Data = industry};
        }

	}
}