using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FinancialThing.DataAccess
{
    public class SectorRepository:IRepository<Sector, Guid>
    {
        private readonly IDataGrabber _grabber;
        private string ServiceUrl { get; set; }

        public SectorRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }

        public async Task<Sector> GetById(Guid id)
        {
            var resp = await _grabber.Get(string.Format("{0}api/sector/{1}", ServiceUrl, id));
            var status = JsonConvert.DeserializeObject<Status>(resp);
            var data = JsonConvert.DeserializeObject<Sector>(status.Data);
            return data;
        }

        public async Task<IQueryable<Sector>> GetQuery()
        {
            var resp = await _grabber.Get(string.Format("{0}api/sector/", ServiceUrl));
            var status = JsonConvert.DeserializeObject<Status>(resp);
            var data = JsonConvert.DeserializeObject<IEnumerable<Sector>>(status.Data);
            return data.AsQueryable();
        }

        public async Task<Sector> Add(Sector entity)
        {
            if (entity != null)
            {
                var data = JsonConvert.SerializeObject(entity);
                var res = await _grabber.Post(string.Format("{0}api/sector/", ServiceUrl), data);
                var status = JsonConvert.DeserializeObject<Status>(res);
                if (status.StatusCode != "0")
                {
                    throw new Exception(status.Data);
                }
            }
            return null;
        }

        public Task Delete(Sector entity)
        {
            throw new NotImplementedException();
        }

        public Task<Sector> FindBy(Expression<Func<Sector, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task SaveOrUpdate(Sector entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Sector entity)
        {
            throw new NotImplementedException();
        }
    }
}