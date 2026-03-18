namespace PluginAbstractions;

public interface IConverterPlugin
{
    PluginManifest GetManifest();

    Task ExecuteAsync(ExecuteContext context, IProgressReporter reporter, CancellationToken cancellationToken = default);
}

public sealed record PluginManifest(
    string PluginId,
    string Version,
    IReadOnlyList<string> SupportedInputExtensions,
    IReadOnlyList<TargetFormat> SupportedTargetFormats,
    ConfigSchema ConfigSchema,
    IReadOnlyList<string> SupportedLocales,
    I18nDescriptor I18n
);

public sealed record I18nDescriptor(
    string LocalesFolder
);

public sealed record TargetFormat(
    string Id,
    string DisplayNameKey,
    string? DescriptionKey = null
);

public sealed record ConfigSchema(
    IReadOnlyList<ConfigSection> Sections
);

public sealed record ConfigSection(
    string Id,
    string? TitleKey,
    string? DescriptionKey,
    bool CollapsedByDefault,
    IReadOnlyList<ConfigField> Fields
);

public sealed record ConfigField(
    string Key,
    ConfigFieldType Type,
    string LabelKey,
    string? HelpKey = null,
    object? DefaultValue = null,
    IReadOnlyList<ConfigOption>? Options = null,
    RangeValidation? Range = null,
    PathFieldDescriptor? Path = null
);

public enum ConfigFieldType
{
    Select,
    MultiSelect,
    Range,
    Text,
    Checkbox,
    Path
}

public sealed record ConfigOption(
    string Id,
    string LabelKey
);

public sealed record RangeValidation(
    double Min,
    double Max,
    double Step = 1
);

public sealed record PathFieldDescriptor(
    PathKind Kind,
    bool MustExist = true
);

public enum PathKind
{
    File,
    Folder
}

public sealed record ExecuteContext(
    string JobId,
    string InputPath,
    string TempJobDir,
    string TargetFormatId,
    IReadOnlyDictionary<string, object?> SelectedConfig,
    string Locale,
    IReadOnlyDictionary<string, object?> OutputNamingContext
);

public interface IProgressReporter
{
    void OnLog(string line);
    void OnProgress(ProgressInfo info);
    void OnCompleted(CompletedInfo info);
    void OnFailed(FailedInfo info);
}

public sealed record ProgressInfo(
    ProgressStage Stage,
    int? PercentWithinStage = null
);

public enum ProgressStage
{
    Preparing,
    Running,
    Finalizing
}

public sealed record CompletedInfo(
    string OutputRelativePath,
    string? OutputSuggestedExt = null,
    IReadOnlyDictionary<string, object?>? Metadata = null
);

public sealed record FailedInfo(
    string ErrorMessage,
    string? ErrorCode = null
);
