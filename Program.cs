using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Define paths
        string spotlightPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets"
        );

        string destinationPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
            "SpotlightImages"
        );

        // Create destination folder if it doesn't exist
        if (!Directory.Exists(destinationPath))
        {
            Directory.CreateDirectory(destinationPath);
        }

        // Get all files in the Spotlight directory
        var files = Directory.GetFiles(spotlightPath);

        if (files.Length == 0)
        {
            Console.WriteLine("No Spotlight images found. Make sure Windows Spotlight is enabled.");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
            return;
        }

        int copiedFiles = 0;
        foreach (var file in files)
        {
            FileInfo fileInfo = new FileInfo(file);

            // Filter out small files (likely non-image)
            if (fileInfo.Length > 100 * 1024) // Only files larger than 100 KB
            {
                string newFileName = Path.Combine(destinationPath, fileInfo.Name + ".jpg");

                // Copy file only if it doesn't already exist
                if (!File.Exists(newFileName))
                {
                    File.Copy(file, newFileName);
                    copiedFiles++;
                }
            }
        }

        Console.WriteLine($"Done! {copiedFiles} new Spotlight images saved in: {destinationPath}");
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
