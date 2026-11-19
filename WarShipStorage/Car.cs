namespace WarShipStorage
{
    public class Car
    {
        
        private string brand;
        private string model;
        private int year;
        private string color;
        private string imagePath;

       
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

      
        public Car(string brand, string model, int year, string color, string imagePath)
        {
            this.brand = brand;
            this.model = model;
            this.year = year;
            this.color = color;
            this.imagePath = imagePath;
        }

       
        public override string ToString()
        {
            return $"{brand} {model} ({year}) - {color}";
        }
    }
}