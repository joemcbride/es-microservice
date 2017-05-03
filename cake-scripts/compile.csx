#load "helpers/Constants.cs"
#load "helpers/Methods.cs"

using System;
using System.IO;
using System.Linq;

Task("dotnetCompile").Does(()=>
  {
    var config = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production"
      ? "Release"
      : "Debug";

    Run("dotnet", string.Format("build src -c {0}", config));
  });
