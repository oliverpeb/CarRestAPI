using CarRestAPI.Models;
using System.Data;

namespace CarRestAPI.Repositories
{
    public class CarsRepository : ICarsRepository
    {
        private int _nextID;
        private List<Car> _cars;

        public CarsRepository()
        {
            _nextID = 1;
            _cars = new List<Car>();
            {
                new Car() { id = _nextID++, model = "Subaru", licenseplate = "123ABC", price = 240000 };
                new Car() { id = _nextID++, model = "Citroin", licenseplate = "CBA321", price = 230000 };
                new Car() { id = _nextID++, model = "Lambog", licenseplate = "ABC321", price = 220000 };
            };
        }
            
            
            
            public List<Car> GetAll(int? amount, string? modelfilter)
            {
                List<Car> result = new List<Car>(_cars);

                 if (modelfilter != null)
                 {
                     result = result.FindAll(car => car.model.Contains(modelfilter,
                    StringComparison.InvariantCultureIgnoreCase));
                    }

                if (amount != null)
                {
                int castAmount = (int)amount;
                return result.Take(castAmount).ToList();
                 }

                return result;
            }
            public Car Add(Car newCar)
             {
            newCar.Validate();
            newCar.id = _nextID++;
            _cars.Add(newCar);
            return newCar;
             }
        public Car Delete(int id)
        {
            Car foundCar = GetbyID(id);
            _cars.Remove(foundCar);
            return foundCar;
        }

        public Car? GetbyID(int id)
        {
            return _cars.Find(x => x.id == id);
        }

        public Car? Update(int id, Car update)
        {
            update.Validate();
            Car? foundCar = GetbyID(id);
            if (foundCar != null)
            {
                return null;
            }
            foundCar.model = update.model;
            foundCar.price = update.price;
            foundCar.licenseplate = update.licenseplate;
            return foundCar;
           
        }


    }
    
}
