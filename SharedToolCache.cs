using System.IO.Compression;

namespace PluginAbstractions;

public static class SharedToolCache
{
    public static readonly string CacheRoot =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ConverTool", "tools");

    public static string GetToolDir(string toolName, string? version = null)
    {
        if (string.IsNullOrWhiteSpace(version))
            return Path.Combine(CacheRoot, toolName);
        return Path.Combine(CacheRoot, toolName, version);
    }

    public static bool IsToolCached(string toolName, string? version = null)
    {
        var dir = GetToolDir(toolName, version);
        return Directory.Exists(dir) && Directory.EnumerateFileSystemEntries(dir).Any();
    }

    public static string GetToolPath(string toolName, string executableName, string? version = null)
    {
        return Path.Combine(GetToolDir(toolName, version), executableName);
    }

    private static bool IsEntryDirectory(ZipArchiveEntry entry)
    {
        return entry.FullName.EndsWith('/') || entry.FullName.EndsWith('\\');
    }

    public static async Task DownloadAndExtractAsync(
        string toolName,
        string version,
        string downloadUrl,
        string targetDir,
        IProgressReporter? reporter = null,
        CancellationToken cancellationToken = default)
    {
        var zipPath = Path.Combine(CacheRoot, $"{toolName}-{version}.zip");
        var extractDir = GetToolDir(toolName, version);

        if (Directory.Exists(extractDir) && Directory.EnumerateFileSystemEntries(extractDir).Any())
        {
            reporter?.OnLog($"[{toolName}] already cached at {extractDir}");
            return;
        }

        Directory.CreateDirectory(CacheRoot);
        Directory.CreateDirectory(extractDir);

        try
        {
            reporter?.OnLog($"[{toolName}] downloading from {downloadUrl}...");

            using var hc = new HttpClient();
            hc.Timeout = TimeSpan.FromMinutes(30);
            var data = await hc.GetByteArrayAsync(downloadUrl, cancellationToken).ConfigureAwait(false);
            await File.WriteAllBytesAsync(zipPath, data, cancellationToken).ConfigureAwait(false);

            reporter?.OnLog($"[{toolName}] extracting...");

            await Task.Run(() =>
            {
                using var archive = ZipFile.OpenRead(zipPath);
                var rootFolder = archive.Entries.FirstOrDefault(e => IsEntryDirectory(e) && e.FullName.Split('/', '\\').Length == 2)?.FullName;
                
                foreach (var entry in archive.Entries)
                {
                    if (IsEntryDirectory(entry)) continue;
                    
                    var relativePath = entry.FullName;
                    if (!string.IsNullOrEmpty(rootFolder) && relativePath.StartsWith(rootFolder))
                    {
                        relativePath = relativePath.Substring(rootFolder.Length);
                    }
                    
                    if (string.IsNullOrWhiteSpace(relativePath)) continue;
                    
                    var destPath = Path.Combine(extractDir, relativePath);
                    Directory.CreateDirectory(Path.GetDirectoryName(destPath)!);
                    
                    using var srcStream = entry.Open();
                    using var destStream = File.Create(destPath);
                    srcStream.CopyTo(destStream);
                }
            }, cancellationToken).ConfigureAwait(false);

            reporter?.OnLog($"[{toolName}] installed to {extractDir}");
        }
        finally
        {
            if (File.Exists(zipPath))
            {
                try { File.Delete(zipPath); } catch { }
            }
        }
    }
}
