using Opgave1ClassLibrary.Models;

namespace Opgave4Rest.Repositories
{
    public class CarsRepository
    {
        private int _nextId = 0;
        private readonly List<Car> Data;
        public CarsRepository()
        {
            Data = new List<Car>
            {
                new Car {Id = _nextId++, Model = "Volvo v60", LicensePlate = "WKB4920", Price = 20000},
                new Car {Id = _nextId++, Model = "Subaru C39", LicensePlate = "PHU8219", Price = 15000},
                new Car {Id = _nextId++, Model = "Ford 260", LicensePlate = "GWJ9968", Price = 14500}
            };
        }
        public List<Car> GetAll()
        {
            if (Data.Count == 0)
            {
                throw new ArgumentNullException($"Car list data is empty");
            }
            else
            {
                return new List<Car>(Data);
            }
        }
        public Car? GetById(int id)
        {
            return Data.Find(Car => Car.Id == id);
        } 
        public Car Add(Car newCar)
        {
            newCar.Validate();
            newCar.Id = _nextId++;
            Data.Add(newCar);
            return newCar;
        }
        public Car Delete(int id)
        {
            Car carToDelete = GetById(id);
            if (carToDelete == null) return null;
            Data.Remove(carToDelete);
            return carToDelete;
        }
        public Car Update(int id, Car updatedCar)
        {
            Car carToUpdate = GetById(id);
            if (carToUpdate == null) return null;
            updatedCar.Id = id;
            carToUpdate.Model = updatedCar.Model;
            carToUpdate.LicensePlate = updatedCar.LicensePlate;
            carToUpdate.Price = updatedCar.Price;
            Data.Remove(carToUpdate);
            Data.Add(updatedCar);
            return updatedCar;
        }
    }
}
