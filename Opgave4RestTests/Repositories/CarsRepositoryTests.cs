using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opgave4Rest.Repositories;
using Opgave1ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opgave4Rest.Repositories.Tests
{
    [TestClass()]
    public class CarsRepositoryTests
    {
        Car testAddCar = new Car { Id = 4, LicensePlate = "GQN221", Model = "FFiesta", Price = 12000 };
        CarsRepository repo = new CarsRepository();
        [TestMethod()]
        //checks carsRepo for getall call and that it throws a null exception when empty
        public void CarsRepositoryTest()
        {
            Assert.IsNotNull(repo);
            List<Car> cars = repo.GetAll();
            foreach (Car car in cars)
            {
                repo.Delete(car.Id);
            }
            Assert.ThrowsException<ArgumentNullException>(() => repo.GetAll());
        }

        //asserts that returned value car is not null
        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.IsTrue(repo.GetById(1) != null);
        }
        //Adds a car to repo and checks if cars.count returned are higher after add call.
        [TestMethod()]
        public void AddTest()
        {
            List<Car> cars1 = repo.GetAll();
            repo.Add(testAddCar);
            List<Car> cars2 = repo.GetAll();
            Assert.IsTrue(cars1.Count < cars2.Count);
        }
        //first deletes all cars in repo. Then asserts repo is empty by checking exceptioncall
        [TestMethod()]
        public void DeleteTest()
        {
            List<Car> cars = repo.GetAll();
            foreach (Car car in cars)
            {
                repo.Delete(car.Id);
            }
            Assert.ThrowsException<ArgumentNullException>(() => repo.GetAll());
        }
        //Not testing updatemethod
    }
}