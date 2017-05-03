#load "./cake-scripts/clobber.csx"
#load "./cake-scripts/compile.csx"
#load "./cake-scripts/restore.csx"
#load "./cake-scripts/test.csx"
#load "./cake-scripts/server.csx"
#load "./cake-scripts/publish.csx"
#load "./cake-scripts/yarn.csx"

var target = Argument("target", "default");

Task("default")
    .IsDependentOn("clobber")
    .IsDependentOn("yarn")
    .IsDependentOn("test")
    .IsDependentOn("publish");

Task("compile")
    .IsDependentOn("restore")
    .IsDependentOn("dotnetCompile");

Task("test")
    .IsDependentOn("compile")
    .IsDependentOn("dotnetTest");

Task("ci")
    .IsDependentOn("clobber")
    .IsDependentOn("yarn")
    .IsDependentOn("test")
    .IsDependentOn("publish");

RunTarget(target);
