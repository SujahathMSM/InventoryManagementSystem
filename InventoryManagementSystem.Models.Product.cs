public class Product
{
    public BindingList<Part> AssociatedParts { get; set; }
    public int ProductID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int InStock { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }
    public void AddAssociatedPart(Part part);
    public bool RemoveAssociatedPart(int id);
    public Part LookupAssociatedPart(int id);
}
