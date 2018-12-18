using System.Collections.Generic;
using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Models;

namespace Arqsi_1160752_1161361_3DF.Data.Repositories
{
    public interface IProductAndMaterialRepository
    {
        Task<ProductMaterialRelationship> NewProductAndMaterialRelationship(ProductMaterialRelationship productMaterialRelationship);
        Task<List<ProductMaterialRelationship>> FindRelationshipsOfProduct(int productID);
        Task SaveChanges();
        void DeleteWithoutSave(ProductMaterialRelationship productMaterialRelationship);
    }
}