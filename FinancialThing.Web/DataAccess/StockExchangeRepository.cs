using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Newtonsoft.Json;

namespace FinancialThing.DataAccess
{
    public class StockExchangeRepository: IStockExchangeServiceRepository
    {
        private IDataGrabber _grabber;
        private string ServiceUrl { get; set; }

        public StockExchangeRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }
        public StockExchange GetById(Guid id)
        {
            var data = JsonConvert.DeserializeObject<StockExchange>(_grabber.Grab(string.Format("{0}api/stockexchange/{1}", ServiceUrl, id)));
            return data;
        }

        public IQueryable<StockExchange> GetQuery()
        {
            var list = JsonConvert.DeserializeObject<IEnumerable<StockExchange>>(_grabber.Grab(string.Format("{0}api/stockexchange/", ServiceUrl)));
            return list.AsQueryable();
        }

        public StockExchange FindBy(Expression<Func<StockExchange, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public StockExchange Add(StockExchange entity)
        {
            throw new NotImplementedException();
        }

        public void Update(StockExchange entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(StockExchange entity)
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(StockExchange entity)
        {
            throw new NotImplementedException();
        }
    }
}