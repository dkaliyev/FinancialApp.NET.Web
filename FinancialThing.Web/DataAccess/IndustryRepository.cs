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

namespace FinancialThing.DataAccess
{
    public class IndustryRepository: IRepository<Industry, Guid>
    {
        private readonly IDataGrabber _grabber;
        private string ServiceUrl { get; set; }

        public IndustryRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }

        public async Task<Industry> GetById(Guid id)
        {
            var resp = await _grabber.Get(string.Format("{0}api/industry/{1}", ServiceUrl, id));
            var status = JsonConvert.DeserializeObject<Status>(resp);
            if (status.StatusCode == "1")
            {
                throw new Exception(status.Data);
            }
            var data = JsonConvert.DeserializeObject<Industry>(status.Data);
            return data;
        }

        public async Task<IQueryable<Industry>> GetQuery()
        {
            var resp = await _grabber.Get(string.Format("{0}api/industry/", ServiceUrl));
            var status = JsonConvert.DeserializeObject<Status>(resp);
            if (status.StatusCode == "1")
            {
                throw new Exception(status.Data);
            }
            var data = JsonConvert.DeserializeObject<IEnumerable<Industry>>(status.Data);
            return data.AsQueryable();
        }

        public async Task<Industry> Add(Industry entity)
        {
            if (entity != null)
            {
                var data = JsonConvert.SerializeObject(entity);
                var res = await _grabber.Post(string.Format("{0}api/industry/", ServiceUrl), data);
                var status = JsonConvert.DeserializeObject<Status>(res);
                if (status.StatusCode != "0")
                {
                    throw new Exception(status.Data);
                }
            }
            return null;
        }

        public Task Delete(Industry entity)
        {
            throw new NotImplementedException();
        }

        public Task<Industry> FindBy(Expression<Func<Industry, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task SaveOrUpdate(Industry entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Industry entity)
        {
            throw new NotImplementedException();
        }
    }
}