using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SmartCampus.API.Services
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string fileName);
        Task DeleteFileAsync(string fileName);
    }
}
