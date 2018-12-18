using System.Collections.Generic;
using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Models;
using Microsoft.EntityFrameworkCore;

namespace Arqsi_1160752_1161361_3DF.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        /*
            Context.
        */
        private readonly ClosetContext _context;

        /*
            Constructor for the repository that receives the context as an argument.
        */
        public ProductRepository(ClosetContext context)
        {
            _context = context;
        }

        /*
            Finds a product of a given id
        */
        public async Task<Product> FindProductById(int id)
        {
            var product = await _context.Products.
            Include(x => x.PossibleDimensions).
            ThenInclude(x => x.HeightPossibleValues).
            ThenInclude(x => (x as DiscretePossibleValues).PossibleValues).
            Include(x => x.PossibleDimensions).
            ThenInclude(x => x.WidthPossibleValues).
            ThenInclude(x => (x as DiscretePossibleValues).PossibleValues).
            Include(x => x.PossibleDimensions).
            ThenInclude(x => x.DepthPossibleValues).
            ThenInclude(x => (x as DiscretePossibleValues).PossibleValues).
            FirstOrDefaultAsync(x => x.ID == id);

            return product;
        }

        public async Task<Product> FindProductByName(string name)
        {
            var product = await _context.Products.
            Include(x => x.ProductCategory).
            Include(x => x.PossibleDimensions).
            ThenInclude(x => x.HeightPossibleValues).
            ThenInclude(x => (x as DiscretePossibleValues).PossibleValues).
            Include(x => x.PossibleDimensions).
            ThenInclude(x => x.WidthPossibleValues).
            ThenInclude(x => (x as DiscretePossibleValues).PossibleValues).
            Include(x => x.PossibleDimensions).
            ThenInclude(x => x.DepthPossibleValues).
            ThenInclude(x => (x as DiscretePossibleValues).PossibleValues).
            FirstOrDefaultAsync(x => x.Name == name);

            return product;
        }

        /*
            Save a new product to the database
        */
        public async Task<Product> SaveNewProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> list =
            await _context.Products.
            Include(x => x.ProductCategory).
            Include(x => x.PossibleDimensions).
            ThenInclude(x => x.HeightPossibleValues).
            ThenInclude(x => (x as DiscretePossibleValues).PossibleValues).
            Include(x => x.PossibleDimensions).
            ThenInclude(x => x.WidthPossibleValues).
            ThenInclude(x => (x as DiscretePossibleValues).PossibleValues).
            Include(x => x.PossibleDimensions).
            ThenInclude(x => x.DepthPossibleValues).
            ThenInclude(x => (x as DiscretePossibleValues).PossibleValues).ToListAsync();

            return list;
        }
    }
}