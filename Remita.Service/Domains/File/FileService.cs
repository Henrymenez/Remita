using Remita.Services.Domains.File.Dtos;

namespace Remita.Services.Domains.File;

public class FileService : IFileService
{
    public FileService()
    {

    }

    public Task<FileUploadResponse> UploadFileAsync(UploadRequest request)
    {
        throw new NotImplementedException();
    }
}
