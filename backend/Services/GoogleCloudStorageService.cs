using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using System;

namespace SmartCampus.API.Services
{
    public class GoogleCloudStorageService : IFileStorageService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public GoogleCloudStorageService(IConfiguration configuration)
        {
            // Google Cloud'da çalışırken kimlik doğrulama otomatiktir.
            // Lokal geliştirme için GOOGLE_APPLICATION_CREDENTIALS ortam değişkeni ayarlanmalıdır
            // veya gcloud auth application-default login komutu çalıştırılmalıdır.
            _storageClient = StorageClient.Create();
            _bucketName = configuration["GoogleCloud:StorageBucketName"] ?? "smart-campus-uploads-480717";
        }

        public async Task<string> UploadFileAsync(IFormFile file, string fileName)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var objectName = $"profiles/{fileName}"; // Klasör yapısı

                var dataObject = await _storageClient.UploadObjectAsync(
                    _bucketName,
                    objectName,
                    file.ContentType,
                    memoryStream
                );

                // Public URL pattern: https://storage.googleapis.com/{bucket}/{object}
                // MediaLink de kullanılabilir ama bazen token içerir, en temizi public link yapısıdır.
                return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
            }
        }

        public async Task DeleteFileAsync(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl)) return;

            try 
            {
                // URL'den obje adını çıkar: https://storage.googleapis.com/bucket-name/profiles/filename.jpg
                var uri = new Uri(fileUrl);
                var path = uri.AbsolutePath.TrimStart('/'); // bucket-name/profiles/filename
                var parts = path.Split('/', 2); // [bucket-name, profiles/filename]
                
                if (parts.Length == 2)
                {
                   var objectName = parts[1];
                   await _storageClient.DeleteObjectAsync(_bucketName, objectName);
                }
            }
            catch (Exception)
            {
                // Dosya zaten yoksa veya silinemezse hata fırlatma, loglanabilir
            }
        }
    }
}
