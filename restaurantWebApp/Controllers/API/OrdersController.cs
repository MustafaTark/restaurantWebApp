using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurantWebApp.Contracts;
using restaurantWebApp.Dto;
using restaurantWebApp.Models;

namespace restaurantWebApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepositoryBase<Order> _repo;

        private readonly IMapper _mapper;
        public OrdersController(IMapper mapper, IRepositoryBase<Order> repo)
        {

            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _repo.GetAllAsync();
            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return ordersDto;
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync([FromBody] OrderDto order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderEntity = _mapper.Map<Order>(order);
            var added = await _repo.CreateAsync(orderEntity);
            return CreatedAtRoute( // 201 Created
            routeName: nameof(GetOrder),
            routeValues: new { id = added.Id },
            value: added);
        }
        [HttpGet("{id}", Name = nameof(GetOrder)),]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        public async Task<IActionResult> GetOrder(int id)
        {
            var o = await _repo.GetByIdAsync(id);
            if (o == null)
            {
                return NotFound();
            }
            var order = _mapper.Map<MealDto>(o);
            return Ok(order);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, OrderDto orderDto)
        {
            if (orderDto == null || orderDto.Id != id)
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
            var order = _mapper.Map<Order>(orderDto);
            await _repo.UpadteAsync(id, order);
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
