namespace Models
{
    public class Category
    {

        public int CategoryId { get; }
        public string Name { get; set; }
        public string Description { get; }

        public Category()
        {

        }

        public Category(string categoryName)
        {
            Name = categoryName;
        }

        public Category(int categoryId)
        {
            CategoryId = categoryId;
        }

        public Category(int categoryId, string categoryName, string categoryDescription)
        {
            CategoryId = categoryId;
            Name = categoryName;
            Description = categoryDescription;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
