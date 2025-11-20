using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using CafeDotNet.Core.Gallery.DTOs;
using CafeDotNet.Manager.Galery.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeDotNet.Web.Controllers;

[Authorize]
public class GaleryController : BaseController
{
    private readonly IGaleryManager _galeryManager;

    public GaleryController(
        IGaleryManager galeryManager, 
        ILogger<HomeController> logger, 
        IHandler<DomainNotification> notifications) : base(logger, notifications)
    {
        _galeryManager = galeryManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<PartialViewResult> List()
    {
        var images = await _galeryManager.GetActiveImagesAsync();

        return PartialView("_GaleryListPartial", images);
    }

    [HttpGet]
    public PartialViewResult Upload()
    {
        return PartialView("_GaleryUploadPartial", new UploadImageRequest());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<JsonResult> Upload(UploadImageRequest request, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return Json(new { success = false, message = "Arquivo inválido." });

        request.FileName = file.FileName;
        request.ContentType = file.ContentType;
        request.Size = file.Length;

        using var stream = file.OpenReadStream();

        var result = await _galeryManager.UploadImageAsync(request, stream);

        HasErrors();
        
        return Json(new { success = result.Success, data = result });
    }

    [HttpGet]
    public async Task<PartialViewResult> Update(long id)
    {
        var image = await _galeryManager.GetImageByIdAsync(id);

        if (HasErrors())
        {
            return null;
        }

        var model = new UpdateImageRequest
        {
            Id = image.Id,
            Description = image.Description,
            Url = image.Url
        };

        return PartialView("_GaleryUploadPartial", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<JsonResult> Update(UpdateImageRequest request, IFormFile? file)
    {
        Stream? fileStream = null;

        if (file != null)
        {
            request.FileName = file.FileName;
            request.ContentType = file.ContentType;
            request.Size = file.Length;

            fileStream = file.OpenReadStream();
        }

        var result = await _galeryManager.UpdateImageAsync(request, fileStream);

        HasErrors();

        return Json(new { success = result.Success, data = result });
    }

    [HttpPost]
    public async Task<JsonResult> Delete(long id)
    {
        var result = await _galeryManager.DeleteImageAsync(id);

        HasErrors();

        return Json(new { success = result.Success });
    }
}
