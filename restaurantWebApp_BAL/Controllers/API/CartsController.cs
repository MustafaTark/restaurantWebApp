using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurantWebApp_DAL.Contracts;
using restaurantWebApp_DAL.Dto;
using restaurantWebApp_DAL.Models;

namespace restaurantWebApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IRepositoryBase<Cart> _repo;

        private readonly IMapper _mapper;
        public CartsController(IMapper mapper, IRepositoryBase<Cart> repo)
        {

            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CartDto>))]
        public async Task<IEnumerable<CartDto>> GetAllAsync()
        {
            var carts = await _repo.GetAllAsync();
            var cartsDto = _mapper.Map<IEnumerable<CartDto>>(carts);
            return cartsDto;
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync([FromBody] CartDto cart)
        {
            if (cart == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cartEntity = _mapper.Map<Cart>(cart);
            var added = await _repo.CreateAsync(cartEntity);
            return CreatedAtRoute( // 201 Created
            routeName: nameof(GetCart),
            routeValues: new { id = added.Id },
            value: added);
        }
        [HttpGet("{id}", Name = nameof(GetCart)),]
        [ProducesResponseType(200, Type = typeof(CartDto))]
        public async Task<IActionResult> GetCart(int id)
        {
            var o = await _repo.GetByIdAsync(id);
            if (o == null)
            {
                return NotFound();
            }
            var cart = _mapper.Map<CartDto>(o);
            return Ok(cart);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, CartDto cartDto)
        {
            if (cartDto == null || cartDto.Id != id)
            {
                return BadRequest(); // 400 Bad request
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }
            var cart = _mapper.Map<Cart>(cartDto);
            await _repo.UpadteAsync(id, cart);
            return new NoContentResult();

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = _repo.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }
            bool? deleted = await _repo.DeleteAsync(id);
            if (deleted.HasValue && deleted.Value) // short circuit AND
            {
                return new NoContentResult(); // 204 No content
            }
            else
            {
                return BadRequest( // 400 Bad request
                $"Order {id} was found but failed to delete.");
            }
        }
    }
}
