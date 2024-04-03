using DataAccess.Context;
using DataAccess.Presistence.Generic;
using Domain.Repository.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Presistence.Product
{
    public class ProductRepository : GenericRepository<Domain.EntityModels.Product>, IProductRepository
    {
        public ProductRepository(BoardSalesDbContext boardSalesDbContext) : base(boardSalesDbContext)
        {
            
        }
    }
}
