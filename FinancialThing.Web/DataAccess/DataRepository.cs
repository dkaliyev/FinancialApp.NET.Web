using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Newtonsoft.Json;
using System.Threading.Tasks;
using FinancialThing.DataAccess;

namespace FinancialThing.Web.DataAccess
{
    public class DataRepository: IRepository<Company, Guid>    
    {
        private IDataGrabber _grabber;
        private string ServiceUrl { get; set; }

        public DataRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }

        public async Task<Company> GetById(Guid id)
        {
            var resp = await _grabber.Get(string.Format("{0}api/data/{1}", ServiceUrl, id));
            var data = JsonConvert.DeserializeObject<Status>(resp);
            var company = JsonConvert.DeserializeObject<Company>(data.Data);
            return company;
        }

        public async Task<IQueryable<Company>> GetQuery()
        {
            var resp = await _grabber.Get(string.Format("{0}api/data/", ServiceUrl));
            var data = JsonConvert.DeserializeObject<Status>(resp);
            var list = JsonConvert.DeserializeObject<IEnumerable<Company>>(data.Data);
            return list.AsQueryable();
        }

        public Task<Company> FindBy(Expression<Func<Company, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<Company> Add(Company entity)
        {
            var resp = await _grabber.Put(string.Format("{0}api/data/", ServiceUrl), "");
            var status = JsonConvert.DeserializeObject<Status>(resp);
            if(status.StatusCode == "1")
            {
                throw new Exception(status.Data);
            }
            return null;
        }

        public async Task SaveOrUpdate(Company entity)
        {   
            var resp = await _grabber.Put(string.Format("{0}api/data/{1}", ServiceUrl, entity.Id), "");
            var status = JsonConvert.DeserializeObject<Status>(resp);
            if (status.StatusCode == "1")
            {
                throw new Exception(status.Data);
            }
        }

        public Task Delete(Company entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Company entity)
        {
            throw new NotImplementedException();
        }
    }
}