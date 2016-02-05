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
    public class RatioRepository : IRatioServiceRepository
    {
        private readonly IDataGrabber _grabber;

        private string ServiceUrl { get; set; }

        public RatioRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }

        public Ratio GetById(Guid id)
        {
            var data = JsonConvert.DeserializeObject<Ratio>(_grabber.Grab(string.Format("{0}api/ratio/{1}", ServiceUrl, id)));
            return data;
        }

        public IQueryable<Ratio> GetQuery()
        {
            var data = JsonConvert.DeserializeObject<IEnumerable<Ratio>>(_grabber.Grab(string.Format("{0}api/ratio", ServiceUrl)));
            return data.AsQueryable();
        }

        public Ratio FindBy(Expression<Func<Ratio, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Ratio Add(Ratio entity)
        {
            if (entity != null)
            {
                var data = JsonConvert.SerializeObject(entity);
                var res = _grabber.Post(string.Format("{0}api/ratio/", ServiceUrl), data);
                var deser = JsonConvert.DeserializeObject<Ratio>(res);
                if (deser != null)
                {
                    return deser;
                }
            }
            return null;
        }

        public void Update(Ratio entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Ratio entity)
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(Ratio entity)
        {
            throw new NotImplementedException();
        }
    }
}