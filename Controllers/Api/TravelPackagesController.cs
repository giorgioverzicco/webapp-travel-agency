using Microsoft.AspNetCore.Mvc;
using webapp_travel_agency.Repositories;

namespace webapp_travel_agency.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelPackagesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TravelPackagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            string? name, int? rating, string? city, string? state, decimal? price)
        {
            var travelPackages = 
                await _unitOfWork.TravelPackage.GetByFilter(name, rating, city, state, price);
            return Ok(travelPackages);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id is null or 0)
            {
                return BadRequest();
            }

            var travelPackage = 
                await _unitOfWork.TravelPackage.GetFirstOrDefaultAsync(x => x.Id == id);

            if (travelPackage is null)
            {
                return NotFound();
            }

            return Ok(travelPackage);
        }
    }
}
