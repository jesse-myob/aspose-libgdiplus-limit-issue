using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Microsoft.Extensions.Configuration;

namespace libgdiplus_limit
{
    class Program
    {
        static void Main(string[] args)
        {
            var asposeLicense = GetConfig("ASPOSE_LICENSE").Value;
            var testFile = GetConfig("DIRECTORY").GetSection("SOURCE").Value;
            var outPutFile = GetConfig("DIRECTORY").GetSection("OUTPUT").Value;
            
            var converter = new Converter(asposeLicense);

            using (var sr = File.Open(testFile, FileMode.Open))
            {
                sr.Seek(0, SeekOrigin.Begin);
                using (var convertedFile = converter.ConvertPdfToJpeg(sr))
                {
                    var fileStream = File.Create(outPutFile);

                    convertedFile.Seek(0, SeekOrigin.Begin);
                    convertedFile.CopyTo(fileStream);
                    
                    fileStream.Close();
                }
            }
        }
        
        private static IConfigurationSection GetConfig(string key)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            return configuration.GetSection(key);
        }
    }

    public class Converter
    {
        public Converter(string asposeLicense)
        {
            LoadLicense(asposeLicense);
        }
        public Stream ConvertPdfToJpeg(Stream inputStream)
        {
            try
            {
                var jpegStream = new MemoryStream();
                using (var pdfDoc = new Document(inputStream))
                {
                    new JpegDevice(new Resolution(72), 100).Process(pdfDoc.Pages.First(), jpegStream);
                    return jpegStream;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something happened. {e} ");
                throw;
            }
        }

        private void LoadLicense(string licenseKey)
        {
            var licenseBytes = Convert.FromBase64String(licenseKey);
            
            using (var totalLicense = new MemoryStream(licenseBytes))
            {
                var asposeLicense = new License();
                asposeLicense.SetLicense(totalLicense);
                
                totalLicense.Seek(0, SeekOrigin.Begin);
                new Aspose.Cells.License().SetLicense(totalLicense);
                totalLicense.Seek(0, SeekOrigin.Begin);
                new Aspose.Slides.License().SetLicense(totalLicense);
                totalLicense.Seek(0, SeekOrigin.Begin);
                new Aspose.Pdf.License().SetLicense(totalLicense);
                totalLicense.Seek(0, SeekOrigin.Begin);
                new Aspose.Note.License().SetLicense(totalLicense);
                totalLicense.Seek(0, SeekOrigin.Begin);
                new Aspose.Diagram.License().SetLicense(totalLicense);
                totalLicense.Seek(0, SeekOrigin.Begin);
                new Aspose.Imaging.License().SetLicense(totalLicense);
                totalLicense.Seek(0, SeekOrigin.Begin);
                new Aspose.Email.License().SetLicense(totalLicense);
            }
        }
    }
}
