using CloudStorageTest.Domain.Entities;
using CloudStorageTest.Domain.Storage;
using Microsoft.AspNetCore.Http;

namespace CloudStorageTest.Infrastructure.Storage;
internal class DropboxStorageService : IStorageService
{
    public string Upload(IFormFile file, User user)
    {
        throw new NotImplementedException();
    }
}
