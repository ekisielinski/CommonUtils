using System.IO;
using Xunit;

namespace EK.CommonUtils.Tests;

public sealed class FileUtilsTests
{
    [Fact]
    public void DeleteIfExists__FileExists__DeleteFile()
    {
        // arrange
        var tempFilePath = Path.GetTempFileName();

        // act
        FileUtils.DeleteIfExists(tempFilePath);

        // assert
        Assert.False(File.Exists(tempFilePath));
    }

    [Fact]
    public void DeleteIfExists__FileDoesNotExist__DoNothing()
    {
        // arrange
        var randomFileName = Path.GetRandomFileName();
        Assert.False(File.Exists(randomFileName));

        // act
        FileUtils.DeleteIfExists(randomFileName);

        // assert
        Assert.False(File.Exists(randomFileName));
    }

    [Fact]
    public void DeleteIfEmpty__EmptyFileExists__DeleteFile()
    {
        // arrange
        var tempFilePath = Path.GetTempFileName();

        // act
        FileUtils.DeleteIfEmpty(tempFilePath);

        // assert
        Assert.False(File.Exists(tempFilePath));
    }

    [Fact]
    public void DeleteIfEmpty__FileDoesNotExist__DoNothing()
    {
        // arrange
        var randomFileName = Path.GetRandomFileName();
        Assert.False(File.Exists(randomFileName));

        // act
        FileUtils.DeleteIfEmpty(randomFileName);

        // assert
        Assert.False(File.Exists(randomFileName));
    }

    [Fact]
    public void DeleteIfEmpty__NonEmptyFileExists__DoNothing()
    {
        // arrange
        var tempFilePath = Path.GetTempFileName();
        File.WriteAllText(tempFilePath, "non-empty-file");

        // act
        FileUtils.DeleteIfEmpty(tempFilePath);

        // assert
        Assert.True(File.Exists(tempFilePath));
    }

    [Fact]
    public void GetSize__EmptyFile__ReturnZeroSize()
    {
        // arrange
        var tempFilePath = Path.GetTempFileName();

        // act
        var bytes = FileUtils.GetSize(tempFilePath).Bytes;

        // assert
        Assert.Equal(0, bytes);
    }

    [Fact]
    public void GetSize__NonEmptyFile__ReturnExpectedSize()
    {
        // arrange
        var tempFilePath = Path.GetTempFileName();
        var data = new byte[] { 1, 2, 3, 4, 5 };
        File.WriteAllBytes(tempFilePath, data);

        // act
        var bytes = FileUtils.GetSize(tempFilePath).Bytes;

        // assert
        Assert.Equal(data.Length, bytes);
    }

    [Fact]
    public void GetSize__FileDoesNotExist__ThrowException()
    {
        // arrange
        var randomFileName = Path.GetRandomFileName();
        Assert.False(File.Exists(randomFileName));

        // act, assert
        Assert.Throws<FileNotFoundException>(() => FileUtils.GetSize(randomFileName));
    }
}
