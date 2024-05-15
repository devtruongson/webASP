
public class ProductDTO
{
    public readonly int id;
    public readonly string? model;
    public readonly string? brand;
    public readonly string? capacity;
    public readonly string? thumbnail;

    public readonly string? required_age;
    public readonly int count_rented;
    public readonly int count_remaining;
    public readonly string? content_markdow;
    public readonly string? content_HTML;
    public readonly string? location_id;
    public readonly string? precent_new;
    public readonly string? price;

    public ProductDTO() { }

    public ProductDTO(int id, string model, string brand, string capacity, string thumbnail, string price)
    {
        this.id = id;
        this.model = model;
        this.brand = brand;
        this.capacity = capacity;
        this.thumbnail = thumbnail;
        this.price = price;
    }

    public ProductDTO(int id, string model, string brand, string capacity, string thumbnail, string required_age, int count_rented, int count_remaining, string content_markdow, string content_HTML, string location_id, string precent_new, string price)
    {
        this.id = id;
        this.model = model;
        this.brand = brand;
        this.capacity = capacity;
        this.thumbnail = thumbnail;
        this.required_age = required_age;
        this.count_rented = count_rented;
        this.count_remaining = count_remaining;
        this.content_markdow = content_markdow;
        this.content_HTML = content_HTML;
        this.location_id = location_id;
        this.precent_new = precent_new;
        this.price = price;
    }
}

