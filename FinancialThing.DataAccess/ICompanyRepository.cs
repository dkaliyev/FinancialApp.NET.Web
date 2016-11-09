using FinancialThing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.DataAccess
{
    public interface ICompanyRepository: IRepository<Company, Guid>
    {
    }
}
