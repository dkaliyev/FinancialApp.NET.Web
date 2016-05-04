using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Filters;
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

        [AllowJsonGet]
        public JsonResult GetRatios()
        {
            var ratios = _ratioServiceRepository.GetQuery().ToList();
            return new JsonResult() { Data = new { _ratios = ratios } };
        }

        public JsonResult GetDictionary()
        {
            var pages = new List<string>() { "FI", "BalanceSh", "CashFlow", "IncomeStatement" };
            var dics = _dictionaryServiceRepository.GetQuery().Where(d => !pages.Contains(d.ParentCode)).ToList();
            return new JsonResult() { Data = new { _dics = dics } };
        }

        [HttpPost]
        public JsonResult AddRatio(Ratio ratio)
        {
            var res = _ratioServiceRepository.Add(ratio);
            var status = "fail";
            if (res != null)
                status = "success";
            return new JsonResult { Data = new { _status = status, _ratio = res } };
        }

        [HttpPost]
        public JsonResult RemoveRatio(Ratio ratio)
        {
            _ratioServiceRepository.Delete(ratio);
            return new JsonResult { Data = new { _status = "done" } };
        }

        [HttpPost]
        public JsonResult BuildRatios()
        {
            _ratioValueRepo.Add(null);
            return new JsonResult { Data = new { _status = "done" } };
        }
    }
}