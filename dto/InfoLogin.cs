
public class InfoLoginDto
{
    public string email { get; set; }
    public string password { get; set; }


    public InfoLoginDto() { }
    public InfoLoginDto(string email, string password)
    {
        this.email = email;
        this.password = password;
    }


}

