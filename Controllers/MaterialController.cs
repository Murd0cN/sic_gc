using Arqsi_1160752_1161361_3DF.Data.Repositories;
using Arqsi_1160752_1161361_3DF.Models;
using Arqsi_1160752_1161361_3DF.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Arqsi_1160752_1161361_3DF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        /*
            Error message for when a material that should already be persisted isn't found.
        */
        private const string MaterialNotFound = "Material doesn't exist.";
        /*
            Error message for when a material that shouldn't exist is already persisted.
        */
        private const string MaterialAlreadyExists = "Material already exists.";
        /*
            Error message for when a finish that shouldn't exist is already persisted.
        */
        private const string FinishAlreadyExists = "Finish already exists.";
        /*
            Error message template for when fetching a finish by ID fails.
        */
        private const string FinishWithIdNotFound = "Finish with the following ID not found: ";
        /*
            Error message template for when a material doesn't have a finish with the specified ID.
        */
        private const string MaterialDoesntHaveFinishWithId = "The material doesn't have a finish with the following ID: ";

        private readonly IMaterialRepository _materialRepository;
        /*
            Repository of finishes.
        */
        private readonly IFinishRepository _finishRepository;

        public MaterialController(IMaterialRepository materialRepository, IFinishRepository finishRepository)
        {
            _materialRepository = materialRepository;
            _finishRepository = finishRepository;
        }

        [HttpGet("{id}", Name = "GetMaterial")]
        public async Task<IActionResult> FindMaterialById(int id)
        {
            Material material = await _materialRepository.FindById(id);

            if (material == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = MaterialNotFound });
            }

            MaterialDto materialAndFinishDto = CreateMaterialDto(material);

            return StatusCode(200, materialAndFinishDto);
        }

        [HttpGet("names/{name}")]
        public async Task<IActionResult> FindMaterialByName(string name)
        {
            Material material = await _materialRepository.FindMaterialByName(name);

            if (material == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = MaterialNotFound });
            }

            MaterialDto materialAndFinishDto = CreateMaterialDto(material);

            return StatusCode(200, materialAndFinishDto);
        }

        /*
            Deletes all of the finishes of the specified material.
        */
        public async Task DeleteFinishesOfMaterial(Material material)
        {
            ICollection<Finish> finishesOfMaterial = new List<Finish>(material.AvailableFinishes);

            foreach (Finish f in finishesOfMaterial)
            {
                material.AvailableFinishes.Remove(f);
                await _finishRepository.DeleteFinish(f);
            }
        }

        /*
            Deletes the specified material, if found. Starts by deleting all of its finishes.
        */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterialById(int id)
        {
            Material material = await _materialRepository.FindById(id);

            if (material == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = MaterialNotFound });
            }

            await DeleteFinishesOfMaterial(material);

            await _materialRepository.DeleteMaterial(material);

            return Ok();
        }

        [HttpDelete("names/{name}")]
        public async Task<IActionResult> DeleteMaterialByName(string name)
        {
            Material material = await _materialRepository.FindMaterialByName(name);

            if (material == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = MaterialNotFound });
            }

            await DeleteFinishesOfMaterial(material);

            await _materialRepository.DeleteMaterial(material);

            return Ok();
        }

        /*
            Creates and saves a new material based on the specified DTO. Fails if the material already exists.
        */
        [HttpPost]
        public async Task<IActionResult> CreateNewMaterial(NewMaterialDto newMaterialDto)
        {
            Material material = await _materialRepository.FindMaterialByName(newMaterialDto.MaterialName);

            if (material != null)
            {
                return StatusCode(409, new ErrorDto { ErrorMessage = MaterialAlreadyExists });
            }

            material = new Material
            {
                MaterialName = newMaterialDto.MaterialName,
                AvailableFinishes = new List<Finish>()
            };

            foreach (NewFinishDto fDto in newMaterialDto.Finishes)
            {
                AddNewFinishToMaterial(fDto, material);
            }

            material = await _materialRepository.NewMaterial(material);

            MaterialDto materialDto = CreateMaterialDto(material);

            return CreatedAtRoute("GetMaterial", new { id = material.ID }, materialDto);
        }

        /*
            Creates an instance of Finish based on the specified NewFinishDto and adds it to the collection of
            finishes of the specified material (if it isn't already present).
            Returns the added finish or null, if it isn't added.
        */
        private Finish AddNewFinishToMaterial(NewFinishDto newFinishDto, Material material)
        {
            Finish newFinish = new Finish
            {
                FinishName = newFinishDto.Name
            };

            if (!material.AvailableFinishes.Contains(newFinish))
            {
                material.AvailableFinishes.Add(newFinish);
            }
            else
            {
                newFinish = null;
            }

            return newFinish;
        }

        /*
            Modifies a material based on the specified DTO. Fails if the material is not found or if the material
            doesn't have one of the already existent finishes that it should keep.
        */
        [HttpPut]
        public async Task<IActionResult> UpdateMaterial(PutMaterialDto putMaterialDto)
        {
            Material material = await _materialRepository.FindById(putMaterialDto.ID);

            if (material == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = MaterialNotFound });
            }

            material.MaterialName = putMaterialDto.Name;
            ICollection<Finish> alreadyExistentFinishesToMantain = new List<Finish>();

            // Builds collection with the finishes that the material already had and that should remain after the update.
            foreach (FinishDto fDto in putMaterialDto.AlreadyExistentFinishes)
            {
                Finish finish = await _finishRepository.FindById(fDto.ID);

                if (finish == null)
                {
                    string errorString = FinishWithIdNotFound + fDto.ID.ToString();
                    return NotFound(new ErrorDto { ErrorMessage = errorString });
                }

                if (!material.AvailableFinishes.Contains(finish) || !material.HasFinishWithId(finish.ID))
                {
                    string errorString = MaterialDoesntHaveFinishWithId + finish.ID.ToString();
                    return NotFound(new ErrorDto { ErrorMessage = errorString });
                }

                if (!alreadyExistentFinishesToMantain.Contains(finish))
                {
                    finish.FinishName = fDto.Name;
                    await _finishRepository.UpdateFinish(finish);
                    alreadyExistentFinishesToMantain.Add(finish);
                }
            }

            // Deletes finishes that were in the material but that are no longer wanted
            ICollection<Finish> auxAvailableFinishes = new List<Finish>(material.AvailableFinishes);
            foreach (Finish finish in auxAvailableFinishes)
            {
                if (!alreadyExistentFinishesToMantain.Contains(finish))
                {
                    material.AvailableFinishes.Remove(finish);
                    await _finishRepository.DeleteFinish(finish);
                }
            }

            // Adds the new finishes to the material (if one of this finishes is already present, it isn't added twice)
            // Also saves the said finishes.
            foreach (NewFinishDto fDto in putMaterialDto.NewFinishes)
            {
                Finish newAddedFinish = AddNewFinishToMaterial(fDto, material);

                if (newAddedFinish != null)
                {
                    await _finishRepository.NewFinish(newAddedFinish);
                }
            }

            // updates the modified material
            material = await _materialRepository.UpdateMaterial(material);

            MaterialDto materialDto = CreateMaterialDto(material);

            return Ok(materialDto);
        }

        [HttpPut("names")]
        public async Task<IActionResult> UpdateMaterialWithName(UpdateMaterialWithNameDto putMaterialDto)
        {
            Material material = await _materialRepository.FindMaterialByName(putMaterialDto.Name);

            if (material == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = MaterialNotFound });
            }
    	
            if(putMaterialDto.Name != null)
                material.MaterialName = putMaterialDto.Name;

            ICollection<Finish> alreadyExistentFinishesToMantain = new List<Finish>();

            material.AvailableFinishes = new List<Finish>();

            // Adds the new finishes to the material (if one of this finishes is already present, it isn't added twice)
            // Also saves the said finishes.
            foreach (string fDto in putMaterialDto.NewFinishes)
            {
                NewFinishDto f = new NewFinishDto
                {
                    Name =  fDto
                };

                Finish newAddedFinish = AddNewFinishToMaterial(f, material);

                if (newAddedFinish != null)
                {
                    await _finishRepository.NewFinish(newAddedFinish);
                }
            }

            // updates the modified material
            material = await _materialRepository.UpdateMaterial(material);

            MaterialDto materialDto = CreateMaterialDto(material);

            return Ok(materialDto);
        }

        /*
            Creates a DTO for a material.
        */
        private MaterialDto CreateMaterialDto(Material material)
        {
            MaterialDto dto = new MaterialDto
            {
                ID = material.ID,
                Name = material.MaterialName,
                AvailableFinishes = new List<FinishDto>()
            };

            foreach (Finish f in material.AvailableFinishes)
            {
                dto.AvailableFinishes.Add(CreateFinishDto(f));
            }

            return dto;
        }

        /*
            Creates a DTO for a finish.
        */
        private FinishDto CreateFinishDto(Finish finish)
        {
            return new FinishDto
            {
                ID = finish.ID,
                Name = finish.FinishName
            };
        }
    }
}