using Remita.Services.Domains.File.Dtos;

namespace Remita.Services.Domains.FileExport;

public interface IExportService
{
    Task<ExportResponse> ExportData(string email);
}
