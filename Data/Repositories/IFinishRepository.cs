using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Models;

namespace Arqsi_1160752_1161361_3DF.Data.Repositories
{
    public interface IFinishRepository
    {
        /*
            Fetches the finish with the specified ID.
        */
        Task<Finish> FindById(int id);

        /*
            Fetches the finish with the specified name.
        */
        Task<Finish> FindByName(string name);

        /*
            Persists the specified finish.
        */
        Task<Finish> NewFinish(Finish finish);

        /*
            Deletes the specified finish.
        */
        Task DeleteFinish(Finish finish);

        /*
            Updates the specified finish.
        */
        Task<Finish> UpdateFinish(Finish finish);
    }
}