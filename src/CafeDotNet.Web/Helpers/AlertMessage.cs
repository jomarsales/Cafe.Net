using CafeDotNet.Web.Enums;

namespace CafeDotNet.Web.Helpers;

public class AlertMessage
{
    public string Message { get; }
    public AlertType Type { get; }

    public AlertMessage(string message, AlertType type)
    {
        this.Message = message;
        this.Type = type;
    }

    public string CssClass => Type switch
    {
        AlertType.Success => "alert alert-success",
        AlertType.Danger => "alert alert-danger",
        AlertType.Info => "alert alert-info",
        AlertType.Warning => "alert alert-warning",
        _ => "alert alert-secondary"
    };
}