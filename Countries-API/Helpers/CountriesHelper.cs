using Countries_API.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Countries_API.Helpers
{
    public class CountriesHelper
    {
        public static async Task<byte[]> DownloadImageAsync(string directoryPath, string fileName, Uri uri)
        {
            using var httpClient = new HttpClient();

            // Get the file extension
            var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
            var fileExtension = Path.GetExtension(uriWithoutQuery);

            // Create file path and ensure directory exists
            var path = Path.Combine(directoryPath, $"{fileName}{fileExtension}");
            Directory.CreateDirectory(directoryPath);

            // Download the image and write to the file
            var imageBytes = await httpClient.GetByteArrayAsync(uri);

            //Convert.ToBase64String(imageBytes);
            await File.WriteAllBytesAsync(path, imageBytes);

            return imageBytes;
        }

        public static byte[] DownloadImage(string directoryPath, string fileName, string fileUrl)
        {
            using(WebClient webClient = new WebClient())
            {
                // Download the image and write to the file
                var imageBytes = webClient.DownloadData(fileUrl);

                // Get the file extension
                var uri = new Uri(fileUrl);
                var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
                var fileExtension = Path.GetExtension(uriWithoutQuery);

                // Create file path and ensure directory exists
                var path = Path.Combine(directoryPath, $"{fileName}{fileExtension}");
                Directory.CreateDirectory(directoryPath);

                //Write on disk
                File.WriteAllBytes(path, imageBytes);

                return imageBytes;
            }
        }

        public static void CreateFileVMResponse(string fileName, byte[] fileBytes, out FileVM file)
        {
            file = new FileVM
            {
                FileName = fileName,
                FileBase64 = Convert.ToBase64String(fileBytes),
                FileSha256 = EncodeToSHA256(fileBytes)
            };
        }

        public static string EncodeToSHA256(byte[] fileBytes)
        {
            using (SHA256 SHA256 = SHA256Managed.Create())
            {
                return BitConverter.ToString(SHA256.ComputeHash(fileBytes)).Replace("-","").ToLowerInvariant();
            }
        }
    }
}
