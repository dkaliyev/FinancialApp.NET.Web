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
    public class RatioValueRepository: IRepository<RatioValue, Guid>
    {
        private readonly IDataGrabber _grabber;
        private string ServiceUrl { get; set; }

        public RatioValueRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }

        public async Task<IQueryable<RatioValue>> GetQuery()
        {
            var resp = await _grabber.Get(string.Format("{0}api/ratiovalue", ServiceUrl));
            var status = JsonConvert.DeserializeObject<Status>(resp);
            var data = JsonConvert.DeserializeObject<IEnumerable<RatioValue>>(status.Data);
            return data.AsQueryable();
        }

        public async Task<RatioValue> Add(RatioValue entity)
        {
            var res = await _grabber.Post(string.Format("{0}api/ratiovalue/", ServiceUrl), "");
            return new RatioValue();
        }

        public Task Delete(RatioValue entity)
        {
            throw new NotImplementedException();
        }

        public Task<RatioValue> FindBy(Expression<Func<RatioValue, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<RatioValue> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveOrUpdate(RatioValue entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(RatioValue entity)
        {
            throw new NotImplementedException();
        }
    }
}