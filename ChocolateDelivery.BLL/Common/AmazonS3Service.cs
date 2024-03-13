using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ChocolateDelivery.BLL;

public static class AmazonS3Service
{
    private static AmazonS3Client _s3Client;
    private static string _bucket;
    public const string CdnUrl = "https://chocopedia.s3.me-central-1.amazonaws.com/";
    static AmazonS3Service()
    {
        _bucket = "chocopedia";
        _s3Client = new AmazonS3Client("AKIA6GBMBSBQU5HLWEFR", "dz5Q6nAenYeYESF7HQ/0Q/kKV6o38OKRG5dei/oE", Amazon.RegionEndpoint.MECentral1);

    }
    public static async Task<string> UploadToS3(IFormFile file, string subDirectory = "", string fileName = "")
    {
        var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var request = new PutObjectRequest
        {
            BucketName = $"{_bucket}{(string.IsNullOrEmpty(subDirectory) ? "" : $"/{subDirectory}")}",
            Key = uniqueFileName,
            InputStream = file.OpenReadStream(),
            ContentType = file.ContentType
        };

        var response = await _s3Client.PutObjectAsync(request);
        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            throw new Exception("Upload failed");
        }

        return uniqueFileName;
    }

    public static async Task<bool> DeleteFromS3(string subDirectory, string fileName = "")
    {
        var response = await _s3Client.DeleteObjectAsync(new DeleteObjectRequest
        {
            BucketName = _bucket,
            Key = $"{subDirectory}/{fileName}"
        });

        return response.HttpStatusCode == HttpStatusCode.OK;
    }
} 