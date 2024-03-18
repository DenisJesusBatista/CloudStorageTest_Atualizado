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
            Email = "gisbella2018@gmail.com",
            /*Gerar refleshTOken e AccessToken
             * Google: https://developers.google.com/oauthplayground/?code=4/0AeaYSHDRAqreEBCKxltlpnl_oJV57B5EA6waLU6cy50sHcbNgoRBwLn5JtqaiYxD8nbK5w&scope=https://www.googleapis.com/auth/drive%20https://www.googleapis.com/auth/drive.appdata%20https://www.googleapis.com/auth/drive.apps.readonly%20https://www.googleapis.com/auth/drive.file%20https://www.googleapis.com/auth/drive.metadata%20https://www.googleapis.com/auth/drive.metadata.readonly%20https://www.googleapis.com/auth/drive.photos.readonly%20https://www.googleapis.com/auth/drive.readonly%20https://www.googleapis.com/auth/drive.scripts
             *Azure: https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.KeyVault%2Fvaults
             */
            RefleshToken = "1//04rlD8WWJmPmeCgYIARAAGAQSNwF-L9IrP-yqaEjUN3riKCFpm5SL8f0aFbmhOnuqk2Hmq3uv6TxKx_esuNdAYIrUDMspgMSkzAs",
            AccessToken = "ya29.a0Ad52N38hdc-dLyxGBvkklp2cQlVQG6AAZZRI6AMQ32Uy_yytM4y5NSECbk8v_9DEJ73JQGRehH2mUPlWEFUQSHtUJNnQQxaKJcg-9FOZxj3fkmXKxD-Rdxbi4QmrssQ9iKoMC69Vf5Z2ji-2msMFPa1UMahEg53TMBbbaCgYKAV8SARASFQHGX2MilRi-HPyZYS1jVybCWdOUlQ0171"
        };

    }
}
