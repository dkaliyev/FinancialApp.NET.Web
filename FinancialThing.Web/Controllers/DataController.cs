using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace FinancialThing.Controllers
{
    public class DataController : Controller
    {
        private IDataServiceRepository _dataRepo;

        public DataController(IDataServiceRepository repo)
        {
            _dataRepo = repo;
        }
        //
        // GET: /Data/
        public ActionResult Index()
        {
            var states = Session["states"] as Dictionary<string, bool> ?? new Dictionary<string, bool>();

            var data = _dataRepo.GetQuery();
            var dataViewModels = new List<DataViewModel>();
            foreach (var datum in data)
            {
                var id = datum.Id.ToString();

                dataViewModels.Add(new DataViewModel()
                {
                    CompanyViewModel = new CompanyViewModel()
                    {
                        DisplayName = datum.FullName,
                        Id = id,
                        Toggle = new Toggle()
                        {
                            Id = id,
                            State = states.ContainsKey(id) ? states[id] : false,
                        },
                        Active = "hide"
                    },
                    Financials = datum.Financials,
                });
                var active = dataViewModels.FirstOrDefault(c=>c.CompanyViewModel.Toggle.State == true);
                if (active != null)
                {
                    active.CompanyViewModel.Active = "show";
                }
            }
            return View(dataViewModels);
        }

        public ActionResult GenerateAll()
        {
            _dataRepo.Add(null);
            return null;
        }

        [HttpPost]
        public ActionResult Generate(string id)
        {
            _dataRepo.SaveOrUpdate(new Company(){Id=new Guid(id)});
            return null;
        }
	}
}