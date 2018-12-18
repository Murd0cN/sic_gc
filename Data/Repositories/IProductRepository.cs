using System.Collections.Generic;
using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Models;

namespace Arqsi_1160752_1161361_3DF.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Product> UpdateProduct(Product product);
        Task<Product> FindProductById(int id);
        Task<Product> FindProductByName(string name);
        Task<Product> SaveNewProduct(Product product);
        Task<Product> DeleteProduct(Product product);
        Task<List<Product>> GetAllProducts();
    }
}