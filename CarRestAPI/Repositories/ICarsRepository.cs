using CarRestAPI.Models;

namespace CarRestAPI.Repositories
{
    public interface ICarsRepository
    {
        Car Add(Car newCar);
        Car Delete(int id);
       // List<Car> GetAll(int? amount, string? modelfilter);
        Car? GetbyID(int id);
        List<Car> GetAll();
        Car? Update(int id, Car update);
    }
}
