
private void Run(string process, params string[] args)
{
    StartProcess(process,
      new ProcessSettings
      {
        Arguments = string.Join(" ", args)
      }
    );
}
