using webapp_travel_agency.Models;
using Microsoft.AspNetCore.Mvc;
using webapp_travel_agency.Repositories;

namespace webapp_travel_agency.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _unitOfWork.Message.GetAllAsync();
            return Ok(messages);
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id is null or 0)
            {
                return BadRequest();
            }
            
            var message = 
                await _unitOfWork.Message.GetFirstOrDefaultAsync(x => x.Id == id);

            if (message is null)
            {
                return NotFound();
            }
            
            return Ok(message);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Message message)
        {
            await _unitOfWork.Message.AddAsync(message);
            await _unitOfWork.SaveAsync();
            
            return CreatedAtAction(nameof(GetById), new { id = message.Id }, message);
        }
    }
}
