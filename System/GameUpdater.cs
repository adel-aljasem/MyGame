using System.Net.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.IO;

public class GameUpdater
{
    private const string FileInfoUrl = "http://localhost:5097/fileinfo/AdilGame.exe"; // URL to your file info endpoint
    private const string FileUrl = "http://localhost:5097/GameFiles/AdilGame.exe"; // URL to your game file
    private static readonly string LocalFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "adilgame.exe");

    public static async Task CheckAndUpdateGameFile()
    {
        using (var client = new HttpClient())
        {
            try
            {
                // Get file metadata from the server
                var fileInfoResponse = await client.GetStringAsync(FileInfoUrl);
                var fileInfo = JsonConvert.DeserializeObject<FileMetadata>(fileInfoResponse);

                var serverLastModified = DateTime.Parse(fileInfo.LastModified);
                var localLastModified = File.Exists(LocalFilePath)
                    ? File.GetLastWriteTimeUtc(LocalFilePath)
                    : DateTime.MinValue;

                // If server file is newer, download it
                if (serverLastModified > localLastModified)
                {
                    Console.WriteLine("New update found. Downloading...");
                    var fileBytes = await client.GetByteArrayAsync(FileUrl);
                    File.WriteAllBytes(LocalFilePath, fileBytes);
                    Console.WriteLine("Update completed.");
                }
                else
                {
                    Console.WriteLine("No new updates.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking for updates: {ex.Message}");
            }
        }
    }

    private class FileMetadata
    {
        public string Filename { get; set; }
        public string LastModified { get; set; }
    }
}
