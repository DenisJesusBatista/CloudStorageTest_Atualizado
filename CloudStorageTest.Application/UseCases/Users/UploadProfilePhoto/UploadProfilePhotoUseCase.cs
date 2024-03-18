using CloudStorageTest.Domain.Entities;
using CloudStorageTest.Domain.Storage;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;

namespace CloudStorageTest.Application.UseCases.Users.UploadProfilePhoto;

public class UploadProfilePhotoUseCase : IUploadProfilePhotoUseCase

{
    /*readonly define que apenas o construtor que passa o valor.*/
    private readonly IStorageService _storageService;

    /*Comando parar criar construtor: ctor*/
    public UploadProfilePhotoUseCase(IStorageService storageService)
    {
        _storageService = storageService;

    }


    public void Execute(IFormFile file)
    {

        var fileStream = file.OpenReadStream();

        var isImage = fileStream.Is<JointPhotographicExpertsGroup>();

        if (isImage == false)
            throw new Exception("The file is not an image.");

        var user = GetFromDatabase();

        _storageService.Upload(file, user);

    }


    private User GetFromDatabase()
    {
        return new User
        {
            Id = 1,
            Name = "Denis",
            Email = "denis.si2011@gmail.com",
            /*Gerar refleshTOken e AccessToken
             * https://developers.google.com/oauthplayground/?code=4/0AeaYSHDRAqreEBCKxltlpnl_oJV57B5EA6waLU6cy50sHcbNgoRBwLn5JtqaiYxD8nbK5w&scope=https://www.googleapis.com/auth/drive%20https://www.googleapis.com/auth/drive.appdata%20https://www.googleapis.com/auth/drive.apps.readonly%20https://www.googleapis.com/auth/drive.file%20https://www.googleapis.com/auth/drive.metadata%20https://www.googleapis.com/auth/drive.metadata.readonly%20https://www.googleapis.com/auth/drive.photos.readonly%20https://www.googleapis.com/auth/drive.readonly%20https://www.googleapis.com/auth/drive.scripts
             */
            RefleshToken = "1//04xSHA5dbKOUlCgYIARAAGAQSNwF-L9IrFA6HEwvrhbpSrtVt_1N5fwKy4oMd1GraAq7Jtdub2xfUconWW35QqizkmJo7s1rC3e0",
            AccessToken = "ya29.a0Ad52N3_4IQ0IHSYL8GFLDTzbt4MusQhlA5zsMGiUs9uWmxIguHDTN812z8-eStLOHHUy8crJQpY8luwfXjCP4dWNVEZWXmnp0JKj_iKhLhVfakE0sgr_GnvDshpKwmkH79irB3cU29jbw33mLSettzTGpFGR890nNIM3aCgYKAXQSARESFQHGX2MiO_wgUbErxatzCrTf-5KYww0171"
        };

    }
}
