using Remita.Services.Domains.File.Dtos;

namespace Remita.Services.Domains.FileExport;

public class ExportService : IExportService
{
    public ExportService()
    {

    }

    public Task<ExportResponse> ExportData(string email)
    {
        throw new NotImplementedException();
    }
}
