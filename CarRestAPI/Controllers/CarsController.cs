using CarRestAPI.Models;
using CarRestAPI.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace CarRestAPI.Controllers
{ 
     
[Route("api/[controller]")]
[ApiController]

    public class CarsController : ControllerBase
    {
        private ICarsRepository _repository;

        public CarsController(ICarsRepository repository)
        {
            _repository = repository;
        }



        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetAll(
            [FromHeader] int? amount,
            [FromQuery] string? modelfilter)
            
        {
            List<Car> result = _repository.GetAll(amount, modelfilter);
            if (result.Count < 1)
            {
                return NoContent(); // NotFound er også ok
            }
            Response.Headers.Add("TotalAmount", "" + result.Count());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public Car Get(int id)
        {
            Car? foundCar = _repository.GetbyID(id);
            return foundCar;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Car> Post([FromBody] Car newCar)
        {
            try
            {

                Car createdCar = _repository.Add(newCar);
                return Created($"api/cars/" + createdCar.id, createdCar);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);

            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public ActionResult<Car> Put(int id, [FromBody] Car update)
        {
            try
            {
                Car? foundCar = _repository.Update(id, update);
                if (foundCar == null)
                {
                    return NotFound();
                }
                return Ok(foundCar);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);

            }


        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public ActionResult<Car> Delete(int? id)
        {


            if (id == null)
            {
                return NotFound(id);
            }
            return Ok(id);
        }




    }
}
