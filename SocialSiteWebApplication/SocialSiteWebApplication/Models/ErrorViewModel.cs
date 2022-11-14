namespace SocialSiteWebApplication.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }
    
    public string? Message { get; set; }
    
    public string Error { get; set; } = string.Empty;

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    
    public bool ShowMessage => !string.IsNullOrEmpty(Message);
    
    public bool ShowError => !string.IsNullOrEmpty(Error);
}