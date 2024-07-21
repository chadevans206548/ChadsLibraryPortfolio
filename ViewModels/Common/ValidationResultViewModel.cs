namespace ViewModels.Common;
public class ValidationResultViewModel
{
    public ValidationResultViewModel()
    {
        this.IsValid = false;
        this.ErrorMessages = [];
    }

    public bool IsValid { get; set; }
    public List<string> ErrorMessages { get; set; }
}
