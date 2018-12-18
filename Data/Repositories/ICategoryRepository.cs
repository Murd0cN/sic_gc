using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Models;

namespace Arqsi_1160752_1161361_3DF.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> FindById(int id);
        Task<Category> FindByName(string name);
        Task<Category> NewCategory(Category newCategory);
        Task<Category> UpdateCategory(Category category);
    }
}