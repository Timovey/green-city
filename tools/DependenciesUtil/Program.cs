using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DependenciesUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            var versions = new Dictionary<string, string>
            {
                ["GreenCity.CommonInfrastructure"] = "1.0.15"
            };
            UpdateNugetVersion(versions);
        }

        private static void UpdateNugetVersion(Dictionary<string, string> nameVersionDictionary)
        {
            var path = @"D:\GreenCity\back";

            var files = Directory.EnumerateFiles(path, "*.csproj", SearchOption.AllDirectories);
            foreach (var csFile in files)
            {
                Console.WriteLine(": ->" + Path.GetFileName(csFile));
                var lines = File.ReadAllLines(csFile);
                (string line, bool changed)[] updatedLines = lines.Select(l => SetVersion(l, nameVersionDictionary)).ToArray();

                if (updatedLines.Any(l => l.changed))
                    File.WriteAllLines(csFile, updatedLines.Select(l => l.line), new UTF8Encoding(true));
            }
        }

        private static (string, bool) SetVersion(string line, Dictionary<string, string> versions)
        {
            if (line.Contains("PackageReference"))
            {
                var tabs = line.Substring(0, line.IndexOf('<'));
                try
                {
                    var xelement = XElement.Parse(line);
                    var packageName = xelement?.Attribute("Include")?.Value;

                    if (!string.IsNullOrEmpty(packageName)
                        && versions.TryGetValue(packageName, out var version))
                    {
                        var versionAttr = xelement.Attribute("Version");
                        Console.WriteLine($"{packageName}: {versionAttr.Value} -> {version}");
                        versionAttr.Value = version;
                        return (tabs + xelement, true);
                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }

            return (line, false);
        }
    }
}
