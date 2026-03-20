# Changelog

All notable changes to **ConverTool.PluginAbstractions** are documented here.  
版本号与 **ConverTool Host** 发版对齐；破坏性变更按 SemVer 处理。

## [1.0.2] - 2026-03-19

### 面向插件开发者

- **`ConfigFieldType` 扩展**：枚举包含 **`MultiSelect`**，与 manifest JSON 中的配置字段类型命名空间对齐，便于插件与 Host 在同一契约下演进。  
  - **注意**：当前公开 Host（v1.0.2）**尚未**为 `MultiSelect` 提供 UI 与值回传；请勿在已发布插件的 `manifest.json` 中使用该类型，直至 Host 发行说明宣布支持。
- **文档与类型注释**：`ConfigSchema` / `FieldPersistOverrides` 等 XML 注释与主仓库 [`plugin-dev.md`](https://github.com/kckc7887/ConverTool/blob/main/docs/plugin-dev.md) 保持一致性说明。
- **修复**：契约层与文档/示例中的笔误或不一致表述（若你使用 **1.0.1** 构建的插件，接口形状未变，一般**无需**重编译，除非你要引用新枚举成员）。

### 与 Host 的对应关系

- 与 **ConverTool Host v1.0.2** 同号发布；`ExecuteContext.OutputNamingContext` 在 Host 侧新增的键（如 `timeYmd` / `timeHms`）由 Host 注入，**非**本包新增字段，但插件可通过字典读取。

---

## [1.0.1] - （见 Host v1.0.1 发版说明）

- 引入 **`FieldPersistOverrides`** / `FieldPersistOverrideRule` 等与 manifest `fieldPersistOverrides` 对齐的契约类型。

## [1.0.0] - 初始版本

- `IConverterPlugin`、`ExecuteContext`、`PluginManifest`、`ConfigSchema` 等核心契约。
