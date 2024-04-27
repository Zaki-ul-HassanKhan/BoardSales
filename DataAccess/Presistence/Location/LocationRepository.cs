using DataAccess.Context;
using DataAccess.Presistence.Generic;
using Domain.Repository.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Presistence.Location
{
    public class LocationRepository : GenericRepository<Domain.EntityModels.Locations>, ILocationRepository
    {
        public LocationRepository(BoardSalesDbContext boardSalesDbContext) : base(boardSalesDbContext)
        {
        }
    }
}
