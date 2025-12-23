namespace OrderExercise
{
    public class Product
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Article { get; set; }
        public decimal Price { get; set; }

        public Product(string name, string article, decimal price)
        {
            Name = name;
            Article = article;
            Price = price;
        }

    }
}
