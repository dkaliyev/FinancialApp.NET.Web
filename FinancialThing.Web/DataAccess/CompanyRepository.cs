using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FinancialThing.Web.DataAccess
{
    public class CompanyRepository: ICompanyRepository
    {
        private readonly IDataGrabber _grabber;
        private string ServiceUrl { get; set; }
        public CompanyRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }
        public async Task<Company> GetById(Guid id)
        {
            var resp = await _grabber.Get(string.Format("{0}api/company/{1}", ServiceUrl, id));
            var status = JsonConvert.DeserializeObject<Status>(resp);
            if(status.StatusCode == "1")
            {
                throw new Exception(status.Data);
            }
            return JsonConvert.DeserializeObject<Company>(status.Data);
        }

        public async Task<IQueryable<Company>> GetQuery()
        {
            var resp = await _grabber.Get(string.Format("{0}api/company/", ServiceUrl));
            var status = JsonConvert.DeserializeObject<Status>(resp);
            if (status.StatusCode == "1")
            {
                throw new Exception(status.Data);
            }
            var data = JsonConvert.DeserializeObject<IEnumerable<Company>>(status.Data);
            return data.AsQueryable();
        }

        public async Task<Company> FindBy(Expression<Func<Company, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<Company> Add(Company entity)
        {
            if (entity != null)
            {
                var data = JsonConvert.SerializeObject(entity);
                var res = await _grabber.Post(string.Format("{0}api/company/", ServiceUrl), data);
                var status = JsonConvert.DeserializeObject<Status>(res);
                if (status.StatusCode != "0")
                {
                    throw new Exception(status.Data);
                }
                return JsonConvert.DeserializeObject<Company>(status.Data);
            }
            throw new Exception("Company is null");
        }

        public Task Update(Company entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Company entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveOrUpdate(Company entity)
        {
            throw new NotImplementedException();
        }
    }
}