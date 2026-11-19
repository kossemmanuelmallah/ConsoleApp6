using System;
using System.Collections.Generic;
using System.IO;

namespace WarShipStorage
{
    public class CarFileRepository : ICarRepository
    {
        private List<Car> cars;
        private string filePath;

        public CarFileRepository(string filePath)
        {
            this.filePath = filePath;
            cars = new List<Car>();
            LoadFromFile();
        }

        public void Add(Car car)
        {
            cars.Add(car);
            SaveToFile();
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < cars.Count)
            {
                cars.RemoveAt(index);
                SaveToFile();
            }
        }

        public void UpdateAt(int index, Car car)
        {
            if (index >= 0 && index < cars.Count)
            {
                cars[index] = car;
                SaveToFile();
            }
        }

        public void Clear()
        {
            cars.Clear();
            SaveToFile();
        }

        public Car GetAt(int index)
        {
            if (index >= 0 && index < cars.Count)
                return cars[index];
            return null;
        }

        public int Count()
        {
            return cars.Count;
        }

        public void SaveToFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (var c in cars)
                    {
                        sw.WriteLine($"{c.Brand};{c.Model};{c.Year};{c.Color};{c.ImagePath}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }

        public void LoadFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length == 5)
                        {
                            cars.Add(new Car(parts[0], parts[1], int.Parse(parts[2]), parts[3], parts[4]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка загрузки: " + ex.Message);
            }
        }
    }
}
