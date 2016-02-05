using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Newtonsoft.Json;

namespace FinancialThing.DataAccess
{
    public class DictionaryRepository: IDictionaryServiceRepository
    {
        private IDataGrabber _grabber;
        private string ServiceUrl { get; set; }

        public DictionaryRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }
        public Dictionary GetById(Guid id)
        {
            var data = JsonConvert.DeserializeObject<Dictionary>(_grabber.Grab(string.Format("{0}api/dictionary/{1}", ServiceUrl, id)));
            return data;
        }

        public IQueryable<Dictionary> GetQuery()
        {
            var list = JsonConvert.DeserializeObject<IEnumerable<Dictionary>>(_grabber.Grab(string.Format("{0}api/dictionary/", ServiceUrl)));
            return list.AsQueryable();
        }

        public Dictionary FindBy(Expression<Func<Dictionary, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Dictionary Add(Dictionary entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Dictionary entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Dictionary entity)
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(Dictionary entity)
        {
            throw new NotImplementedException();
        }
    }
}