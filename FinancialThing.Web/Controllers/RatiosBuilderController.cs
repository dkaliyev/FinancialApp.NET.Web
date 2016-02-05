using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Microsoft.Ajax.Utilities;

namespace FinancialThing.Controllers
{
    public class RatiosBuilderController : Controller
    {
        private IDictionaryServiceRepository _dictionaryServiceRepository;

        private IRatioServiceRepository _ratioServiceRepository;

        private IRatioValueServiceRepository _ratioValueRepo;


        public RatiosBuilderController(IDictionaryServiceRepository dictionaryServiceRepository, IRatioServiceRepository ratioServiceRepository,
            IRatioValueServiceRepository ratioValueRepo)
        {
            _dictionaryServiceRepository = dictionaryServiceRepository;
            _ratioServiceRepository = ratioServiceRepository;
            _ratioValueRepo = ratioValueRepo;
        }
        //
        // GET: /Ratios/
        public ActionResult Index()
        {
            var pages = new List<string>() { "FI", "BalanceSh", "CashFlow", "IncomeStatement" };
            var dics = _dictionaryServiceRepository.GetQuery().Where(d => !pages.Contains(d.ParentCode)).AsEnumerable();
            var ratios = _ratioServiceRepository.GetQuery().AsEnumerable();

            var vm = new RatioViewModel()
            {
                Ratios = ratios,
                Dictionary = dics
            };

            return View(vm);
        }

        [HttpPost]
        public JsonResult AddRatio(Ratio ratio)
        {
            var res = _ratioServiceRepository.Add(ratio);
            var status = "fail";
            if (res != null)
                status = "success";
            return new JsonResult {Data = new {_status = status, _ratio = res}};
        }

        [HttpPost]
        public JsonResult BuildRatios()
        {
            _ratioValueRepo.Add(null);
            return new JsonResult { Data = new { _status = "done" } };
        }
	}
}