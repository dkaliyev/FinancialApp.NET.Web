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
    public class IndustryRepository: IIndustryServiceRepository
    {
        private readonly IDataGrabber _grabber;
        private string ServiceUrl { get; set; }

        public IndustryRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }

        public Industry GetById(Guid id)
        {
            var data = JsonConvert.DeserializeObject<Industry>(_grabber.Grab(string.Format("{0}api/industry/{1}", ServiceUrl, id)));
            return data;
        }

        public IQueryable<Industry> GetQuery()
        {
            var data = JsonConvert.DeserializeObject<IEnumerable<Industry>>(_grabber.Grab(string.Format("{0}api/industry/", ServiceUrl)));
            return data.AsQueryable();
        }

        public Industry FindBy(Expression<Func<Industry, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Industry Add(Industry entity)
        {
            if (entity != null)
            {
                var data = JsonConvert.SerializeObject(entity);
                var res = _grabber.Post(string.Format("{0}api/industry/", ServiceUrl), data);
                var deser = JsonConvert.DeserializeObject<Industry>(res);
                if (deser != null)
                {
                    return deser;
                }
            }
            return null;
        }

        public void Update(Industry entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Industry entity)
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(Industry entity)
        {
            throw new NotImplementedException();
        }
    }
}