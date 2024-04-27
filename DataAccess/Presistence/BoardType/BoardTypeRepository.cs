using DataAccess.Context;
using DataAccess.Presistence.Generic;
using Domain.Repository.BoardType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Presistence.BoardType
{
    public class BoardTypeRepository : GenericRepository<Domain.EntityModels.BoardTypes>, IBoardTypeRepository
    {
        public BoardTypeRepository(BoardSalesDbContext boardSalesDbContext) : base(boardSalesDbContext)
        {
        }
    }
}
