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
    public class RatioValueRepository: IRatioValueServiceRepository
    {
        private readonly IDataGrabber _grabber;
        private string ServiceUrl { get; set; }

        public RatioValueRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }

        public RatioValue GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RatioValue> GetQuery()
        {
            var data = JsonConvert.DeserializeObject<IEnumerable<RatioValue>>(_grabber.Grab(string.Format("{0}api/ratiovalue", ServiceUrl)));
            return data.AsQueryable();
        }

        public RatioValue FindBy(Expression<Func<RatioValue, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public RatioValue Add(RatioValue entity)
        {
            var res = _grabber.Post(string.Format("{0}api/ratiovalue/", ServiceUrl), null);
            return new RatioValue();
        }

        public void Update(RatioValue entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(RatioValue entity)
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(RatioValue entity)
        {
            throw new NotImplementedException();
        }
    }
}