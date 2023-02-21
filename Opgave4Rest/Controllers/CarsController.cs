using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Opgave1ClassLibrary.Models;
using Opgave4Rest.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Opgave4Rest.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarsRepository _carsRepository;
        public CarsController(CarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        // GET: api/<CarsController>
        [EnableCors("AllowAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get()
        {
            List<Car> cars = _carsRepository.GetAll();
            if (cars == null) return NotFound("couldn't find any cars");
            return Ok(cars);
        }

        // GET api/<CarsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Car> Get(int id)
        {
            Car car = _carsRepository.GetById(id);
            if (car == null) return NotFound("No car found with id:"+ id);
            return Ok(car);
        }

        // POST api/<CarsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Car> Post([FromBody] Car car)
        {
            try
            {
                Car createdCar = _carsRepository.Add(car);
                return Created($"api/Cars/{createdCar.Id}", createdCar);
            }
            catch (ArgumentNullException n)
            {
                return BadRequest(n.Message);
            }
            catch (ArgumentOutOfRangeException n)
            {
                return BadRequest(n.Message);
            }
            catch (ArgumentException n)
            {
                return BadRequest(n.Message);
            }
        }

        // PUT api/<CarsController>/5
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        [HttpPut("{id}")]
        public ActionResult<Car> Put(int id, [FromBody] Car value)
        {
            try
            {
                Car updatedCar = _carsRepository.Update(id, value);
                return Created($"/api/Cars/{updatedCar.Id}", updatedCar);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Car was null");
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<CarsController>/5
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Car> Delete(int id)
        {
            Car deletedCar = _carsRepository.Delete(id);
            if (deletedCar == null) return NotFound("No car deleted. No car found with id: "+ id);
            return Accepted(deletedCar);
        }
    }
}
