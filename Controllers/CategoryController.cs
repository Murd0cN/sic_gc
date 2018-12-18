using Arqsi_1160752_1161361_3DF.Data.Repositories;
using Arqsi_1160752_1161361_3DF.Models;
using Arqsi_1160752_1161361_3DF.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;

namespace Arqsi_1160752_1161361_3DF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private const string CreateWithSuccess = "Category created with success.";
        private const string CategoryWithNameDoenstExists = "Categoria não existe";
        private const string CategoryAlreadyExists = "Category already exists.";
        private const string ParentCategoryNotFound = "Parent category doensn't exist.";
        private const string CategoryWithIDNotFound = "Category with specified ID not found.";

        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("names/{name}")]
        public async Task<IActionResult> FindCategoryByName(string name)
        {
            Category category = await _categoryRepository.FindByName(name);

            if (category == null)
            {
                return StatusCode(404, new ErrorDto { ErrorMessage = CategoryWithNameDoenstExists });
            }

            CatergoryWithNamesDto categoryDto = CreateCategoryWithNamesDto(category);

            return StatusCode(200, categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCategory(NewCategoryDto newCategoryDto)
        {
            //Trys to fetch a category with the same name passed in the DTO
            Category category = await _categoryRepository.FindByName(newCategoryDto.CategoryName);
            Category parent = null;

            //If the category was found return a HTTP 400 BadRequest
            if (category != null)
            {
                return StatusCode(409, new ErrorDto { ErrorMessage = CategoryAlreadyExists });
            }

            //If the parent category name is null or equal to the new category, we can create a category without the parent information
            if (newCategoryDto.ParentCategoryName != null &&
               !String.Equals(newCategoryDto.CategoryName, newCategoryDto.ParentCategoryName, StringComparison.OrdinalIgnoreCase))
            {
                //Trys to fetch the parent category
                parent = await _categoryRepository.FindByName(newCategoryDto.ParentCategoryName);

                //If no category was found, return a HTTP 400 Bad Request
                if (parent == null)
                {
                    return NotFound(new ErrorDto { ErrorMessage = ParentCategoryNotFound });
                }
            }

            //Create the new Category
            category = new Category
            {
                Name = newCategoryDto.CategoryName,
                ParentCategory = parent
            };

            if (parent != null)
            {
                parent.ChildCategory.Add(category);
            }

            //Save the new category and returns the database instance of the category
            category = await _categoryRepository.NewCategory(category);

            CategoryDto categoryDto = CreateCategoryDto(category);

            return CreatedAtRoute("GetCategory", new { id = category.ID }, categoryDto);
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> FindCategoryByID(int id)
        {
            Category category = await _categoryRepository.FindById(id);

            if (category == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = CategoryWithIDNotFound });
            }

            CategoryDto categoryDto = CreateCategoryDto(category);

            return StatusCode(200, categoryDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(ChangeCategoryDto changeCategoryDto)
        {
            Category category = await _categoryRepository.FindById(changeCategoryDto.CategoryID);

            if (category == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = CategoryWithIDNotFound });
            }

            Category parent = await _categoryRepository.FindById(changeCategoryDto.ParentCategoryID);

            if (parent == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ParentCategoryNotFound });
            }

            //Remove current child from father
            category.ParentCategory.ChildCategory.Remove(category);

            category.ParentCategory = parent;

            category.ParentCategory.ChildCategory.Add(category);

            category = await _categoryRepository.UpdateCategory(category);

            CategoryDto returnCategory = CreateCategoryDto(category);
            return Ok(returnCategory);
        }

        [HttpPut("names")]
        public async Task<IActionResult> UpdateCategoryWithNames(ChangeCategoryWithNamesDto changeCategoryDto)
        {
            Category category = await _categoryRepository.FindByName(changeCategoryDto.category);

            if (category == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = CategoryWithNameDoenstExists });
            }

            Category parent = await _categoryRepository.FindByName(changeCategoryDto.parentCagetory);

            if (parent == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ParentCategoryNotFound });
            }

            //Remove current child from father
            if(category.ParentCategory != null)
                category.ParentCategory.ChildCategory.Remove(category);

            category.ParentCategory = parent;
            
            category.ParentCategory.ChildCategory.Add(category);

            category = await _categoryRepository.UpdateCategory(category);

            CategoryDto returnCategory = CreateCategoryDto(category);
            return Ok(returnCategory);
        }

        /*
            Creates and returns a Category Dto based on the category passed as parameter
         */
        private CategoryDto CreateCategoryDto(Category category)
        {
            CategoryDto categoryDto;
            categoryDto = new CategoryDto
            {
                CategoryID = category.ID,
                CategoryName = category.Name,
            };

            if (category.ParentCategory != null)
            {
                categoryDto.ParentCategoryID = category.ParentCategory.ID;
            }
            else
            {
                categoryDto.ParentCategoryID = category.ID;
            }

            if (category.ChildCategory != null)
            {
                categoryDto.ChildCategoriesIDs = new List<int>();
                foreach (Category c in category.ChildCategory)
                {
                    categoryDto.ChildCategoriesIDs.Add(c.ID);
                }
            }

            return categoryDto;
        }

        private CatergoryWithNamesDto CreateCategoryWithNamesDto(Category category)
        {
            CatergoryWithNamesDto categoryDto = new CatergoryWithNamesDto
            {
                CategoryID = category.ID,
                CategoryName = category.Name,
            };

            if (category.ParentCategory != null)
            {
                categoryDto.ParentCategoryName = category.ParentCategory.Name;
            }
            else
            {
                categoryDto.ParentCategoryName = category.Name;
            }

            if (category.ChildCategory != null)
            {
                categoryDto.ChildCategoriesNames = new List<string>();
                foreach (Category c in category.ChildCategory)
                {
                    categoryDto.ChildCategoriesNames.Add(c.Name);
                }
            }

            return categoryDto;
        }
    }
}