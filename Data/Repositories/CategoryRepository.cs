using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Exceptions;
using Arqsi_1160752_1161361_3DF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Arqsi_1160752_1161361_3DF.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ClosetContext _context;

        public CategoryRepository(ClosetContext context)
        {
            _context = context;
        }

        /*
            Searchs for a category with the given id
        */
        public async Task<Category> FindById(int id)
        {
            var category = await _context.Categories.Include(c => c.ChildCategory).Include(c => c.ParentCategory).FirstOrDefaultAsync(i => i.ID == id);

            return category;
        }

        /*
            Searchs for a category with the given name
            Name - Name to look for
        */
        public async Task<Category> FindByName(string name)
        {
            var category = await _context.Categories.Include(c => c.ChildCategory).Include(c => c.ParentCategory).FirstOrDefaultAsync(x => x.Name == name);

            return category;
        }

        /*
            Adds a new category to the database contex
        */
        public async Task<Category> NewCategory(Category newCategory)
        {
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();

            return newCategory;
        }

        /*
            Updates a given category
        */
        public async Task<Category> UpdateCategory(Category category)
        {
            _context.Update(category);

            await _context.SaveChangesAsync();

            return category;
        }


    }
}