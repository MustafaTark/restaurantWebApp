using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurantWebApp_DAL.Contracts;
using restaurantWebApp_DAL.Data;
using restaurantWebApp_DAL.Dto;
using restaurantWebApp_DAL.Models;

namespace restaurantWebApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IRepositoryBase<Meal> _repo;
       
        private readonly IMapper _mapper;
       public MealsController( IMapper mapper,IRepositoryBase<Meal> repo)
        {

            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200,Type =typeof(IEnumerable<Meal>))]
        public async Task<IEnumerable<Meal>> GetAllAsync()
        {
            var meals= await _repo.GetAllAsync();
            //var mealsDto=_mapper.Map<IEnumerable<MealDto>>(meals);
            return meals;
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync ([FromBody][FromForm] MealDto meal)
        {
            if (meal == null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var mealEntity=_mapper.Map<Meal>(meal);
            var added = await _repo.CreateAsync(mealEntity);
            return CreatedAtRoute( // 201 Created
            routeName: nameof(GetMeal),
            routeValues: new { id = added.Id },
            value: added);
        }
        [HttpGet("{id}", Name = nameof(GetMeal)),]
        [ProducesResponseType(200,Type=typeof(MealDto))]
        public async Task<IActionResult> GetMeal(int id)
        {
            var m=await _repo.GetByIdAsync(id);
            if (m == null)
            {
                return NotFound();
            }
            var meal = _mapper.Map<MealDto>(m);
            return Ok(meal);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id,[FromBody]MealDto mealDto)
        {
            if (mealDto == null)
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
            var meal = _mapper.Map<Meal>(mealDto);
            await _repo.UpadteAsync(id, meal);
            return new NoContentResult();

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var existing =  _repo.GetByIdAsync(id);
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
                $"Meal {id} was found but failed to delete.");
            }
        }


    }
}
