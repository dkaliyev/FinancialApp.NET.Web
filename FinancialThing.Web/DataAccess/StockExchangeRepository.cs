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
    public class StockExchangeRepository: IRepository<StockExchange, Guid>
    {
        private IDataGrabber _grabber;
        private string ServiceUrl { get; set; }

        public StockExchangeRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }
        public async Task<StockExchange> GetById(Guid id)
        {
            var resp = await _grabber.Get(string.Format("{0}api/stockexchange/{1}", ServiceUrl, id));
            var status = JsonConvert.DeserializeObject<Status>(resp);
            if(status.StatusCode == "1")
            {
                throw new Exception(status.Data);
            }
            var data = JsonConvert.DeserializeObject<StockExchange>(status.Data);
            return data;
        }

        public async Task<IQueryable<StockExchange>> GetQuery()
        {
            var resp = await _grabber.Get(string.Format("{0}api/stockexchange/", ServiceUrl));
            var status = JsonConvert.DeserializeObject<Status>(resp);
            if (status.StatusCode == "1")
            {
                throw new Exception(status.Data);
            }
            var list = JsonConvert.DeserializeObject<IEnumerable<StockExchange>>(status.Data);
            return list.AsQueryable();
        }

        public Task<StockExchange> Add(StockExchange entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(StockExchange entity)
        {
            throw new NotImplementedException();
        }

        public Task<StockExchange> FindBy(Expression<Func<StockExchange, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task SaveOrUpdate(StockExchange entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(StockExchange entity)
        {
            throw new NotImplementedException();
        }
    }
}