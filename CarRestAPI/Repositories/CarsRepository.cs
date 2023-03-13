using CarRestAPI.Models;
using System.Data;

namespace CarRestAPI.Repositories
{
    public class CarsRepository
    {
        private int _nextID;
        private List<Car> _cars;

        public CarsRepository()
        {
            _nextID = 1;
            _cars = new List<Car>()
            {
             new Car() { Id = _nextID++, Model = "Subaru", Licenseplate = "123ABC", Price = 240000 },
             new Car() { Id = _nextID++, Model = "Citroin", Licenseplate = "CBA321", Price = 230000 },
             new Car() { Id = _nextID++, Model = "Lambog", Licenseplate = "ABC321", Price = 220000 }
            };
        }

        public List<Car> GetAll() 
        {
            return new List<Car>(_cars);        
        
        }
        public Car? GetbyID(int id)
        {
            return _cars.Find(x => x.Id == id);
        }

        public Car Add(Car newCar)
        {
            
            newCar.Id = _nextID++;
            _cars.Add(newCar);
            return newCar;
        }
           
            
        public Car? Delete(int id)
        {
            Car? foundCar = GetbyID(id);
            if (foundCar == null)
            {
                return null;    
            }
            _cars.Remove(foundCar);
            return foundCar;
        }

        

        public Car? Update(int id, Car update)
        {
            Car? foundCar = GetbyID(id);
            if (foundCar == null)
            {
                return null;
            }
            foundCar.Model = update.Model;
            foundCar.Price = update.Price;
            foundCar.Licenseplate = update.Licenseplate;
            return foundCar;
           
        }


    }
    
}
