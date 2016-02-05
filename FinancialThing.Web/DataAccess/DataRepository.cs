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
    public class DataRepository: IDataServiceRepository    
    {
        private IDataGrabber _grabber;
        private string ServiceUrl { get; set; }

        public DataRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }

        public Company GetById(Guid id)
        {
            var data = JsonConvert.DeserializeObject<Company>(_grabber.Grab(string.Format("{0}api/data/{1}", ServiceUrl, id)));
            return data;
        }

        public IQueryable<Company> GetQuery()
        {
            var list = JsonConvert.DeserializeObject<IEnumerable<Company>>(_grabber.Grab(string.Format("{0}api/data/", ServiceUrl)));
            return list.AsQueryable();
        }

        public Company FindBy(Expression<Func<Company, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Company Add(Company entity)
        {
            _grabber.Put(string.Format("{0}api/data/", ServiceUrl), "");
            return null;
        }

        public void Update(Company entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Company entity)
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(Company entity)
        {
            _grabber.Put(string.Format("{0}api/data/{1}", ServiceUrl, entity.Id), "");
        }
    }
}