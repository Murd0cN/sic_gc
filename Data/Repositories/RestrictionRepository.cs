using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Models;
using Microsoft.EntityFrameworkCore;

namespace Arqsi_1160752_1161361_3DF.Data.Repositories
{
    /*
        Repository classe for the Restriction class.
    */
    public class RestrictionRepository : IRestrictionRepository
    {
        /*
            Context.
        */
        private readonly ClosetContext _context;

        /*
            Constructor for the repository that receives the context as an argument.
        */
        public RestrictionRepository(ClosetContext context)
        {
            _context = context;
        }

        /*
            Finds the restriction with the specified ID.
        */
        public async Task<Restriction> FindById(int id)
        {
            var restriction = await _context.DimensionsRestrictions.FirstOrDefaultAsync(i => i.ID == id);
            if (restriction != null)
            {
                return restriction;
            }

            var restriction2 = await _context.MaterialRestrictions.FirstOrDefaultAsync(i => i.ID == id);
            if (restriction2 != null)
            {
                return restriction2;
            }

            var restriction3 = await _context.PercentageRestrictions.FirstOrDefaultAsync(i => i.ID == id);
            return restriction3;
        }

        /*
            Deletes the specified restriction.
        */
        public async Task<Restriction> DeleteRestriction(Restriction restriction)
        {
            if (restriction is DimensionsRestriction)
            {
                DimensionsRestriction dRestriction = (DimensionsRestriction)restriction;
                _context.DimensionsRestrictions.Remove(dRestriction);
            }
            else
            {
                if (restriction is MaterialRestriction)
                {
                    MaterialRestriction mRestriction = (MaterialRestriction)restriction;
                    _context.MaterialRestrictions.Remove(mRestriction);
                }
                else
                {
                    PercentageRestriction pRestriction = (PercentageRestriction)restriction;
                    _context.PercentageRestrictions.Remove(pRestriction);
                }
            }

            await _context.SaveChangesAsync();

            return restriction;
        }
    }
}