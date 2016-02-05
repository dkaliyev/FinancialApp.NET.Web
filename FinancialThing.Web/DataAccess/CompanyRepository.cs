using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Newtonsoft.Json;

namespace FinancialThing.Web.DataAccess
{
    public class CompanyRepository: ICompanyServiceRepository
    {
        private readonly IDataGrabber _grabber;
        private string ServiceUrl { get; set; }
        public CompanyRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }
        public Company GetById(Guid id)
        {
            var data = JsonConvert.DeserializeObject<Company>(_grabber.Grab(string.Format("{0}api/company/{1}", ServiceUrl, id)));
            return data;
        }

        public IQueryable<Company> GetQuery()
        {
            var data = JsonConvert.DeserializeObject<IEnumerable<Company>>(_grabber.Grab(string.Format("{0}api/company/", ServiceUrl)));
            return data.AsQueryable();
        }

        public Company FindBy(Expression<Func<Company, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Company Add(Company entity)
        {
            if (entity != null)
            {
                var data = JsonConvert.SerializeObject(entity);
                var res = _grabber.Post(string.Format("{0}api/company/", ServiceUrl), data);
                var deser = JsonConvert.DeserializeObject<Company>(res);
                if (deser != null)
                {
                    return deser;
                }
            }
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
            throw new NotImplementedException();
        }
    }
}