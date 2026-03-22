# ConverTool.PluginAbstractions

该包包含 **ConverTool** 的 Host <-> Plugin 协议契约（contract）。

插件需要实现 `PluginAbstractions.IConverterPlugin`，并由 Host 调用时提供：

- `PluginAbstractions.ExecuteContext`
- `PluginAbstractions.IProgressReporter`

完整的 `manifest.json` 与 `configSchema` 格式说明请参考主仓库文档：

- [docs/plugin-dev.md](https://github.com/kckc7887/ConverTool/blob/main/docs/plugin-dev.md)

契约类型（如 `ConfigSchema`、`ConfigField`、`FieldPersistOverrideRule`）与 `manifest.json` 中的扩展字段对应；发布版本见包版本号（当前 **1.1.0**，与 Host **v1.1.0** 发行线对齐）。

## 版本说明（1.1.0）

- **`ConfigField.VisibleForTargetFormatIds`**：与 manifest 中 `visibleForTargetFormats` 对应；非空时 Host 仅在**当前选中的目标格式 id**匹配时显示该字段（见主仓库 `docs/plugin-dev.md`）。

## 早期说明（1.0.2）

- **`ConfigFieldType`** 与 JSON 配置字段类型对齐，含 **`MultiSelect`** 等；**Host 对 `MultiSelect` 的渲染**以主仓库文档为准。
- **与 Host 对齐**：`ExecuteContext.OutputNamingContext` 中由 Host 注入的键（如 **`timeYmd`、`timeHms`** 等）可在插件内只读使用（详见主仓库 `docs/plugin-dev.md`）。
- 详细变更见本仓库 **[CHANGELOG.md](./CHANGELOG.md)**（若有）。

