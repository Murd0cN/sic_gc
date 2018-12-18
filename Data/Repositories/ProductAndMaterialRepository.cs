using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Models;
using Microsoft.EntityFrameworkCore;

namespace Arqsi_1160752_1161361_3DF.Data.Repositories
{
    public class ProductAndMaterialRepository : IProductAndMaterialRepository
    {
        private readonly ClosetContext _context;

        public ProductAndMaterialRepository(ClosetContext context)
        {
            _context = context;
        }

        public async Task<ProductMaterialRelationship> NewProductAndMaterialRelationship(ProductMaterialRelationship productMaterialRelationship)
        {
            await _context.ProductMaterialRelationships.AddAsync(productMaterialRelationship);

            return productMaterialRelationship;
        }


        /*
            Returns the relationships associated to the product with the specified ID.
        */
        public async Task<List<ProductMaterialRelationship>> FindRelationshipsOfProduct(int productID)
        {
            return await _context.ProductMaterialRelationships.Include(m => m.Material).Where(i => i.ProductId == productID).ToListAsync();
        }

        public void DeleteWithoutSave(ProductMaterialRelationship productMaterialRelationship)
        {
            _context.ProductMaterialRelationships.Remove(productMaterialRelationship);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();

            return;
        }
    }
}