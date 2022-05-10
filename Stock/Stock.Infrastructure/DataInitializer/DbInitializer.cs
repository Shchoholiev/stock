using Stock.Core.Entities;
using Stock.Infrastructure.EF.EducationalPortal.Infrastructure.EF;

namespace Stock.Infrastructure.DataInitializer
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var items = new List<Item>
            {
                new Item { Name = "Table", Unit = "Piece", Count = 12, Price = 999.99, LastAppeal = DateTime.Now },
                new Item { Name = "Chair", Unit = "Piece", Count = 42, Price = 799.99, LastAppeal = DateTime.Now },
                new Item { Name = "Bed", Unit = "Piece", Count = 19, Price = 2999.99, LastAppeal = DateTime.Now },
                new Item { Name = "Blanket", Unit = "Piece", Count = 40, Price = 109.99, LastAppeal = DateTime.Now },
                new Item { Name = "Lamp", Unit = "Piece", Count = 72, Price = 10.65, LastAppeal = DateTime.Now },
                new Item { Name = "Bananas", Unit = "Kg", Count = 150, Price = 15.8, LastAppeal = DateTime.Now },
                new Item { Name = "Laptop", Unit = "Piece", Count = 33, Price = 15659.99, LastAppeal = DateTime.Now },
                new Item { Name = "Mouse", Unit = "Piece", Count = 7, Price = 487.99, LastAppeal = DateTime.Now },
                new Item { Name = "Phone", Unit = "Piece", Count = 57, Price = 10879.99, LastAppeal = DateTime.Now },
                new Item { Name = "Tablet", Unit = "Piece", Count = 44, Price = 7879.99, LastAppeal = DateTime.Now },
                new Item { Name = "Headphones", Unit = "Piece", Count = 9, Price = 1479.99, LastAppeal = DateTime.Now },
                new Item { Name = "Milk", Unit = "Liter", Count = 20, Price = 2.5, LastAppeal = DateTime.Now },
                new Item { Name = "Water", Unit = "Liter", Count = 20000, Price = 1, LastAppeal = DateTime.Now },
                new Item { Name = "Water", Unit = "500 ml bottle", Count = 230, Price = 0.69, LastAppeal = DateTime.Now },
                new Item { Name = "Juice", Unit = "950 ml bottle", Count = 47, Price = 31.2, LastAppeal = DateTime.Now },
                new Item { Name = "Bread", Unit = "Piece", Count = 107, Price = 10.79, LastAppeal = DateTime.Now },
                new Item { Name = "Apple", Unit = "Kg", Count = 130, Price = 6.89, LastAppeal = DateTime.Now },
                new Item { Name = "Orange", Unit = "Kg", Count = 90, Price = 10.89, LastAppeal = DateTime.Now },
                new Item { Name = "Strawberry", Unit = "Kg", Count = 131, Price = 50, LastAppeal = DateTime.Now },
                new Item { Name = "Raspberry", Unit = "Kg", Count = 51, Price = 79.69, LastAppeal = DateTime.Now },
                new Item { Name = "Onion", Unit = "Kg", Count = 32, Price = 56.69, LastAppeal = DateTime.Now },
                new Item { Name = "Potato", Unit = "Kg", Count = 418, Price = 26.69, LastAppeal = DateTime.Now },
                new Item { Name = "Carrot", Unit = "Kg", Count = 220, Price = 16.69, LastAppeal = DateTime.Now },
                new Item { Name = "Cucumber", Unit = "Kg", Count = 279, Price = 19.69, LastAppeal = DateTime.Now },
                new Item { Name = "Tomato", Unit = "Kg", Count = 170, Price = 25.79, LastAppeal = DateTime.Now },
                new Item { Name = "Corn", Unit = "Ear", Count = 230, Price = 8.79, LastAppeal = DateTime.Now },
                new Item { Name = "Beet", Unit = "Kg", Count = 30, Price = 15.29, LastAppeal = DateTime.Now },
                new Item { Name = "Broccoli", Unit = "Kg", Count = 62, Price = 25.29, LastAppeal = DateTime.Now },
                new Item { Name = "Green Cabbage", Unit = "Kg", Count = 42, Price = 2.29, LastAppeal = DateTime.Now },
                new Item { Name = "Purple Cabbage", Unit = "Kg", Count = 22, Price = 5.29, LastAppeal = DateTime.Now },
                new Item { Name = "Celery", Unit = "Kg", Count = 67, Price = 15.29, LastAppeal = DateTime.Now },
                new Item { Name = "Garlic", Unit = "Kg", Count = 17, Price = 5.59, LastAppeal = DateTime.Now },
                new Item { Name = "Parsnips", Unit = "Kg", Count = 45, Price = 45.59, LastAppeal = DateTime.Now },
                new Item { Name = "Peas", Unit = "Kg", Count = 42, Price = 17.59, LastAppeal = DateTime.Now },
                new Item { Name = "Pumpkin", Unit = "Kg", Count = 111, Price = 5.59, LastAppeal = DateTime.Now },
                new Item { Name = "Radish", Unit = "Kg", Count = 51, Price = 10.59, LastAppeal = DateTime.Now },
                new Item { Name = "Bullets", Unit = "20 Pack", Count = 60, Price = 12.5, LastAppeal = DateTime.Now },
                new Item { Name = "Pillow", Unit = "Piece", Count = 15, Price = 120, LastAppeal = DateTime.Now },
                new Item { Name = "Wheat Grain", Unit = "Tn", Count = 45, Price = 5800, LastAppeal = DateTime.Now },
                new Item { Name = "Pig", Unit = "Animal", Count = 10, Price = 4350, LastAppeal = DateTime.Now },
                new Item { Name = "Pork", Unit = "Kg", Count = 500, Price = 87.99, LastAppeal = DateTime.Now },
                new Item { Name = "Cup", Unit = "Piece", Count = 3, Price = 89.99, LastAppeal = DateTime.Now },
            };

            context.Items.AddRange(items);
            context.SaveChanges();
        }
    }
}
