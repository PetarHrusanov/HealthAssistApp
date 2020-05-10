// <copyright file="HtmlToPdfConverter.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Web.Methods.PDF
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public interface IHtmlToPdfConverter
    {
        byte[] Convert(string basePath, string htmlCode);
    }

    public class HtmlToPdfConverter : IHtmlToPdfConverter
    {
    public byte[] Convert(string basePath, string htmlCode)
        {
            var inputFileName = $"input_{Guid.NewGuid()}.html";
            var outputFileName = $"output_{Guid.NewGuid()}.pdf";
            var formatType = "A4";
            var orientationType = "portrait";

            File.WriteAllText($"{basePath}/{inputFileName}", htmlCode);

            var startInfo = new ProcessStartInfo("phantomjs.exe")
            {
                WorkingDirectory = basePath,
                Arguments = $"rasterize.js \"{basePath}/{inputFileName}\" \"{outputFileName}\" \"{formatType}\" \"{orientationType}\"",
                UseShellExecute = true,
            };

            var process = new Process { StartInfo = startInfo };
            process.Start();

            process.WaitForExit();

            var bytes = File.ReadAllBytes($"{basePath}/{outputFileName}");

            File.Delete($"{basePath}/{inputFileName}");
            File.Delete($"{basePath}/{outputFileName}");

            return bytes;
        }
    }
}
