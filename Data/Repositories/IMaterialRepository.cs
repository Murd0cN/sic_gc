using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Models;

namespace Arqsi_1160752_1161361_3DF.Data.Repositories
{
    public interface IMaterialRepository
    {
        Task<Material> FindById(int id);
        Task<Material> FindMaterialByName(string name);
        Task<Material> NewMaterial(Material material);
        Task DeleteMaterial(Material material);
        Task<Material> UpdateMaterial(Material material);
    }
}