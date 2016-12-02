using FinancialThing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FinancialThing.Utilities;
using System.Configuration;
using Newtonsoft.Json;

namespace FinancialThing.DataAccess
{
    public class ExpandedCompanyRepository : IRepository<ExpandedCompany, Guid>
    {
        private readonly IDataGrabber _grabber;
        private string ServiceUrl { get; set; }
        public ExpandedCompanyRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }

        public Task<ExpandedCompany> Add(ExpandedCompany entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(ExpandedCompany entity)
        {
            throw new NotImplementedException();
        }

        public Task<ExpandedCompany> FindBy(Expression<Func<ExpandedCompany, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<ExpandedCompany> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<ExpandedCompany>> GetQuery()
        {
            var resp = await _grabber.Get(string.Format("{0}api/data/", ServiceUrl));

            var data = FTJsonSerializer<Response<IEnumerable<ExpandedCompany>>>.Deserialize(resp);
            //var data = JsonConvert.DeserializeObject<Response<IEnumerable<ExpandedCompany>>>(resp);

            if (data.Status == 1)
            {
                throw new Exception(data.Message);
            }
            //var data = JsonConvert.DeserializeObject<IEnumerable<ExpandedCompany>>(status.Data);
            return data.Data.AsQueryable();
        }

        public Task SaveOrUpdate(ExpandedCompany entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(ExpandedCompany entity)
        {
            throw new NotImplementedException();
        }
    }
}