
public class BlogDTO
{
    public readonly int id;
    public readonly string? title;
    public readonly string? description;
    public readonly bool? is_active;
    public readonly string? content_MarkDown;
    public readonly string? content_HTML;
    public readonly int cate_id;
    public BlogDTO() { }
    public BlogDTO(int id, string title, string description, bool is_active, string content_MarkDown, string content_HTML, int cate_id)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.is_active = is_active;
        this.content_MarkDown = content_MarkDown;
        this.content_HTML = content_HTML;
        this.cate_id = cate_id;
    }

}