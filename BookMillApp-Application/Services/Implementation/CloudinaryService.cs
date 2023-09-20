using BookMillApp_Application.Services.Abstraction;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PaperFineryApp_Shared;

namespace BookMillApp_Application.Services.Implementation
{
    public class CloudinaryService:ICloudinaryService
    { 
    private readonly IConfiguration _config;
    public CloudinaryService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<StandardResponse<string>> UploadImage(IEnumerable<IFormFile> images)
    {
        string apiKey = _config["CloudinarySettings:ApiKey"];
        string apiSecret = _config["CloudinarySettings:ApiSecret"];
        string cloudName = _config["CloudinarySettings:CloudName"];
        var cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
        cloudinary.Api.Secure = true;

        var pictureSize = Convert.ToInt64(_config["PhotoSettings:Size"]);
        var listOfImageExtensions = new List<string>() { ".jpg", ".png", ".jpeg" };

        var uploadResults = new List<UploadResult>();
        var imagePathList = new List<string>();
        foreach (var image in images)
        {
            if (image.Length > pictureSize)
            {
                throw new ArgumentException("File size exceeded");
            }

            var pictureFormat = false;
            foreach (var item in listOfImageExtensions)
            {
                if ((image.FileName.ToLower().EndsWith(item)))
                {
                    pictureFormat = true;
                    break;
                }
            }
            if (pictureFormat == false)
            {
                throw new ArgumentException("File format not supported");
            }

            var uploadResult = new ImageUploadResult();
            using (var imageStream = image.OpenReadStream())
            {
                string filename = image.FileName;
                uploadResult = await cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription(filename, imageStream),
                    PublicId = "Joyce_Papermill/" + filename,
                    Transformation = new Transformation().Crop("thumb").Gravity("face").Width(150)
                });
            }
            _ = uploadResult.PublicId;
            uploadResults.Add(uploadResult);
            imagePathList.Add(uploadResult.Url.ToString());
        }
        var itemImagePaths = string.Join(",", imagePathList);
        return new StandardResponse<string> { Succeeded = true, Message = "file upload operation successful", Data = itemImagePaths };
    }
}
}
