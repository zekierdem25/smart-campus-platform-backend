using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SmartCampus.API.Tests.Helpers;

public class MockFileStorageService : SmartCampus.API.Services.IFileStorageService
{
    public Task<string> UploadFileAsync(IFormFile file, string fileName)
    {
        // Test için mock URL döndür
        var mockUrl = $"/uploads/profiles/{fileName}";
        return Task.FromResult(mockUrl);
    }

    public Task DeleteFileAsync(string fileUrl)
    {
        // Test için silme işlemini simüle et (gerçekten bir şey yapmıyor)
        return Task.CompletedTask;
    }
}

