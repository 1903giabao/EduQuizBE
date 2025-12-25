using AutoMapper;
using DocxTemplater;
using DocxTemplater.Images;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Application.Services;
using EduQuiz.Infrastructure.UnitOfWork;
using EduQuiz.Share.Extensions;
using static EduQuiz.Share.Enums.Enum;

namespace EduQuiz.Application.UseCases.Teacher
{
    public class PrintAcademicReportUseCase : IUseCase<PrintAcademicReportUseCaseInput, PrintAcademicReportUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITemplateService _templateService;

        public PrintAcademicReportUseCase(IUnitOfWork unitOfWork, IMapper mapper, ITemplateService templateService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _templateService = templateService;
        }

        public async Task<PrintAcademicReportUseCaseOutput> HandleAsync(PrintAcademicReportUseCaseInput useCaseInput)
        {
            string groupCode = GroupCodeEnum.ACADEMIC_REPORT.ToString();
            var executePath = Path.Combine(
                    AppContext.BaseDirectory,
                    "Templates",
                    groupCode
                );

            if (!Directory.Exists(executePath))
            {
                throw new KeyNotFoundException($"Execute path not found");
            }

            string defaultFile = Directory.GetFiles(executePath, $"{groupCode}_default.docx").FirstOrDefault();

            if (defaultFile == null)
            {
                throw new KeyNotFoundException($"Default file not found");
            }

            byte[] fileBytes = await File.ReadAllBytesAsync(defaultFile);

            if (fileBytes == null)
            {
                throw new KeyNotFoundException($"Error when convert to byte[]");
            }

            var listModel = _templateService.BindAcademicReportModel(groupCode, useCaseInput.StudentReports);

            foreach (object model in listModel)
            {
                using var inputStream = new MemoryStream(fileBytes);
                var docTemplate = new DocxTemplate(inputStream, new ProcessSettings()
                {
                    IgnoreLineBreaksAroundTags = false
                });
                docTemplate.RegisterFormatter(new ImageFormatter());
                docTemplate.BindModel("data", model);

                await using var result = docTemplate.Process();
                docTemplate.Validate();

                // 1️⃣ Save DOCX to temp file
                var reportModel = (AcademicReportBindingModel)model;

                var safeName = string.Join("_",
                    reportModel.StudentName.Split(Path.GetInvalidFileNameChars())
                );

                var tempDir = Path.Combine(Path.GetTempPath(), "EduQuizReports");
                Directory.CreateDirectory(tempDir);

                var docxPath = Path.Combine(
                    tempDir,
                    $"BCHT - ANH 6 - {safeName}.docx"
                );

                await using (var fs = new FileStream(docxPath, FileMode.Create, FileAccess.Write))
                {
                    result.Position = 0;
                    await result.CopyToAsync(fs);
                }

                foreach (var file in Directory.GetFiles(tempDir, "*.pdf"))
                {
                    File.Delete(file);
                }

                // 2️⃣ Convert DOCX → PDF
                LibreOfficeConverter.ConvertDocxToPdf(docxPath, tempDir);

                // 3️⃣ Move PDF to final destination
                var pdfPath = Path.Combine(
                    @"D:\CV\PetProject\EduProject\template\BCHT",
                    $"BCHT - ANH 6 - {safeName}.pdf"
                );

                // Wait a bit for LibreOffice to finish writing file
                await Task.Delay(500);

                // Find the generated PDF
                var generatedPdf = Directory
                    .GetFiles(tempDir, "*.pdf")
                    .OrderByDescending(File.GetLastWriteTime)
                    .FirstOrDefault();

                if (generatedPdf == null)
                {
                    throw new FileNotFoundException("LibreOffice did not generate PDF");
                }

                // Move to final destination
                File.Move(generatedPdf, pdfPath, overwrite: true);

                // 4️⃣ Cleanup temp DOCX
                File.Delete(docxPath);
            }

            return new PrintAcademicReportUseCaseOutput();
        }
    }
}
