using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapp_travel_agency.Models;
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
    }
}
