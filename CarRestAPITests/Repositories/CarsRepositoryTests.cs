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
            int expectedCount = 0;

            CarsRepository repos = new CarsRepository();

            List<Car> list = repos.GetAll();

            Assert.IsNotNull(list);
            Assert.AreEqual(typeof(List<Car>), list.GetType());
            Assert.AreEqual(expectedCount, list.Count());

            foreach (var Car in list)
            {
                int foundIds = list.FindAll(x => x.id == Car.id).Count(); 
                if (foundIds > 1) 
                { 
                    Assert.Fail($"ID: {Car.id} exists {foundIds} times in the list");                
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

            Car newCar = new Car { id = wrongID, model = model, licenseplate = licenseplate, price = price };

            Car addedCars = repos.Add(newCar);

            Assert.AreEqual(typeof(Car), addedCars.GetType());

            Assert.AreEqual(CorrectID, addedCars.id);
            Assert.AreEqual(model, addedCars.model);
            Assert.AreEqual(licenseplate, addedCars.licenseplate);
            Assert.AreEqual(price, addedCars.price);

            Assert.AreEqual(beforeAdd + 1, repos.GetAll().Count());

        }

        [TestMethod()]
        public void DeleteTest()
        {
            int id = 1;
            int wrongID = 99;

            CarsRepository repos = new CarsRepository();
            int beforeDelete = repos.GetAll().Count();

            Car? deleted = repos.Delete(id);
            Car? triedDeleted = repos.Delete(wrongID);

            Assert.IsNull(repos.GetbyID(id));
            Assert.AreEqual(beforeDelete - 1, repos.GetAll().Count());
            Assert.IsNull(triedDeleted);
        }

        [TestMethod()]
        public void GetbyIDTest()
        {
            int Id = 1;
            int wrongID = 99;
            string model = "Subaru";
            string licenseplate = "123ABC";
            int price = 240000;

            CarsRepository repos = new CarsRepository();

            Car? foundCar = repos.GetbyID(Id);
            Car? notFoundCar = repos.GetbyID(wrongID);


            Assert.IsNotNull(foundCar);
            Assert.AreEqual(Id, foundCar.id);
            Assert.AreEqual(model, foundCar.model);
            Assert.AreEqual(price, foundCar.price);
            Assert.AreEqual(licenseplate, foundCar.licenseplate);

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
            Car update = new Car { id = id, model = model, licenseplate = licenseplate, price = price };

            Car? updated = repos.Update(id, update);
            Car? wrongUpdated = repos.Update(wrongId, update);

            Assert.AreEqual(id, update.id);
            Assert.AreEqual(model, update.model);
            Assert.AreEqual(licenseplate, update.licenseplate);
            Assert.AreEqual(price, update.price);

            Assert.IsNull(wrongUpdated);
        }
    }
}