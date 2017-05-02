#addin "Cake.Yarn"

Task("yarn")
    .Does(() =>
    {
        Yarn.FromPath(".").Install();
    });
