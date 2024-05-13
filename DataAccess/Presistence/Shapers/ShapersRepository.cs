using DataAccess.Context;
using DataAccess.Presistence.Generic;
using Domain.Repository;
using Domain.Repository.Shapers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Presistence.Shapers
{
    public class ShapersRepository : GenericRepository<Domain.EntityModels.Shapers> , IShapersRepository
    {
        public ShapersRepository(BoardSalesDbContext boardSalesDbContext) : base(boardSalesDbContext)
        {
            
        }
    }
}
