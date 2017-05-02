using System;

Task("clobber")
    .Does(() =>
    {
        var directoriesToClean = new [] {@"obj", @"bin", @".build" };
        var dir = System.IO.Directory.GetCurrentDirectory();

        Information("Cleaning files from {0}", dir);
        DeleteDirectories(dir, directoriesToClean);
    });


private void DeleteDirectories(string dir, string[] directoriesToClean)
{
    System.IO.Directory.EnumerateDirectories(dir)
        .Where(x => !x.Contains("node_modules"))
        .ToList()
        .ForEach(currentDir =>
        {
            if(directoriesToClean.Any(currentDir.EndsWith))
            {
                Information("Cleaning {0}", currentDir);
                System.IO.Directory.Delete(currentDir, true);
            }
            else
            {
                DeleteDirectories(currentDir, directoriesToClean);
            }
        });
}
