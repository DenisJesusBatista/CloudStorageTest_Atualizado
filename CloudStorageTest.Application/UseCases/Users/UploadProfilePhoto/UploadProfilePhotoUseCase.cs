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
            Email = "denis.si2011@gmail.com"
        };

    }
}
