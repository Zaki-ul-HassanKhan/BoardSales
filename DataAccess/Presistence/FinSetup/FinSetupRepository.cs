using DataAccess.Context;
using DataAccess.Presistence.Generic;
using Domain.Repository.FinSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Presistence.FinSetup
{
    public class FinSetupRepository : GenericRepository<Domain.EntityModels.FinSetup>, IFinSetupRepository
    {
        public FinSetupRepository(BoardSalesDbContext boardSalesDbContext) : base(boardSalesDbContext)
        {
            
        }
    }
}
