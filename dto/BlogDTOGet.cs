namespace webASP.dto
{
    public class BlogDTOGet
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public string? ContentMarkdown { get; set; }
        public string? ContentHtml { get; set; }
        public int CateId { get; set; }

        public BlogDTOGet() { }

        public BlogDTOGet(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public BlogDTOGet(int id, string title, string description, bool isActive, string contentMarkdown, string contentHtml, int cateId)
        {
            Id = id;
            Title = title;
            Description = description;
            IsActive = isActive;
            ContentMarkdown = contentMarkdown;
            ContentHtml = contentHtml;
            CateId = cateId;
        }

        public BlogDTOGet(int id, string title, string description, string contentMarkdown, string contentHtml, int cateId)
        {
            Id = id;
            Title = title;
            Description = description;
            ContentMarkdown = contentMarkdown;
            ContentHtml = contentHtml;
            CateId = cateId;
        }
    }
}
