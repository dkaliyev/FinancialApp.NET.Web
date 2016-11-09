using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FinancialThing.DataAccess
{
    public class DictionaryRepository: IRepository<Dictionary, Guid>
    {
        private IDataGrabber _grabber;
        private string ServiceUrl { get; set; }

        public DictionaryRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }
        public async Task<Dictionary> GetById(Guid id)
        {
            var response = await _grabber.Get(string.Format("{0}api/dictionary/{1}", ServiceUrl, id));
            var status = JsonConvert.DeserializeObject<Status>(response);
            if(status.StatusCode == "1")
            {
                throw new Exception(status.Data);
            }
            return JsonConvert.DeserializeObject<Dictionary>(status.Data);
        }

        public async Task<IQueryable<Dictionary>> GetQuery()
        {
            var response = await _grabber.Get(string.Format("{0}api/dictionary/", ServiceUrl));
            var status = JsonConvert.DeserializeObject<Status>(response);
            if (status.StatusCode == "1")
            {
                throw new Exception(status.Data);
            }
            var list = JsonConvert.DeserializeObject<IEnumerable<Dictionary>>(status.Data);
            return list.AsQueryable();
        }

        public Task<Dictionary> Add(Dictionary entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Dictionary entity)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary> FindBy(Expression<Func<Dictionary, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task SaveOrUpdate(Dictionary entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Dictionary entity)
        {
            throw new NotImplementedException();
        }
    }
}