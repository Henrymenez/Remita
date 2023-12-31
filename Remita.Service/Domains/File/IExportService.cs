﻿using Remita.Models.Entities.Domians.Course;
using Remita.Services.Domains.File.Dtos;

namespace Remita.Services.Domains.File;

public interface IExportService
{
    Task<ExportResponse> Export(List<StudentCourse> data);
}
