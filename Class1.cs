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

/// <summary>
/// Host merges JSON <c>manifest.json</c> <c>configSchema</c> with this contract. Optional lists may be
/// omitted when plugins build manifests in code; file-based manifests can still declare the same keys.
/// </summary>
public sealed record ConfigSchema(
    IReadOnlyList<ConfigSection> Sections,
    /// <summary>
    /// When a rule's <see cref="FieldPersistOverrideRule.When"/> matches the current UI values, Host applies
    /// <see cref="FieldPersistOverrideRule.Fields"/> only to the <strong>serialized user-settings snapshot</strong>
    /// (and on restore after load) so the next launch defaults accordingly, without forcing immediate UI changes
    /// during capture unless restore runs.
    /// </summary>
    IReadOnlyList<FieldPersistOverrideRule>? FieldPersistOverrides = null
);

/// <summary>
/// Declares that certain field values should be written to persisted user settings (and applied after restore)
/// when a boolean condition on another field holds — e.g. "if retain is off, persist enable as false for next launch".
/// </summary>
public sealed record FieldPersistOverrideRule(
    VisibleIfCondition When,
    IReadOnlyDictionary<string, object?> Fields
);

/// <summary>
/// Same semantics as manifest <c>visibleIf</c>: show or match when <paramref name="FieldKey"/> equals <paramref name="Expected"/>.
/// </summary>
public sealed record VisibleIfCondition(
    string FieldKey,
    bool Expected
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
