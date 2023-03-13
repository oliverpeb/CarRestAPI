using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRestAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Xml.Linq;
using CarRestAPI.Models;
using System.ComponentModel;

namespace CarRestAPI.Repositories.Tests
{
    [TestClass()]
    public class CarsRepositoryTests
    {
        

        [TestMethod()]
        public void GetAllTest()
        {
            int expectedCount = 3;

            CarsRepository repos = new CarsRepository();

            List<Car> list = repos.GetAll();

            Assert.IsNotNull(list);
            Assert.AreEqual(typeof(List<Car>), list.GetType());
            Assert.AreEqual(expectedCount, list.Count());

            foreach (var Car in list)
            {
                int foundIds = list.FindAll(x => x.Id == Car.Id).Count(); 
                if (foundIds > 1) 
                { 
                    Assert.Fail($"ID: {Car.Id} exists {foundIds} times in the list");                
                }
            }

        }

        [TestMethod()]
        public void AddTest()
        {
            int wrongID = 33;
            int CorrectID = 4;
            string model = "Subaru";
            string licenseplate = "123ABC";
            int price = 240000;

            CarsRepository repos = new CarsRepository();
            int beforeAdd = repos.GetAll().Count();

            Car newCar = new Car { Id = wrongID, Model = model, Licenseplate = licenseplate, Price = price };

            Car addedCars = repos.Add(newCar);

            Assert.AreEqual(typeof(Car), addedCars.GetType());

            Assert.AreEqual(CorrectID, addedCars.Id);
            Assert.AreEqual(model, addedCars.Model);
            Assert.AreEqual(licenseplate, addedCars.Licenseplate);
            Assert.AreEqual(price, addedCars.Price);

            Assert.AreEqual(beforeAdd + 1, repos.GetAll().Count());

        }

        [TestMethod()]
        public void DeleteTest()
        {
            int id = 1;
            int wrongID = 25;

            CarsRepository repos = new CarsRepository();
            int beforeDelete = repos.GetAll().Count();

            Car? deleted = repos.Delete(id);
            Car? triedDelete = repos.Delete(wrongID);
           
            // Assert
            Assert.IsNull(repos.GetbyID(id));
            Assert.AreEqual(beforeDelete - 1, repos.GetAll().Count());
            Assert.IsNull(triedDelete);
        }

        [TestMethod()]
        public void GetbyIDTest()
        {
            int id = 1;
            int wrongID = 999;
            string model = "Subaru";
            string licenseplate = "123ABC";
            int price = 240000;

            CarsRepository repos = new CarsRepository();

            Car? foundCar = repos.GetbyID(id);
            Car? notFoundCar = repos.GetbyID(wrongID);


            Assert.IsNotNull(foundCar);
            Assert.AreEqual(id, foundCar.Id);
            Assert.AreEqual(model, foundCar.Model);
            Assert.AreEqual(price, foundCar.Price);
            Assert.AreEqual(licenseplate, foundCar.Licenseplate);

            Assert.IsNull(notFoundCar);
        }
    

        [TestMethod()]
        public void UpdateTest()
        {
            int id = 1;
            int wrongId = 99;
            string model = "Subaru";
            string licenseplate = "123ABC";
            int price = 240000;

            CarsRepository repos = new CarsRepository();
            Car update = new Car { Id = id, Model = model, Licenseplate = licenseplate, Price = price };

            Car? updated = repos.Update(id, update);
            Car? wrongUpdated = repos.Update(wrongId, update);

            Assert.AreEqual(id, update.Id);
            Assert.AreEqual(model, update.Model);
            Assert.AreEqual(licenseplate, update.Licenseplate);
            Assert.AreEqual(price, update.Price);

            Assert.IsNull(wrongUpdated);
        }
    }
}