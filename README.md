# ConverTool.PluginAbstractions

该包包含 **ConverTool** 的 Host <-> Plugin 协议契约（contract）。

插件需要实现 `PluginAbstractions.IConverterPlugin`，并由 Host 调用时提供：

- `PluginAbstractions.ExecuteContext`
- `PluginAbstractions.IProgressReporter`

完整的 `manifest.json` 与 `configSchema` 格式说明请参考主仓库文档：

- [docs/plugin-dev.md](https://github.com/kckc7887/ConverTool/blob/main/docs/plugin-dev.md)

契约类型（如 `ConfigSchema`、`FieldPersistOverrideRule`）与 `manifest.json` 中的扩展字段对应；发布版本见包版本号（当前 **1.0.2**）。

## 版本说明（1.0.2）

- **`ConfigFieldType`** 与 JSON 配置字段类型对齐，新增 **`MultiSelect`** 等枚举成员，便于在契约层表达「多选」类字段；**当前 Host v1.0.2 尚未渲染 `MultiSelect`**，请勿在 manifest 中使用，直至 Host 文档宣布支持。
- **与 Host 对齐**：`ExecuteContext.OutputNamingContext` 中由 Host 注入的键（如 **`timeYmd`、`timeHms`** 等）可在插件内只读使用，用于与输出命名模板一致的时间片段（详见主仓库 `docs/plugin-dev.md` §7.7）。
- 详细变更见本仓库 **[CHANGELOG.md](./CHANGELOG.md)**。

