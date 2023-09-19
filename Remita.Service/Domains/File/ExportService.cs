using Remita.Models.Entities.Domians.Course;
using Remita.Services.Domains.File.Dtos;

namespace Remita.Services.Domains.File;

public class ExportService : IExportService
{
    public ExportService()
    {

    }

    public Task<ExportResponse> Export(List<StudentCourse> data)
    {
        throw new NotImplementedException();
    }
}
