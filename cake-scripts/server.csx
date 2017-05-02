#load "helpers/Constants.cs"

using System;
using System.Linq;

Task("server").Does(() => Launch("dotnet", "watch run"));

private void Launch(string process, params string[] args)
{
    StartProcess(process, new ProcessSettings
      {
        Arguments = string.Join(" ", args),
        WorkingDirectory = Constants.Domain.ApplicationPath
      }
    );
}
