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
    public class CategoryEntryRepository: ICategoryEntryServiceRepository
    {
        private IDataGrabber _grabber;
        private string ServiceUrl { get; set; }

        public CategoryEntryRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }
        public CategoryEntry GetById(Guid id)
        {
            var data = JsonConvert.DeserializeObject<CategoryEntry>(_grabber.Grab(string.Format("{0}api/categoryentry/{1}", ServiceUrl, id)));
            return data;
        }

        public IQueryable<CategoryEntry> GetQuery()
        {
            var list = JsonConvert.DeserializeObject<IEnumerable<CategoryEntry>>(_grabber.Grab(string.Format("{0}api/categoryentry/", ServiceUrl)));
            return list.AsQueryable();
        }

        public CategoryEntry FindBy(Expression<Func<CategoryEntry, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public CategoryEntry Add(CategoryEntry entity)
        {
            throw new NotImplementedException();
        }

        public void Update(CategoryEntry entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(CategoryEntry entity)
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(CategoryEntry entity)
        {
            throw new NotImplementedException();
        }
    }
}