# ConverTool.PluginAbstractions

该包包含 **ConverTool** 的 Host <-> Plugin 协议契约（contract）。

插件需要实现 `PluginAbstractions.IConverterPlugin`，并由 Host 调用时提供：

- `PluginAbstractions.ExecuteContext`
- `PluginAbstractions.IProgressReporter`

完整的 `manifest.json` 与 `configSchema` 格式说明请参考主仓库的 `docs/plugin-dev.md`。

