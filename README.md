# ConverTool.PluginAbstractions

This package contains the Host<->Plugin contract for **ConverTool**.

Plugins implement `PluginAbstractions.IConverterPlugin` and are invoked by Host with:

- `PluginAbstractions.ExecuteContext`
- `PluginAbstractions.IProgressReporter`

See `docs/plugin-dev.md` in the main repository for the full manifest and configSchema format.

