using CloudStorageTest.Application.UseCases.Users.UploadProfilePhoto;
using CloudStorageTest.Domain.Storage;
using CloudStorageTest.Infrastructure.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Configurando a injeção de depedência

/*Configurando a injeção de depedência*/
builder.Services.AddScoped<IUploadProfilePhotoUseCase, UploadProfilePhotoUseCase>();

/*Configurando a injeção de depedência*/
builder.Services.AddScoped<IStorageService>(options =>
{
    var clientId = builder.Configuration.GetValue<string>("CloudStorage:ClientId");
    var clientSecret = builder.Configuration.GetValue<string>("CloudStorage:ClientSecret");

    var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
    {
        ClientSecrets = new ClientSecrets
        {
            ClientId = clientId,
            ClientSecret = clientSecret
        },
        //Scopes = [Google.Apis.Drive.v3.DriveService.Scope.Drive],
        Scopes = new[] { Google.Apis.Drive.v3.DriveService.Scope.Drive },
        DataStore = new FileDataStore("GoogleDriveTest")
    });
    return new GoogleDriveStorageService(apiCodeFlow);
});



#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
