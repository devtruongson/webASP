
public class AllCodeDTO
{
    public readonly int id;
    public readonly string? type;
    public readonly string? content_title;
    public readonly string? content_detail;
    public readonly string? code;
    public AllCodeDTO() { }
    public AllCodeDTO(int id, string type, string content_title, string content_detail, string code)
    {
        this.id = id;
        this.type = type;
        this.content_title = content_title;
        this.content_detail = content_detail;
        this.code = code;
    }

}