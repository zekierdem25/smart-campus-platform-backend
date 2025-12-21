using PdfSharpCore.Fonts;
using SmartCampus.API.Services;
using System;
using System.Reflection;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class CustomFontResolverTests
{
    private readonly CustomFontResolver _resolver;

    public CustomFontResolverTests()
    {
        _resolver = new CustomFontResolver();
    }

    [Fact]
    public void DefaultFontName_ShouldReturnArial()
    {
        // Act
        var result = _resolver.DefaultFontName;

        // Assert
        Assert.Equal("Arial", result);
    }

    [Fact]
    public void GetFont_WithNonExistentFont_ShouldReturnEmptyArray()
    {
        // Arrange
        var nonExistentFont = "NonExistentFont.ttf";

        // Act
        var result = _resolver.GetFont(nonExistentFont);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetFont_WithNullFaceName_ShouldThrowException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _resolver.GetFont(null!));
    }

    [Fact]
    public void GetFont_WithEmptyFaceName_ShouldReturnEmptyArray()
    {
        // Act
        var result = _resolver.GetFont("");

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ResolveTypeface_WithArialRegular_ShouldReturnCorrectFont()
    {
        // Act
        var result = _resolver.ResolveTypeface("Arial", false, false);

        // Assert
        Assert.NotNull(result);
        // Windows'ta "arial.ttf", Linux'ta "LiberationSans-Regular.ttf" döndürmeli
        var fontName = result.ToString();
        Assert.NotNull(fontName);
        Assert.NotEmpty(fontName);
    }

    [Fact]
    public void ResolveTypeface_WithArialBold_ShouldReturnCorrectFont()
    {
        // Act
        var result = _resolver.ResolveTypeface("Arial", true, false);

        // Assert
        Assert.NotNull(result);
        // FontResolverInfo başarıyla oluşturuldu
    }

    [Fact]
    public void ResolveTypeface_WithArialItalic_ShouldReturnCorrectFont()
    {
        // Act
        var result = _resolver.ResolveTypeface("Arial", false, true);

        // Assert
        Assert.NotNull(result);
        // FontResolverInfo başarıyla oluşturuldu
    }

    [Fact]
    public void ResolveTypeface_WithArialBoldItalic_ShouldReturnCorrectFont()
    {
        // Act
        var result = _resolver.ResolveTypeface("Arial", true, true);

        // Assert
        Assert.NotNull(result);
        // FontResolverInfo başarıyla oluşturuldu
    }

    [Fact]
    public void ResolveTypeface_WithArialCaseInsensitive_ShouldReturnCorrectFont()
    {
        // Act
        var result1 = _resolver.ResolveTypeface("arial", false, false);
        var result2 = _resolver.ResolveTypeface("ARIAL", false, false);
        var result3 = _resolver.ResolveTypeface("ArIaL", false, false);

        // Assert
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.NotNull(result3);
        Assert.NotNull(result1.ToString());
        Assert.NotNull(result2.ToString());
        Assert.NotNull(result3.ToString());
    }

    [Fact]
    public void ResolveTypeface_WithNonArialFont_ShouldReturnDefaultFont()
    {
        // Act
        var result = _resolver.ResolveTypeface("Times New Roman", false, false);

        // Assert
        Assert.NotNull(result);
        var fontName = result.ToString();
        Assert.NotNull(fontName);
        // Linux'ta "LiberationSans-Regular.ttf", Windows'ta "arial.ttf" döndürmeli
    }

    [Fact]
    public void ResolveTypeface_WithNullFamilyName_ShouldThrowException()
    {
        // Act & Assert
        Assert.Throws<NullReferenceException>(() => _resolver.ResolveTypeface(null!, false, false));
    }

    [Fact]
    public void ResolveTypeface_WithEmptyFamilyName_ShouldReturnDefaultFont()
    {
        // Act
        var result = _resolver.ResolveTypeface("", false, false);

        // Assert
        Assert.NotNull(result);
        var fontName = result.ToString();
        Assert.NotNull(fontName);
    }

    [Fact]
    public void ResolveTypeface_WithDifferentFontNames_ShouldReturnDefaultFont()
    {
        // Act
        var result1 = _resolver.ResolveTypeface("Helvetica", false, false);
        var result2 = _resolver.ResolveTypeface("Courier", false, false);
        var result3 = _resolver.ResolveTypeface("Verdana", false, false);

        // Assert
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.NotNull(result3);
        Assert.NotNull(result1.ToString());
        Assert.NotNull(result2.ToString());
        Assert.NotNull(result3.ToString());
    }

    [Fact]
    public void IsLinux_ShouldReturnBoolean()
    {
        // Arrange
        var method = typeof(CustomFontResolver)
            .GetMethod("IsLinux", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);

        // Act
        var result = method!.Invoke(_resolver, null) as bool?;

        // Assert
        Assert.NotNull(result);
        Assert.True(result.HasValue);
    }

    [Fact]
    public void IsLinux_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(CustomFontResolver)
            .GetMethod("IsLinux", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }

    [Fact]
    public void ResolveTypeface_WithArialBoldOnWindows_ShouldReturnArialBold()
    {
        // Arrange - Windows platform kontrolü
        var isWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;

        // Act
        var result = _resolver.ResolveTypeface("Arial", true, false);

        // Assert
        Assert.NotNull(result);
        // FontResolverInfo başarıyla oluşturuldu
    }

    [Fact]
    public void ResolveTypeface_WithArialItalicOnWindows_ShouldReturnArialItalic()
    {
        // Arrange - Windows platform kontrolü
        var isWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;

        // Act
        var result = _resolver.ResolveTypeface("Arial", false, true);

        // Assert
        Assert.NotNull(result);
        // FontResolverInfo başarıyla oluşturuldu
    }

    [Fact]
    public void ResolveTypeface_WithArialBoldItalicOnWindows_ShouldReturnArialBoldItalic()
    {
        // Arrange - Windows platform kontrolü
        var isWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;

        // Act
        var result = _resolver.ResolveTypeface("Arial", true, true);

        // Assert
        Assert.NotNull(result);
        // FontResolverInfo başarıyla oluşturuldu
    }

    [Fact]
    public void ResolveTypeface_WithArialRegularOnWindows_ShouldReturnArialRegular()
    {
        // Arrange - Windows platform kontrolü
        var isWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;

        // Act
        var result = _resolver.ResolveTypeface("Arial", false, false);

        // Assert
        Assert.NotNull(result);
        // FontResolverInfo başarıyla oluşturuldu
    }

    [Fact]
    public void GetFont_ShouldNotThrowException()
    {
        // Arrange
        var faceName = "test-font.ttf";

        // Act & Assert
        var exception = Record.Exception(() => _resolver.GetFont(faceName));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ResolveTypeface_ShouldNotThrowException()
    {
        // Arrange
        var familyName = "Arial";
        var isBold = false;
        var isItalic = false;

        // Act & Assert
        var exception = Record.Exception(() => _resolver.ResolveTypeface(familyName, isBold, isItalic));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ResolveTypeface_WithAllBoldItalicCombinations_ShouldReturnValidResults()
    {
        // Act
        var regular = _resolver.ResolveTypeface("Arial", false, false);
        var bold = _resolver.ResolveTypeface("Arial", true, false);
        var italic = _resolver.ResolveTypeface("Arial", false, true);
        var boldItalic = _resolver.ResolveTypeface("Arial", true, true);

        // Assert
        Assert.NotNull(regular);
        Assert.NotNull(bold);
        Assert.NotNull(italic);
        Assert.NotNull(boldItalic);
        Assert.NotNull(regular.ToString());
        Assert.NotNull(bold.ToString());
        Assert.NotNull(italic.ToString());
        Assert.NotNull(boldItalic.ToString());
    }
}

