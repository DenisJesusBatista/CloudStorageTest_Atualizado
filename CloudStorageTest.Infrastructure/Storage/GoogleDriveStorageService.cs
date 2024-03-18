using CloudStorageTest.Domain.Entities;
using CloudStorageTest.Domain.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Http;

namespace CloudStorageTest.Infrastructure.Storage;
public class GoogleDriveStorageService : IStorageService
{
    /*Fazendo a injeção de depedência*/
    private readonly GoogleAuthorizationCodeFlow _authorization;

    public GoogleDriveStorageService(GoogleAuthorizationCodeFlow authorization)
    {
        /*Garantir que sempre vai instânciar o servico, pasando _authorization como injeção de depedência*/
        _authorization = authorization;
    }

    public string Upload(IFormFile file, User user)
    {
        var credential = new UserCredential(_authorization, user.Email, new TokenResponse
        {
            AccessToken = user.AccessToken,
            RefreshToken = user.RefleshToken

        });

        var service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer
        {
            ApplicationName = "GoogleDriveTest",
            HttpClientInitializer = credential
        });

        var driveFile = new Google.Apis.Drive.v3.Data.File
        {
            Name = file.Name,
            MimeType = file.ContentType,
            //Pasta do googledriver: Parents

        };

        var command = service.Files.Create(driveFile, file.OpenReadStream(), file.ContentType);
        command.Fields = "id"; /*Esse comando garante que o id do retorno seja preenchido*/

        var response = command.Upload();

        if (response.Status is not Google.Apis.Upload.UploadStatus.Completed /*or Google.Apis.Upload.UploadStatus.NotStarted*/)
            throw response.Exception;

        return command.ResponseBody.Id;


    }
}
