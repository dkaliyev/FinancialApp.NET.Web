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
    public class SectorRepository:ISectorServiceRepository
    {
        private readonly IDataGrabber _grabber;
        private string ServiceUrl { get; set; }

        public SectorRepository(IDataGrabber grabber)
        {
            _grabber = grabber;
            ServiceUrl = ConfigurationManager.AppSettings["LocalServiceUrl"];
        }

        public Sector GetById(Guid id)
        {
            var data = JsonConvert.DeserializeObject<Sector>(_grabber.Grab(string.Format("{0}api/sector/{1}", ServiceUrl, id)));
            return data;
        }

        public IQueryable<Sector> GetQuery()
        {
            var data = JsonConvert.DeserializeObject<IEnumerable<Sector>>(_grabber.Grab(string.Format("{0}api/sector/", ServiceUrl)));
            return data.AsQueryable();
        }

        public Sector FindBy(Expression<Func<Sector, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Sector Add(Sector entity)
        {
            if (entity != null)
            {
                var data = JsonConvert.SerializeObject(entity);
                var res = _grabber.Post(string.Format("{0}api/sector/", ServiceUrl), data);
                var deser = JsonConvert.DeserializeObject<Sector>(res);
                if (deser != null)
                {
                    return deser;
                }
            }
            return null;
        }

        public void Update(Sector entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Sector entity)
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(Sector entity)
        {
            throw new NotImplementedException();
        }
    }
}