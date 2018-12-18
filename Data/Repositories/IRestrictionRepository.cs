using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Models;

namespace Arqsi_1160752_1161361_3DF.Data.Repositories
{
    /*
        Repository interface for the Restriction class.
    */
    public interface IRestrictionRepository
    {
        /*
            Finds the restriction with the specified ID.
        */
        Task<Restriction> FindById(int id);
        /*
            Deletes the specified restriction.
        */
        Task<Restriction> DeleteRestriction(Restriction restriction);
    }
}