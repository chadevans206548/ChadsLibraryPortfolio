namespace ViewModels.Authentication;
public class AuthenticationResponseViewModel
{
    public bool IsAuthSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
    public string? Token { get; set; }
}
