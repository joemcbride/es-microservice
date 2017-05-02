using System.Diagnostics;

Task("clean")
    .Does(() =>
    {
        StartProcess("git", @"clean -d -f -x -e tools/*");
    });