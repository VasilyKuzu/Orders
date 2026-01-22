namespace OrderExercise.Domain
{
    public class Product
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Article { get; set; }


        public Product(string name, string article)
        {
            Name = name;
            Article = article;
        }

    }
}
