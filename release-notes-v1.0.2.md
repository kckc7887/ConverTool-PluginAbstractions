## ConverTool.PluginAbstractions v1.0.2

面向 **插件开发者**（NuGet：`ConverTool.PluginAbstractions`）。

### 变更摘要

- **`ConfigFieldType`** 扩展 **`MultiSelect`**，与 manifest 配置字段类型命名空间对齐。  
  **注意**：Host v1.0.2 **尚未**提供 `MultiSelect` 的 UI 与回传；请勿在已发布插件的 manifest 中使用，直至 Host 宣布支持。
- **文档与 XML 注释**：`ConfigSchema`、`FieldPersistOverrides` 等与主仓库 `plugin-dev.md` 对齐。
- **与 Host**：`ExecuteContext.OutputNamingContext` 中由 Host 注入的键（如 `timeYmd` / `timeHms`）可在插件内只读使用（与 ConverTool v1.0.2 输出命名一致）。

完整条目见仓库根目录 **`CHANGELOG.md`**。
