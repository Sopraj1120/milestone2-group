namespace dvdrental.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Movies> Movies { get; set; }
    }

}
