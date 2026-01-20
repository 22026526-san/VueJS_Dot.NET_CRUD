using Backend.Core.Entities;
using Backend.Core.Interfaces;
using Backend.Infrastructure.Data;

namespace Backend.Infrastructure.Repositories
{
    public class ProductRepository(DapperContext context) : BaseRepository<InsuranceProduct>(context, "insurance_product", "product_id"), IProductRepository
    {
    
    }
}