using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Exceptions;
using Arqsi_1160752_1161361_3DF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Arqsi_1160752_1161361_3DF.Data.Repositories
{
    /*
        Repository for the material class.
    */
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ClosetContext _context;

        public MaterialRepository(ClosetContext context)
        {
            _context = context;
        }

        /*
            Deletes a given material.
         */
        public async Task DeleteMaterial(Material material)
        {
            var materialVar = _context.Materials.Remove(material);

            await _context.SaveChangesAsync();
        }

        /*
            Searchs for a material with the given ID.
        */
        public async Task<Material> FindById(int id)
        {
            var material = await _context.Materials.Include(f => f.AvailableFinishes).FirstOrDefaultAsync(x => x.ID == id);

            return material;
        }

        /*
            Searchs for a Material with a given name.
        */
        public async Task<Material> FindMaterialByName(string name)
        {
            var material = await _context.Materials.Include(f => f.AvailableFinishes).FirstOrDefaultAsync(x => x.MaterialName == name);

            return material;
        }

        /*
            Saves the new material to the context database.
        */
        public async Task<Material> NewMaterial(Material material)
        {
            await _context.Materials.AddAsync(material);
            await _context.SaveChangesAsync();

            return material;
        }

        /*
            Updates a given material.
         */
        public async Task<Material> UpdateMaterial(Material material)
        {
            _context.Update(material);

            await _context.SaveChangesAsync();

            return material;
        }
    }
}