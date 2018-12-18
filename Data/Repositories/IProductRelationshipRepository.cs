using System.Collections.Generic;
using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Models;

namespace Arqsi_1160752_1161361_3DF.Data.Repositories
{
    public interface IProductRelationshipRepository
    {
        Task<ProductRelationship> GetRelationshipByIds(int parentId, int childId);
        /*
            Fetches the relationship that associates the parent and the child with the specified names.
        */
        Task<ProductRelationship> GetRelationshipByNames(string parentName, string childName);
        Task<ProductRelationship> NewRelationship(ProductRelationship productRelationship);

        Task<ICollection<ProductRelationship>> GetRelationshipById(int id);
        Task<ProductRelationship> RemoveRelationship(ProductRelationship productRelationship);

        Task<List<ProductRelationship>> GetRelationshipsOfParentById(int parentID);
        Task<List<ProductRelationship>> GetRelationshipsOfChildById(int childID);
        /*
            Updates the specified product relationship.
        */
        Task<ProductRelationship> UpdateRelationship(ProductRelationship relationship);
    }
}