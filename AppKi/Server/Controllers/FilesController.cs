using ApiOne.Client;
using ApiOne.Client.Contracts.Requests;
using AppKi.Server.Services;
using AppKi.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace AppKi.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
    private const int MaxFileSize = 200 * 1024 * 1024;
    private readonly IApiOneService _apiOneService;
    private readonly ILogger<FilesController> _logger;

    public FilesController(IApiOneService apiOneService, ILogger<FilesController> logger)
    {
        _apiOneService = apiOneService;
        _logger = logger;
    }

    [HttpPost]
    //[DisableFormValueModelBinding]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
    [RequestSizeLimit(MaxFileSize)]
    public async Task<IActionResult> TestUpload()
    {
        var request = HttpContext.Request;

        // validation of Content-Type
        // 1. first, it must be a form-data request
        // 2. a boundary should be found in the Content-Type
        if (!request.HasFormContentType ||
            !MediaTypeHeaderValue.TryParse(request.ContentType, out var mediaTypeHeader) ||
            string.IsNullOrEmpty(mediaTypeHeader.Boundary.Value))
        {
            return new UnsupportedMediaTypeResult();
        }

        var reader = new MultipartReader(mediaTypeHeader.Boundary.Value, request.Body);
        var section = await reader.ReadNextSectionAsync();

        // This sample try to get the first file from request and save it
        // Make changes according to your needs in actual use
        while (section != null)
        {
            var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(
                section.ContentDisposition,
                out var contentDisposition);

            if (contentDisposition != null && hasContentDispositionHeader &&
                contentDisposition.DispositionType.Equals("form-data") &&
                !string.IsNullOrEmpty(contentDisposition.FileName.Value))
            {
                await _apiOneService.StoreStream(FromStream(section.Body));
                return Ok();
            }

            section = await reader.ReadNextSectionAsync();
        }

        // If the code runs to this location, it means that no files have been saved
        return BadRequest("No files data in the request.");
    }


    private async IAsyncEnumerable<SomeData> FromStream(Stream stream)
    {
        using var sr = new StreamReader(stream);
        var line = await sr.ReadLineAsync();
        var number = 0;
        
        while (!string.IsNullOrEmpty(line))
        {
            line = await sr.ReadLineAsync();
            if (string.IsNullOrEmpty(line))
                yield break;
            
            _logger.LogInformation(line);
            var data = line.Split(',');

            yield return new SomeData
            {
                Number = number++,
                FirstName = data[0],
                LastName = data[1],
                Score = int.Parse(data[2]),
                Note = data[3]
            };
        }
    }
}