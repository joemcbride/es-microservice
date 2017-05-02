#load "helpers/Constants.cs"
#load "helpers/Methods.cs"

using System;
using System.IO;
using System.Linq;

Task("dotnetCompile").Does(()=>
  {
    Run("dotnet", "build src");
  });
