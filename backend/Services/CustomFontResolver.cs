using System.Reflection;
using PdfSharpCore.Fonts;

namespace SmartCampus.API.Services;

public class CustomFontResolver : IFontResolver
{
    public string DefaultFontName => "Arial";

    public byte[] GetFont(string faceName)
    {
        // Linux Path (Debian/Ubuntu/Standard .NET Images)
        if (File.Exists($"/usr/share/fonts/truetype/liberation/{faceName}"))
        {
            return File.ReadAllBytes($"/usr/share/fonts/truetype/liberation/{faceName}");
        }

        // Windows Fallback
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            var fontsPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            var fullPath = Path.Combine(fontsPath, faceName);
            
            if (File.Exists(fullPath))
                return File.ReadAllBytes(fullPath);
                
            // Try with lowercase if not found (case sensitivity varies)
            fullPath = Path.Combine(fontsPath, faceName.ToLower());
            if (File.Exists(fullPath))
                return File.ReadAllBytes(fullPath);
        }

        // Return empty array if font not found (fallback to default)
        return Array.Empty<byte>();
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        // Linux Logic: Map Arial to Liberation Sans
        if (IsLinux())
        {
            if (familyName.Equals("Arial", StringComparison.OrdinalIgnoreCase))
            {
                if (isBold && isItalic) return new FontResolverInfo("LiberationSans-BoldItalic.ttf");
                if (isBold) return new FontResolverInfo("LiberationSans-Bold.ttf");
                if (isItalic) return new FontResolverInfo("LiberationSans-Italic.ttf");
                return new FontResolverInfo("LiberationSans-Regular.ttf");
            }
        }
        else 
        {
            // Windows Logic: Keep Arial
            if (familyName.Equals("Arial", StringComparison.OrdinalIgnoreCase))
            {
                if (isBold && isItalic) return new FontResolverInfo("arialbi.ttf");
                if (isBold) return new FontResolverInfo("arialbd.ttf");
                if (isItalic) return new FontResolverInfo("ariali.ttf");
                return new FontResolverInfo("arial.ttf");
            }
        }

        // Default fallback (Liberation for Linux, Arial for Windows)
        if (IsLinux())
            return new FontResolverInfo("LiberationSans-Regular.ttf");
        
        return new FontResolverInfo("arial.ttf");
    }

    private bool IsLinux()
    {
        return Environment.OSVersion.Platform == PlatformID.Unix || 
               Environment.OSVersion.Platform == PlatformID.MacOSX;
    }
}
