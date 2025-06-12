namespace Common.Options;

public class AppOptions
{
    public string? RequestsFolder { get; set; }

    public string FullFolderPath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "Temp", RequestsFolder ?? Environment.CurrentDirectory);
}