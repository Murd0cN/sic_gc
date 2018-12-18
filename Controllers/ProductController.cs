using System.Collections.Generic;
using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Data.Factory;
using Arqsi_1160752_1161361_3DF.Data.Repositories;
using Arqsi_1160752_1161361_3DF.Dtos;
using Arqsi_1160752_1161361_3DF.Models;
using Arqsi_1160752_1161361_3DF.services;
using Microsoft.AspNetCore.Mvc;

namespace Arqsi_1160752_1161361_3DF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private const string ProductDoesNotFit = "Product does not fit the parent product";
        private const string MaterialRestrictionNotPossible = "The child product doesn't have any of the materials of the parent product.";
        private const string RelationshipAlreadyExists = "Product relationship already exists";
        private const string ParentProductNotFound = "Parent product doesn't exist";
        private const string ProductAlreadyExists = "Product already exists.";
        private const string ChildProductNotFound = "Child product doesn't exist";
        private const string CategoryDoesntExist = "Category doesn't exist.";
        private const string NoMaterialFound = "No material and finish found.";
        private const string DimensionsError = "Wrong dimensions specified.";
        private const string ItemError = "Invalid item";
        private const string ProductWithIDNotFound = "Product with specified ID not found.";
        private const string ProductWithNameNotFound = "Product with specified name not found.";
        private const string ChildrenOfProductNotFound = "No children found for the product with the specified ID.";
        private const string ProductRelationshipNotFound = "Não foi encontrada a relação entre os produtos especificados.";
        private const string ParentsOfProductNotFound = "No parents found for the product with the specified ID.";
        private const string RestrictionsOfProductNotFound = "No restrictions found for the product with the specified ID.";
        private const string InvalidNumberOfPercentageRestrictions = "Invalid number of percentage restrictions. There should have been a minimum and maximum percentage restriction for each dimension.";
        private const string InvalidPercentageRestrictions = "The child product wouldn't fit the parent product with the specified percentage restrictions.";
        private const int NumberOfDimensions = 3;
        private const int MinHeightPercentageIdx = 0;
        private const int MaxHeightPercentageIdx = 1;
        private const int MinWidthPercentageIdx = 2;
        private const int MaxWidthPercentageIdx = 3;
        private const int MinDepthPercentageIdx = 4;
        private const int MaxDepthPercentageIdx = 5;

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IProductAndMaterialRepository _productAndMaterialRepository;
        private readonly IProductRelationshipRepository _productRelationshipRepository;
        private readonly IRestrictionRepository _restrictionRepository;

        public ProductController(IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IMaterialRepository materialFinishRepository,
            IProductAndMaterialRepository productAndMaterialRepository,
            IProductRelationshipRepository productRelationshipRepository,
            IRestrictionRepository restrictionRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _materialRepository = materialFinishRepository;
            _productAndMaterialRepository = productAndMaterialRepository;
            _productRelationshipRepository = productRelationshipRepository;
            _restrictionRepository = restrictionRepository;
        }

        [HttpPost("check")]
        public async Task<IActionResult> CheckRestriction(CheckRestrictionDto main)
        {
            ItemDto parent = main.parent;
            ItemDto child = main.child;

            ProductRelationship pr = await _productRelationshipRepository.GetRelationshipByIds(parent.productId, child.productId);

            if (pr == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = RestrictionsOfProductNotFound });
            }

            if (pr.Restrictions.Count != 0)
            {
                ValidateRestriction vr = new ValidateRestriction();

                if (!vr.CheckRestriction(parent, child, pr.Restrictions))
                {
                    return NotFound(new ErrorDto { ErrorMessage = ItemError });
                }
            }

            return StatusCode(204);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProduct() {

            List<Product> list = await _productRepository.GetAllProducts();

            GetAllProductDto final = new GetAllProductDto();
            final.produtos = new List<GetProductWithNamesDto>();
            foreach(Product p in list) {
                GetProductWithNamesDto dto = await CreateGetProductWithNameDto(p);
                final.produtos.Add(dto);
            }
            return Ok(final);
        }

        [HttpGet("mandatory/{id}")]
        public async Task<IActionResult> GetMandatoryProducts(int id)
        {
            Product product = await _productRepository.FindProductById(id);

            if (product == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ProductWithIDNotFound });
            }

            List<ProductRelationship> list = await _productRelationshipRepository.GetRelationshipsOfParentById(id);
            List<int> mandatoryChildren = new List<int>();
            foreach (ProductRelationship pr in list)
            {
                if (pr.IsMandatory)
                {
                    mandatoryChildren.Add(pr.ChildProductID);
                }
            }
            return Ok(mandatoryChildren);
        }

        [HttpPost("associate")]
        public async Task<IActionResult> AssociateProducts(AssociateProductDto associateProductDto)
        {
            Product parentProduct = await _productRepository.FindProductById(associateProductDto.ParentId);

            if (parentProduct == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ParentProductNotFound });
            }

            Product childProduct = await _productRepository.FindProductById(associateProductDto.ChildId);
            if (childProduct == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ChildProductNotFound });
            }

            ProductRelationship productRelationship = await _productRelationshipRepository.GetRelationshipByIds(associateProductDto.ParentId, associateProductDto.ChildId);

            if (productRelationship != null)
            {
                return StatusCode(409, new ErrorDto { ErrorMessage = RelationshipAlreadyExists });
            }

            // create restriction for the dimensions
            IRestrictionFactory restrictionFactory = new RestrictionFactory();
            DimensionsRestriction dimensionsRestriction = restrictionFactory.CreateDimensionsRestriction(parentProduct, childProduct);
            if (dimensionsRestriction == null)
            {
                return BadRequest(new ErrorDto
                {
                    ErrorMessage = ProductDoesNotFit
                });

            }

            MaterialRestriction materialRestriction = null;
            // create restriction for the materials (if specified)
            if (associateProductDto.RestrictMaterials)
            {
                ICollection<ProductMaterialRelationship> parentMaterialRelats =
                    await _productAndMaterialRepository.FindRelationshipsOfProduct(parentProduct.ID);
                ICollection<ProductMaterialRelationship> childMaterialRelats =
                    await _productAndMaterialRepository.FindRelationshipsOfProduct(childProduct.ID);
                materialRestriction =
                    restrictionFactory.CreateMaterialRestriction(parentMaterialRelats, childMaterialRelats);
                if (materialRestriction == null)
                {
                    return BadRequest(new ErrorDto
                    {
                        ErrorMessage = MaterialRestrictionNotPossible
                    });
                }
            }

            PercentageRestriction percentageRestriction = null;
            // create percentage restrictions
            if (associateProductDto.PercentageRestrictions.Count != 0)
            {
                if (associateProductDto.PercentageRestrictions.Count != (NumberOfDimensions * 2))
                {
                    return BadRequest(new ErrorDto
                    {
                        ErrorMessage = InvalidNumberOfPercentageRestrictions
                    });
                }
                else
                {
                    NewPercentageRestrictionDto newPercentageRestrictionDto = new NewPercentageRestrictionDto
                    {
                        MinHeightPercentage = associateProductDto.PercentageRestrictions[MinHeightPercentageIdx],
                        MaxHeightPercentage = associateProductDto.PercentageRestrictions[MaxHeightPercentageIdx],
                        MinWidthPercentage = associateProductDto.PercentageRestrictions[MinWidthPercentageIdx],
                        MaxWidthPercentage = associateProductDto.PercentageRestrictions[MaxWidthPercentageIdx],
                        MinDepthPercentage = associateProductDto.PercentageRestrictions[MinDepthPercentageIdx],
                        MaxDepthPercentage = associateProductDto.PercentageRestrictions[MaxDepthPercentageIdx]
                    };
                    percentageRestriction = restrictionFactory.
                        CreatePercentageRestriction(newPercentageRestrictionDto, parentProduct, childProduct);

                    if (percentageRestriction == null)
                    {
                        return BadRequest(new ErrorDto
                        {
                            ErrorMessage = InvalidPercentageRestrictions
                        });
                    }
                }
            }

            productRelationship = new ProductRelationship
            {
                ParentProduct = parentProduct,
                ChildProduct = childProduct,
                IsMandatory = associateProductDto.IsMandatory
            };

            productRelationship.Restrictions = new HashSet<Restriction>();

            productRelationship.Restrictions.Add(dimensionsRestriction);
            if (associateProductDto.RestrictMaterials)
            {
                productRelationship.Restrictions.Add(materialRestriction);
            }
            if (percentageRestriction != null)
            {
                productRelationship.Restrictions.Add(percentageRestriction);
            }

            await _productRelationshipRepository.NewRelationship(productRelationship);

            return StatusCode(201, new GetProductAssociationDto
            {
                AssociationId = productRelationship.ID,
                ParentId = associateProductDto.ParentId,
                ChildId = associateProductDto.ChildId,
                IsMandatory = associateProductDto.IsMandatory,
                RestrictMaterials = associateProductDto.RestrictMaterials,
                PercentageRestrictions = new List<float>(associateProductDto.PercentageRestrictions)
            });
        }

        [HttpPost("names/associate")]
        public async Task<IActionResult> AssociateProductsWithName(AssociateProductsWithName associateProductDto)
        {
            Product parentProduct = await _productRepository.FindProductByName(associateProductDto.Parent);

            if (parentProduct == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ParentProductNotFound });
            }

            Product childProduct = await _productRepository.FindProductByName(associateProductDto.Child);
            if (childProduct == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ChildProductNotFound });
            }

            ProductRelationship productRelationship = await _productRelationshipRepository.GetRelationshipByIds(parentProduct.ID, childProduct.ID);

            if (productRelationship != null)
            {
                return StatusCode(409, new ErrorDto { ErrorMessage = RelationshipAlreadyExists });
            }

            // create restriction for the dimensions
            IRestrictionFactory restrictionFactory = new RestrictionFactory();
            DimensionsRestriction dimensionsRestriction = restrictionFactory.CreateDimensionsRestriction(parentProduct, childProduct);
            if (dimensionsRestriction == null)
            {
                return BadRequest(new ErrorDto
                {
                    ErrorMessage = ProductDoesNotFit
                });

            }

            MaterialRestriction materialRestriction = null;
            // create restriction for the materials (if specified)
            if (associateProductDto.RestrictMaterials)
            {
                ICollection<ProductMaterialRelationship> parentMaterialRelats =
                    await _productAndMaterialRepository.FindRelationshipsOfProduct(parentProduct.ID);
                ICollection<ProductMaterialRelationship> childMaterialRelats =
                    await _productAndMaterialRepository.FindRelationshipsOfProduct(childProduct.ID);
                materialRestriction =
                    restrictionFactory.CreateMaterialRestriction(parentMaterialRelats, childMaterialRelats);
                if (materialRestriction == null)
                {
                    return BadRequest(new ErrorDto
                    {
                        ErrorMessage = MaterialRestrictionNotPossible
                    });
                }
            }

            PercentageRestriction percentageRestriction = null;
            // create percentage restrictions
            if (associateProductDto.PercentageRestrictions.Count != 0)
            {
                if (associateProductDto.PercentageRestrictions.Count != (NumberOfDimensions * 2))
                {
                    return BadRequest(new ErrorDto
                    {
                        ErrorMessage = InvalidNumberOfPercentageRestrictions
                    });
                }
                else
                {
                    NewPercentageRestrictionDto newPercentageRestrictionDto = new NewPercentageRestrictionDto
                    {
                        MinHeightPercentage = associateProductDto.PercentageRestrictions[MinHeightPercentageIdx],
                        MaxHeightPercentage = associateProductDto.PercentageRestrictions[MaxHeightPercentageIdx],
                        MinWidthPercentage = associateProductDto.PercentageRestrictions[MinWidthPercentageIdx],
                        MaxWidthPercentage = associateProductDto.PercentageRestrictions[MaxWidthPercentageIdx],
                        MinDepthPercentage = associateProductDto.PercentageRestrictions[MinDepthPercentageIdx],
                        MaxDepthPercentage = associateProductDto.PercentageRestrictions[MaxDepthPercentageIdx]
                    };
                    percentageRestriction = restrictionFactory.
                        CreatePercentageRestriction(newPercentageRestrictionDto, parentProduct, childProduct);

                    if (percentageRestriction == null)
                    {
                        return BadRequest(new ErrorDto
                        {
                            ErrorMessage = InvalidPercentageRestrictions
                        });
                    }
                }
            }

            productRelationship = new ProductRelationship
            {
                ParentProduct = parentProduct,
                ChildProduct = childProduct,
                IsMandatory = associateProductDto.IsMandatory
            };

            productRelationship.Restrictions = new HashSet<Restriction>();

            productRelationship.Restrictions.Add(dimensionsRestriction);
            if (associateProductDto.RestrictMaterials)
            {
                productRelationship.Restrictions.Add(materialRestriction);
            }
            if (percentageRestriction != null)
            {
                productRelationship.Restrictions.Add(percentageRestriction);
            }

            await _productRelationshipRepository.NewRelationship(productRelationship);

            return StatusCode(201);
        }

        /*
            Deletes the relationship between the specified (by name) products.
        */
        [HttpDelete("names/associate")]
        public async Task<IActionResult> DeleteAssociationByName(DeleteProductAssociationByNameDto dto)
        {
            ProductRelationship relationship = await _productRelationshipRepository.GetRelationshipByNames(dto.ParentName, dto.ChildName);

            if (relationship == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ProductRelationshipNotFound });
            }

            // delete the restrictions of the relationship
            List<Restriction> restrictionsCopy = new List<Restriction>(relationship.Restrictions);
            foreach (Restriction r in restrictionsCopy)
            {
                await _restrictionRepository.DeleteRestriction(r);
            }

            //relationship.Restrictions = new List<Restriction>();

            await _productRelationshipRepository.RemoveRelationship(relationship);

            return Ok();
        }

        [HttpPut("names/associate")]
        public async Task<IActionResult> ChangeAssociationByNames(AssociateProductsWithName associateProductDto)
        {
            Product parentProduct = await _productRepository.FindProductByName(associateProductDto.Parent);

            if (parentProduct == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ParentProductNotFound });
            }

            Product childProduct = await _productRepository.FindProductByName(associateProductDto.Child);
            if (childProduct == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ChildProductNotFound });
            }

            ProductRelationship productRelationship = await _productRelationshipRepository.GetRelationshipByIds(parentProduct.ID, childProduct.ID);

            if (productRelationship == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ProductRelationshipNotFound });
            }

            IRestrictionFactory restrictionFactory = new RestrictionFactory();
            MaterialRestriction materialRestriction = null;
            // create restriction for the materials (if specified)
            if (associateProductDto.RestrictMaterials && !productRelationship.HasMaterialRestriction())
            {
                ICollection<ProductMaterialRelationship> parentMaterialRelats =
                    await _productAndMaterialRepository.FindRelationshipsOfProduct(parentProduct.ID);
                ICollection<ProductMaterialRelationship> childMaterialRelats =
                    await _productAndMaterialRepository.FindRelationshipsOfProduct(childProduct.ID);
                materialRestriction =
                    restrictionFactory.CreateMaterialRestriction(parentMaterialRelats, childMaterialRelats);
                if (materialRestriction == null)
                {
                    return BadRequest(new ErrorDto
                    {
                        ErrorMessage = MaterialRestrictionNotPossible
                    });
                }
            }

            PercentageRestriction percentageRestriction = null;
            // create percentage restrictions
            if (associateProductDto.PercentageRestrictions.Count != 0)
            {
                if (associateProductDto.PercentageRestrictions.Count != (NumberOfDimensions * 2))
                {
                    return BadRequest(new ErrorDto
                    {
                        ErrorMessage = InvalidNumberOfPercentageRestrictions
                    });
                }
                else
                {
                    NewPercentageRestrictionDto newPercentageRestrictionDto = new NewPercentageRestrictionDto
                    {
                        MinHeightPercentage = associateProductDto.PercentageRestrictions[MinHeightPercentageIdx],
                        MaxHeightPercentage = associateProductDto.PercentageRestrictions[MaxHeightPercentageIdx],
                        MinWidthPercentage = associateProductDto.PercentageRestrictions[MinWidthPercentageIdx],
                        MaxWidthPercentage = associateProductDto.PercentageRestrictions[MaxWidthPercentageIdx],
                        MinDepthPercentage = associateProductDto.PercentageRestrictions[MinDepthPercentageIdx],
                        MaxDepthPercentage = associateProductDto.PercentageRestrictions[MaxDepthPercentageIdx]
                    };
                    percentageRestriction = restrictionFactory.
                        CreatePercentageRestriction(newPercentageRestrictionDto, parentProduct, childProduct);

                    if (percentageRestriction == null)
                    {
                        return BadRequest(new ErrorDto
                        {
                            ErrorMessage = InvalidPercentageRestrictions
                        });
                    }

                    PercentageRestriction existantPercentageRestriction = productRelationship.GetPercentageRestriction();
                    if (existantPercentageRestriction != null)
                    {
                        if (existantPercentageRestriction.Equals(percentageRestriction))
                        {
                            percentageRestriction = null;
                        }
                    }
                }
            }

            productRelationship.IsMandatory = associateProductDto.IsMandatory;

            if (!associateProductDto.RestrictMaterials)
            {
                MaterialRestriction toRemove = productRelationship.GetMaterialRestriction();
                productRelationship.RemoveMaterialRestriction();

                if (toRemove != null)
                {
                    await _restrictionRepository.DeleteRestriction(toRemove);
                }
            }
            else
            {
                if (materialRestriction != null)
                {
                    MaterialRestriction toRemove = productRelationship.GetMaterialRestriction();
                    productRelationship.RemoveMaterialRestriction();
                    productRelationship.Restrictions.Add(materialRestriction);

                    if (toRemove != null)
                    {
                        await _restrictionRepository.DeleteRestriction(toRemove);
                    }
                }
            }

            if (associateProductDto.PercentageRestrictions.Count == 0)
            {
                PercentageRestriction toRemove = productRelationship.GetPercentageRestriction();
                productRelationship.RemovePercentageRestriction();

                if (toRemove != null)
                {
                    await _restrictionRepository.DeleteRestriction(toRemove);
                }
            }
            else
            {
                if (percentageRestriction != null)
                {
                    PercentageRestriction toRemove = productRelationship.GetPercentageRestriction();
                    productRelationship.RemovePercentageRestriction();
                    productRelationship.Restrictions.Add(percentageRestriction);

                    if (toRemove != null)
                    {
                        await _restrictionRepository.DeleteRestriction(toRemove);
                    }
                }
            }

            await _productRelationshipRepository.UpdateRelationship(productRelationship);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewProduct(NewProductDto newProductDto)
        {
            Product product = await _productRepository.FindProductByName(newProductDto.ProductName);

            if (product != null)
            {
                return StatusCode(409, new ErrorDto { ErrorMessage = ProductAlreadyExists });
            }

            //Create a new instance of product
            product = new Product();
            product.Name = newProductDto.ProductName;
            product.Price = newProductDto.Price;

            Category category = await _categoryRepository.FindById(newProductDto.CategoryId);

            if (category == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = CategoryDoesntExist });
            }

            product.ProductCategory = category;

            PossibleValuesFactory dimensionsFactory = new PossibleValuesFactory();

            PossibleValues heightPossibleValues = dimensionsFactory.CreateNewPossibleValuesForDimension(newProductDto.NewHeightDimensions);
            PossibleValues widthPossibleValues = dimensionsFactory.CreateNewPossibleValuesForDimension(newProductDto.NewWidthDimensions);
            PossibleValues depthPossibleValues = dimensionsFactory.CreateNewPossibleValuesForDimension(newProductDto.NewDepthDimensions);

            if (heightPossibleValues == null || widthPossibleValues == null || depthPossibleValues == null)
            {
                return BadRequest(new ErrorDto { ErrorMessage = DimensionsError });
            }

            product.PossibleDimensions = new PossibleDimensions
            {
                HeightPossibleValues = heightPossibleValues,
                WidthPossibleValues = widthPossibleValues,
                DepthPossibleValues = depthPossibleValues
            };

            await _productRepository.SaveNewProduct(product);

            //Loop throught all materials
            foreach (int id in newProductDto.Materials)
            {
                Material material = await _materialRepository.FindById(id);

                ProductMaterialRelationship productMaterialRelationship = new ProductMaterialRelationship
                {
                    Product = product,
                    Material = material
                };

                await _productAndMaterialRepository.NewProductAndMaterialRelationship(productMaterialRelationship);
            }
            await _productAndMaterialRepository.SaveChanges();

            GetProductDto dto = await CreateGetProductDto(product);

            return CreatedAtRoute("GetProduct", new { id = product.ID }, dto);
        }

        [HttpPost("names")]
        public async Task<IActionResult> CreateProductWithNames(NewProductWithNamesDto newProductDto)
        {
            Product product = await _productRepository.FindProductByName(newProductDto.ProductName);

            if (product != null)
            {
                return StatusCode(409, new ErrorDto { ErrorMessage = ProductAlreadyExists });
            }

            //Create a new instance of product
            product = new Product();
            product.Name = newProductDto.ProductName;
            product.Price = newProductDto.Price;

            Category category = await _categoryRepository.FindByName(newProductDto.categoryName);

            if (category == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = CategoryDoesntExist });
            }

            product.ProductCategory = category;

            PossibleValuesFactory dimensionsFactory = new PossibleValuesFactory();

            PossibleValues heightPossibleValues = dimensionsFactory.CreateNewPossibleValuesForDimension(newProductDto.NewHeightDimensions);
            PossibleValues widthPossibleValues = dimensionsFactory.CreateNewPossibleValuesForDimension(newProductDto.NewWidthDimensions);
            PossibleValues depthPossibleValues = dimensionsFactory.CreateNewPossibleValuesForDimension(newProductDto.NewDepthDimensions);

            if (heightPossibleValues == null || widthPossibleValues == null || depthPossibleValues == null)
            {
                return BadRequest(new ErrorDto { ErrorMessage = DimensionsError });
            }

            product.PossibleDimensions = new PossibleDimensions
            {
                HeightPossibleValues = heightPossibleValues,
                WidthPossibleValues = widthPossibleValues,
                DepthPossibleValues = depthPossibleValues
            };

            await _productRepository.SaveNewProduct(product);

            //Loop throught all materials
            foreach (string name in newProductDto.Materials)
            {
                Material material = await _materialRepository.FindMaterialByName(name);

                ProductMaterialRelationship productMaterialRelationship = new ProductMaterialRelationship
                {
                    Product = product,
                    Material = material
                };

                await _productAndMaterialRepository.NewProductAndMaterialRelationship(productMaterialRelationship);
            }
            await _productAndMaterialRepository.SaveChanges();

            GetProductDto dto = await CreateGetProductDto(product);

            return CreatedAtRoute("GetProduct", new { id = product.ID }, dto);
        }

        /*
            Finds the product with the specified ID.
        */
        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<IActionResult> FindProductByID(int id)
        {
            Product product = await _productRepository.FindProductById(id);

            if (product == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ProductWithIDNotFound });
            }

            GetProductDto dto = await CreateGetProductDto(product);

            return StatusCode(200, dto);
        }

        [HttpGet]
        public async Task<IActionResult> FindProductByName([FromQuery] string name)
        {
            Product product = await _productRepository.FindProductByName(name);

            if (product == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ProductWithNameNotFound });
            }

            GetProductWithNamesDto dto = await CreateGetProductWithNameDto(product);

            return StatusCode(200, dto);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeProduct(ChangeProductDto changeProductDto)
        {
            Product product = await _productRepository.FindProductById(changeProductDto.ProductId);

            if (product == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ProductWithIDNotFound });
            }

            if (changeProductDto.Name != null)
            {
                product.Name = changeProductDto.Name;
            }

            if (changeProductDto.Price != null)
            {
                product.Price = changeProductDto.Price.Value;
            }

            if (changeProductDto.CategoryId != null)
            {
                Category category = await _categoryRepository.FindById(changeProductDto.CategoryId.Value);

                if (category == null)
                {
                    return NotFound(new ErrorDto { ErrorMessage = CategoryDoesntExist });
                }

                product.ProductCategory = category;
            }

            PossibleValuesFactory dimensionsFactory = new PossibleValuesFactory();

            if (changeProductDto.NewHeightDimensions != null)
            {
                PossibleValues dimensionValues = dimensionsFactory.CreateNewPossibleValuesForDimension(changeProductDto.NewHeightDimensions);

                if (dimensionValues == null)
                {
                    return BadRequest(new ErrorDto { ErrorMessage = DimensionsError });
                }

                product.PossibleDimensions.HeightPossibleValues = dimensionValues;
            }

            if (changeProductDto.NewWidthDimensions != null)
            {
                PossibleValues dimensionValues = dimensionsFactory.CreateNewPossibleValuesForDimension(changeProductDto.NewWidthDimensions);

                if (dimensionValues == null)
                {
                    return BadRequest(new ErrorDto { ErrorMessage = DimensionsError });
                }

                product.PossibleDimensions.WidthPossibleValues = dimensionValues;
            }

            if (changeProductDto.NewDepthDimensions != null)
            {
                PossibleValues dimensionValues = dimensionsFactory.CreateNewPossibleValuesForDimension(changeProductDto.NewDepthDimensions);

                if (dimensionValues == null)
                {
                    return BadRequest(new ErrorDto { ErrorMessage = DimensionsError });
                }

                product.PossibleDimensions.DepthPossibleValues = dimensionValues;
            }

            if (changeProductDto.MaterialsAndFinishes != null)
            {
                //Apagar as relacoes que já existem
                List<ProductMaterialRelationship> relationships = await _productAndMaterialRepository.
                    FindRelationshipsOfProduct(changeProductDto.ProductId);

                if (relationships.Count != 0)
                {
                    foreach (ProductMaterialRelationship pr in relationships)
                    {
                        _productAndMaterialRepository.DeleteWithoutSave(pr);
                    }

                    await _productAndMaterialRepository.SaveChanges();
                }

                foreach (int id in changeProductDto.MaterialsAndFinishes)
                {
                    Material material = await _materialRepository.FindById(id);

                    ProductMaterialRelationship productMaterialRelationship = new ProductMaterialRelationship
                    {
                        Product = product,
                        Material = material
                    };

                    await _productAndMaterialRepository.NewProductAndMaterialRelationship(productMaterialRelationship);
                }
                await _productAndMaterialRepository.SaveChanges();
            }

            await _productRepository.UpdateProduct(product);

            GetProductDto dto = await CreateGetProductDto(product);

            return Ok(dto);

        }

        [HttpPut("names")]
        public async Task<IActionResult> ChangeProductWithNames([FromBody] ChangeProductWithNamesDto changeProductDto)
        {
            Product product = await _productRepository.FindProductByName(changeProductDto.Product);

            if (product == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ProductWithNameNotFound });
            }

            if (changeProductDto.Name != null)
            {
                product.Name = changeProductDto.Name;
            }

            if (changeProductDto.Price != null)
            {
                product.Price = changeProductDto.Price.Value;
            }

            if (changeProductDto.Category != null)
            {
                Category category = await _categoryRepository.FindByName(changeProductDto.Category);

                if (category == null)
                {
                    return NotFound(new ErrorDto { ErrorMessage = CategoryDoesntExist });
                }

                product.ProductCategory = category;
            }

            PossibleValuesFactory dimensionsFactory = new PossibleValuesFactory();

            if (changeProductDto.NewHeightDimensions != null)
            {
                PossibleValues dimensionValues = dimensionsFactory.CreateNewPossibleValuesForDimension(changeProductDto.NewHeightDimensions);

                if (dimensionValues == null)
                {
                    return BadRequest(new ErrorDto { ErrorMessage = DimensionsError });
                }

                product.PossibleDimensions.HeightPossibleValues = dimensionValues;
            }

            if (changeProductDto.NewWidthDimensions != null)
            {
                PossibleValues dimensionValues = dimensionsFactory.CreateNewPossibleValuesForDimension(changeProductDto.NewWidthDimensions);

                if (dimensionValues == null)
                {
                    return BadRequest(new ErrorDto { ErrorMessage = DimensionsError });
                }

                product.PossibleDimensions.WidthPossibleValues = dimensionValues;
            }

            if (changeProductDto.NewDepthDimensions != null)
            {
                PossibleValues dimensionValues = dimensionsFactory.CreateNewPossibleValuesForDimension(changeProductDto.NewDepthDimensions);

                if (dimensionValues == null)
                {
                    return BadRequest(new ErrorDto { ErrorMessage = DimensionsError });
                }

                product.PossibleDimensions.DepthPossibleValues = dimensionValues;
            }

            if (changeProductDto.MaterialsAndFinishes != null)
            {
                //Apagar as relacoes que já existem
                List<ProductMaterialRelationship> relationships = await _productAndMaterialRepository.
                    FindRelationshipsOfProduct(product.ID);

                if (relationships.Count != 0)
                {
                    foreach (ProductMaterialRelationship pr in relationships)
                    {
                        _productAndMaterialRepository.DeleteWithoutSave(pr);
                    }

                    await _productAndMaterialRepository.SaveChanges();
                }

                foreach (string id in changeProductDto.MaterialsAndFinishes)
                {
                    Material material = await _materialRepository.FindMaterialByName(id);

                    ProductMaterialRelationship productMaterialRelationship = new ProductMaterialRelationship
                    {
                        Product = product,
                        Material = material
                    };

                    await _productAndMaterialRepository.NewProductAndMaterialRelationship(productMaterialRelationship);
                }
                await _productAndMaterialRepository.SaveChanges();
            }

            await _productRepository.UpdateProduct(product);

            GetProductDto dto = await CreateGetProductDto(product);

            return Ok(dto);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            Product product = await _productRepository.FindProductById(id);

            if (product == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ProductWithIDNotFound });
            }

            // remove product relationships
            ICollection<ProductRelationship> productRelationships = await _productRelationshipRepository.GetRelationshipById(id);
            if (productRelationships != null)
            {
                //List<ProductRelationship> productRelationshipsCopy = new List<ProductRelationship>(productRelationships);
                foreach (ProductRelationship pr in productRelationships)
                {
                    // remove the restrictions of the relationship
                    List<Restriction> restrictionsCopy = new List<Restriction>(pr.Restrictions);
                    foreach (Restriction restriction in restrictionsCopy)
                    {
                        await _restrictionRepository.DeleteRestriction(restriction);
                    }

                    pr.Restrictions = new List<Restriction>();

                    await _productRelationshipRepository.RemoveRelationship(pr);
                }
            }

            // remove material relationships
            ICollection<ProductMaterialRelationship> materialRelationships = await _productAndMaterialRepository.FindRelationshipsOfProduct(id);
            if (materialRelationships != null)
            {
                foreach (ProductMaterialRelationship pmr in materialRelationships)
                {
                    _productAndMaterialRepository.DeleteWithoutSave(pmr);
                }

                await _productAndMaterialRepository.SaveChanges();
            }

            await _productRepository.DeleteProduct(product);

            return Ok();
        }

        [HttpDelete("names/{name}")]
        public async Task<IActionResult> DeleteProductWithName(string name)
        {
            Product product = await _productRepository.FindProductByName(name);

            if (product == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ProductWithIDNotFound });
            }

            // remove product relationships
            ICollection<ProductRelationship> productRelationships = await _productRelationshipRepository.GetRelationshipById(product.ID);
            if (productRelationships != null)
            {
                foreach (ProductRelationship pr in productRelationships)
                {
                    // remove the restrictions of the relationship
                    List<Restriction> restrictionsCopy = new List<Restriction>(pr.Restrictions);
                    foreach (Restriction restriction in restrictionsCopy)
                    {
                        await _restrictionRepository.DeleteRestriction(restriction);
                    }

                    pr.Restrictions = new List<Restriction>();

                    await _productRelationshipRepository.RemoveRelationship(pr);
                }
            }

            // remove material relationships
            ICollection<ProductMaterialRelationship> materialRelationships = await _productAndMaterialRepository.FindRelationshipsOfProduct(product.ID);
            if (materialRelationships != null)
            {
                foreach (ProductMaterialRelationship pmr in materialRelationships)
                {
                    _productAndMaterialRepository.DeleteWithoutSave(pmr);
                }

                await _productAndMaterialRepository.SaveChanges();
            }

            await _productRepository.DeleteProduct(product);


            return StatusCode(200, "Apagado com sucesso");
        }


        /*
            Finds the products that are parts of the product with the specified ID.
        */
        [HttpGet("{id}/parts")]
        public async Task<IActionResult> FindProductParts(int id)
        {
            List<ProductRelationship> relationships = await _productRelationshipRepository.GetRelationshipsOfParentById(id);

            if (relationships == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ChildrenOfProductNotFound });
            }

            GetProductPartsDto dto = new GetProductPartsDto
            {
                ParentID = id,
                PartsIDs = new List<int>()
            };

            foreach (ProductRelationship relat in relationships)
            {
                dto.PartsIDs.Add(relat.ChildProductID);
            }

            return StatusCode(200, dto);
        }

        /*
            Finds the products that are parents to the product with the specified ID.
        */
        [HttpGet("{id}/partOf")]
        public async Task<IActionResult> FindProductParents(int id)
        {
            List<ProductRelationship> relationships = await _productRelationshipRepository.GetRelationshipsOfChildById(id);

            if (relationships == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = ParentsOfProductNotFound });
            }

            GetParentsOfProductDto dto = new GetParentsOfProductDto
            {
                ChildID = id,
                ParentsIDs = new List<int>()
            };

            foreach (ProductRelationship relat in relationships)
            {
                dto.ParentsIDs.Add(relat.ParentProductID);
            }

            return StatusCode(200, dto);
        }

        /*
            Finds all the restrictions that implicate the product with the specified ID.
        */
        [HttpGet("{productID}/restrictions")]
        public async Task<IActionResult> FindProductRestrictions(int productID)
        {
            List<ProductRelationship> relatAsChild = await _productRelationshipRepository.GetRelationshipsOfChildById(productID);
            List<ProductRelationship> relatAsParent = await _productRelationshipRepository.GetRelationshipsOfParentById(productID);

            if (relatAsChild == null || relatAsParent == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = RestrictionsOfProductNotFound });
            }

            GetProductRestrictionsDto dto = new GetProductRestrictionsDto
            {
                ProductID = productID,
                ProductRestrictions = new List<int>()
            };

            foreach (ProductRelationship relat in relatAsChild)
            {
                foreach (Restriction restriction in relat.Restrictions)
                {
                    dto.ProductRestrictions.Add(restriction.ID);
                }
            }

            foreach (ProductRelationship relat in relatAsParent)
            {
                foreach (Restriction restriction in relat.Restrictions)
                {
                    dto.ProductRestrictions.Add(restriction.ID);
                }
            }

            return StatusCode(200, dto);
        }

        /*
            Creates a DTO for a GET request of a product.
        */
        private async Task<GetProductDto> CreateGetProductDto(Product product)
        {
            GetProductDto dto = new GetProductDto
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.ProductCategoryID,
                MaterialsAndFinishes = new List<int>()
            };

            PossibleValuesOfDimensionDto heightDto = CreateNewDimensionsDto(product.PossibleDimensions.HeightPossibleValues);
            dto.HeightPossibleValues = heightDto;

            PossibleValuesOfDimensionDto widthDto = CreateNewDimensionsDto(product.PossibleDimensions.WidthPossibleValues);
            dto.WidthPossibleValues = widthDto;

            PossibleValuesOfDimensionDto depthDto = CreateNewDimensionsDto(product.PossibleDimensions.DepthPossibleValues);
            dto.DepthPossibleValues = depthDto;

            List<ProductMaterialRelationship> productMaterialRelationships = await _productAndMaterialRepository.FindRelationshipsOfProduct(product.ID);
            if (productMaterialRelationships != null)
            {
                foreach (ProductMaterialRelationship relat in productMaterialRelationships)
                {
                    dto.MaterialsAndFinishes.Add(relat.MaterialId);
                }
            }

            return dto;
        }

        private async Task<GetProductWithNamesDto> CreateGetProductWithNameDto(Product product)
        {
            GetProductWithNamesDto dto = new GetProductWithNamesDto
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Category = product.ProductCategory.Name,
                MaterialsAndFinishes = new List<string>()
            };

            PossibleValuesOfDimensionDto heightDto = CreateNewDimensionsDto(product.PossibleDimensions.HeightPossibleValues);
            dto.HeightPossibleValues = heightDto;

            PossibleValuesOfDimensionDto widthDto = CreateNewDimensionsDto(product.PossibleDimensions.WidthPossibleValues);
            dto.WidthPossibleValues = widthDto;

            PossibleValuesOfDimensionDto depthDto = CreateNewDimensionsDto(product.PossibleDimensions.DepthPossibleValues);
            dto.DepthPossibleValues = depthDto;

            List<ProductMaterialRelationship> productMaterialRelationships = await _productAndMaterialRepository.FindRelationshipsOfProduct(product.ID);
            if (productMaterialRelationships != null)
            {
                foreach (ProductMaterialRelationship relat in productMaterialRelationships)
                {
                    dto.MaterialsAndFinishes.Add(relat.Material.MaterialName);
                }
            }

            return dto;
        }

        /*
            Creates a DTO that represents the possible values of a dimension, based in the specified object.
        */
        private PossibleValuesOfDimensionDto CreateNewDimensionsDto(PossibleValues dimensionValues)
        {
            PossibleValuesOfDimensionDto dimensionDto = new PossibleValuesOfDimensionDto
            {
                Values = new List<float>()
            };

            if (dimensionValues is ContinuousPossibleValues)
            {
                ContinuousPossibleValues continuousDimensionValues = (ContinuousPossibleValues)dimensionValues;
                dimensionDto.IsDiscrete = false;

                dimensionDto.Values.Add(continuousDimensionValues.MinValue);
                dimensionDto.Values.Add(continuousDimensionValues.MaxValue);
            }
            else
            {
                DiscretePossibleValues discreteDimensionValues = (DiscretePossibleValues)dimensionValues;
                dimensionDto.IsDiscrete = true;

                foreach (Float f in discreteDimensionValues.PossibleValues)
                {
                    dimensionDto.Values.Add(f.FloatValue);
                }
            }

            return dimensionDto;
        }
    }
}