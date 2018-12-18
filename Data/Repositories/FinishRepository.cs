using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Models;
using Microsoft.EntityFrameworkCore;

namespace Arqsi_1160752_1161361_3DF.Data.Repositories
{
    /*
        Repository for the Finish class.
    */
    public class FinishRepository : IFinishRepository
    {
        /*
            Context.
        */
        private readonly ClosetContext _context;

        /*
            Constructor for the repository.
        */
        public FinishRepository(ClosetContext context)
        {
            _context = context;
        }

        /*
            Deletes the specified finish.
        */
        public async Task DeleteFinish(Finish finish)
        {
            var f = _context.Finishes.Remove(finish);
            await _context.SaveChangesAsync();
        }

        /*
            Fetches the finish with the specified ID.
        */
        public async Task<Finish> FindById(int id)
        {
            var finish = await _context.Finishes.FirstOrDefaultAsync(x => x.ID == id);

            return finish;
        }

        /*
            Fetches the finish with the specified name.
        */
        public async Task<Finish> FindByName(string name)
        {
            return await _context.Finishes.FirstOrDefaultAsync(x => x.FinishName == name);
        }

        /*
            Persists the specified finish.
        */
        public async Task<Finish> NewFinish(Finish finish)
        {
            await _context.Finishes.AddAsync(finish);
            await _context.SaveChangesAsync();

            return finish;
        }

        /*
            Updates the specified finish.
        */
        public async Task<Finish> UpdateFinish(Finish finish)
        {
            _context.Update(finish);

            await _context.SaveChangesAsync();

            return finish;
        }
    }
}