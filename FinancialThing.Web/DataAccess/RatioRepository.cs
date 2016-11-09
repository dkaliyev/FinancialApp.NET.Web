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
    public class RatioRepository : IRepository<Ratio, Guid>
    {
        private readonly IDataGrabber _grabber;

        private string ServiceUrl { get; set; }

        public RatioRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }

        public async Task<Ratio> GetById(Guid id)
        {
            var resp = await _grabber.Get(string.Format("{0}api/ratio/{1}", ServiceUrl, id));
            var status = JsonConvert.DeserializeObject<Status>(resp);
            if (status.StatusCode == "1")
            {
                throw new Exception(status.Data);
            }
            var data = JsonConvert.DeserializeObject<Ratio>(status.Data);
            return data;
        }

        public async Task<IQueryable<Ratio>> GetQuery()
        {
            var resp = await _grabber.Get(string.Format("{0}api/ratio", ServiceUrl));
            var status = JsonConvert.DeserializeObject<Status>(resp);
            if (status.StatusCode == "1")
            {
                throw new Exception(status.Data);
            }
            var data = JsonConvert.DeserializeObject<IEnumerable<Ratio>>(status.Data);
            return data.AsQueryable();
        }

        public async Task<Ratio> Add(Ratio entity)
        {
            if (entity != null)
            {
                var data = JsonConvert.SerializeObject(entity);
                var res =  await _grabber.Post(string.Format("{0}api/ratio/", ServiceUrl), data);
                var status = JsonConvert.DeserializeObject<Status>(res);
                if (status.StatusCode != "0")
                {
                    throw new Exception(status.Data);
                }
            }
            return null;
        }

        public async Task Delete(Ratio entity)
        {
            if (entity != null)
            {
                var data = JsonConvert.SerializeObject(entity);
                var res = await _grabber.Delete(string.Format("{0}api/ratio/", ServiceUrl), data);
            }
        }

        public Task<Ratio> FindBy(Expression<Func<Ratio, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task SaveOrUpdate(Ratio entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Ratio entity)
        {
            throw new NotImplementedException();
        }
    }
}