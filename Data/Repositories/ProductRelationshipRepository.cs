using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Models;
using Microsoft.EntityFrameworkCore;

namespace Arqsi_1160752_1161361_3DF.Data.Repositories
{
    public class ProductRelationshipRepository : IProductRelationshipRepository
    {
        private readonly ClosetContext _context;

        public ProductRelationshipRepository(ClosetContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ProductRelationship>> GetRelationshipById(int id)
        {
            return await _context.ProductRelationships.
            Where(x => x.ParentProductID == id || x.ChildProductID == id).
            Include(x => x.Restrictions).
            ToListAsync();
        }

        public async Task<ProductRelationship> GetRelationshipByIds(int parentId, int childId)
        {
            ProductRelationship productRelationship = await _context.ProductRelationships.
            Include(x => x.Restrictions).FirstOrDefaultAsync(
                x => x.ChildProductID == childId && x.ParentProductID == parentId
            );

            return productRelationship;
        }

        /*
            Fetches the relationship that associates the parent and the child with the specified names.
        */
        public async Task<ProductRelationship> GetRelationshipByNames(string parentName, string childName)
        {
            ProductRelationship productRelationship = await _context.ProductRelationships.
            Include(x => x.Restrictions).FirstOrDefaultAsync(
                x => x.ChildProduct.Name == childName && x.ParentProduct.Name == parentName
            );

            return productRelationship;
        }

        public async Task<ProductRelationship> NewRelationship(ProductRelationship productRelationship)
        {
            await _context.ProductRelationships.AddAsync(productRelationship);
            await _context.SaveChangesAsync();

            return productRelationship;
        }

        public async Task<ProductRelationship> RemoveRelationship(ProductRelationship productRelationship)
        {
            _context.ProductRelationships.Remove(productRelationship);
            await _context.SaveChangesAsync();

            return productRelationship;
        }

        /*
            Fetches the relationships with the specified ID for the parent product.
        */
        public async Task<List<ProductRelationship>> GetRelationshipsOfParentById(int parentID)
        {
            return await _context.ProductRelationships.Include(r => r.Restrictions).Where(i => i.ParentProductID == parentID).ToListAsync();
        }

        /*
            Fetches the relationships with the specified ID for the child product.
        */
        public async Task<List<ProductRelationship>> GetRelationshipsOfChildById(int childID)
        {
            return await _context.ProductRelationships.Include(r => r.Restrictions).Where(i => i.ChildProductID == childID).ToListAsync();
        }

        /*
            Updates the specified product relationship.
        */
        public async Task<ProductRelationship> UpdateRelationship(ProductRelationship relationship)
        {
            _context.Update(relationship);

            await _context.SaveChangesAsync();

            return relationship;
        }
    }
}