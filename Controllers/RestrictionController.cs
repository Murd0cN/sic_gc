using System.Threading.Tasks;
using Arqsi_1160752_1161361_3DF.Data.Factory;
using Arqsi_1160752_1161361_3DF.Data.Repositories;
using Arqsi_1160752_1161361_3DF.Dtos;
using Arqsi_1160752_1161361_3DF.Models;
using Microsoft.AspNetCore.Mvc;

namespace Arqsi_1160752_1161361_3DF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestrictionController : ControllerBase
    {
        private const string RestrictionsNotFound = "No restriction found with the specified ID.";

        private readonly IRestrictionRepository _restrictionRepository;

        public RestrictionController(IRestrictionRepository restrictionRepository)
        {
            _restrictionRepository = restrictionRepository;
        }

        /*
           Finds the restriction with the specified ID.
        */
        [HttpGet("{id}", Name = "GetRestriction")]
        public async Task<IActionResult> FindRestrictionByID(int id)
        {
            Restriction restriction = await _restrictionRepository.FindById(id);

            if (restriction == null)
            {
                return NotFound(new ErrorDto { ErrorMessage = RestrictionsNotFound });
            }

            IRestrictionFactory restFactory = new RestrictionFactory();
            GetRestrictionDto dto = restFactory.CreatesRestrictionDto(restriction);

            return StatusCode(200, dto);
        }
    }
}