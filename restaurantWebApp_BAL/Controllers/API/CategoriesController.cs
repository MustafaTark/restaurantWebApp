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
    public class CategoriesController : ControllerBase
    {
        private readonly IRepositoryBase<Category> _repo;

        private readonly IMapper _mapper;
        public CategoriesController(IMapper mapper, IRepositoryBase<Category> repo)
        {

            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repo.GetAllAsync();
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoryDtos;
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryDto category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoryEntity = _mapper.Map<Category>(category);
            var added = await _repo.CreateAsync(categoryEntity);
            return CreatedAtRoute( // 201 Created
            routeName: nameof(GetCategory),
            routeValues: new { id = added.Id },
            value: added);
        }
        [HttpGet("{id}", Name = nameof(GetCategory)),]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        public async Task<IActionResult> GetCategory(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null)
            {
                return NotFound();
            }
            var category = _mapper.Map<CategoryDto>(c);
            return Ok(category);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, CategoryDto categoryDto)
        {
            if (categoryDto == null || categoryDto.Id != id)
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
            var category = _mapper.Map<Category>(categoryDto);
            await _repo.UpadteAsync(id, category);
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
                $"Category {id} was found but failed to delete.");
            }
        }
    }
}
