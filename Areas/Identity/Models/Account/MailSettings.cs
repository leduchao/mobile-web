namespace MobileWeb.Areas.Identity.Models.Account;

public class MailSettings
{
    public string From { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }

    public MailSettings()
    {
        From = "haosuplo7140@gmail.com";
        Password = "pixd tojs ocml gerb";
        Host = "smtp.gmail.com";
        Port = 587;
    }

}
