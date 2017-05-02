#load "helpers/Methods.cs"

using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

Task("dotnetTest")
  .Does(() =>
  {
    var files = System.IO.Directory.GetFiles(@"src", "*.csproj", SearchOption.AllDirectories)
      .Where(x => x.Contains("Tests"));

    foreach (var file in files)
    {
      Run("dotnet", "test " + file);
    }
  });
