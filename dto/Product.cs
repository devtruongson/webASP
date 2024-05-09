
public class ProductDTO
{
    public int id;
    public string model;
    public string brand;
    public string capacity;
    public string thumbnail;

    public ProductDTO(int id, string model, string brand, string capacity, string thumbnail)
    {
        this.id = id;
        this.model = model;
        this.brand = brand;
        this.capacity = capacity;
        this.thumbnail = thumbnail;
    }
}

