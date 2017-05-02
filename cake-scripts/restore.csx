#load "helpers/Constants.cs"
#load "helpers/Methods.cs"

using System;
using System.IO;
using System.Linq;

Task("restore").Does(()=>
  {
    Run("dotnet", "restore src");
  });
