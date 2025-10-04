using CafeDotNet.Web.Enums;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace CafeDotNet.Web.Helpers;

public static class ControllerExtensions
{
    private const string AlertKey = nameof(AlertMessage);

    public static void SetAlert(this Controller controller, string message, AlertType type = AlertType.Info)
    {
        var alert = new AlertMessage(message, type);

        controller.TempData[AlertKey] = JsonConvert.SerializeObject(alert);
    }

    public static AlertMessage GetAlert(this Controller controller)
    {
        if (controller.TempData.TryGetValue(AlertKey, out var obj) && obj is string json)
        {
            return JsonConvert.DeserializeObject<AlertMessage>(json);
        }
       
        return null;
    }
}
