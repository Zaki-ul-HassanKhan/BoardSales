using DataAccess.Context;
using DataAccess.Presistence.Generic;
using Domain.Repository.FinSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Presistence.FinSystem
{
    public class FinSystemRepository : GenericRepository<Domain.EntityModels.FinSystem>, IFinSystemRepository
    {
        public FinSystemRepository(BoardSalesDbContext boardSalesDbContext) : base(boardSalesDbContext)
        {
            
        }
    }
}
