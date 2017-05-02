#load "helpers/Constants.cs"
#load "helpers/Methods.cs"

using System;
using System.Linq;

Task("publish").Does(()=>
  {
    var dir = System.IO.Directory.GetCurrentDirectory();
    dir = System.IO.Path.Combine(dir, Constants.Domain.PublishOutputFolder);
    Information("Publishing to " + dir);

    var config = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production"
      ? "Release"
      : "Debug";

    Run("dotnet",
      string.Format("publish {0} -o {1} -c {2}",
        Constants.Domain.ApplicationPath,
        dir,
        config));
  });
