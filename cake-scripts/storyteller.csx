#tool "nuget:?package=StoryTeller&version=3.0.1"
#load "helpers/Constants.cs"

using System;
using System.Linq;

Task("st:run").Does(() => LaunchSt("run", "--results-path", "st-results/index"));
Task("st:open").Does(() => LaunchSt("open"));

private void LaunchSt(string command, params string[] extraArgs)
{
    var args = new[] {
      command,
      Constants.Domain.StoryTellerAppPath
    }.Concat(extraArgs);

    StartProcess("./tools/StoryTeller/tools/ST.exe", new ProcessSettings { Arguments = string.Join(" ", args) });
}
