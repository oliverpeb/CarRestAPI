using System.Reflection.Emit;
using System.Xml.Linq;

namespace CarRestAPI.Models
{
    public class Car
    {
        //Test
        public int Id { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public string Licenseplate { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Car)) return false;
            Car cars = (Car)obj;
            if (cars.Id != Id) return false;
            if (cars.Model != Model) return false;
            if (cars.Licenseplate != Licenseplate) return false;
            if (cars.Price != Price) return false;
            return true;
        }

        public void Validateid()
        {
            if (Id == null)
                throw new ArgumentException("id cannot be null");
        }

        public void ValidateModel()
        {

            if (Model.Length < 4)
                throw new ArgumentException("The model name cannot be less than 4");

        }

        public void ValidatePrice()
        {
            if (Price < 0)
                throw new ArgumentOutOfRangeException("the price of the car cannot be less than 1" + Price);
        }

        public void ValidateLicensePlate()
        {
            if (Licenseplate.Length <= 2)
                throw new ArgumentOutOfRangeException("The licenseplate value must be between 2 and 7");
            if (Licenseplate.Length >= 7)
                throw new ArgumentOutOfRangeException("The licenseplate value must be between 2 and 7");
        }

        public void Validate()
        {
            Validateid();
            ValidateModel();
            ValidatePrice();
            ValidateLicensePlate();
        }
    }
}

