# Unity 项目代码规范

## 1. 命名规范

### 1.1 脚本和类名
- 使用 PascalCase（首字母大写）
- 类名应使用名词或名词短语
- 脚本名应与类名完全一致
- 示例：`PlayerController.cs` 包含类 `PlayerController`

### 1.2 公共成员变量
- 使用 PascalCase（首字母大写）
- 使用 `[SerializeField]` 序列化私有字段以在 Inspector 中显示
- 添加 `[Tooltip("描述")]` 提供字段说明
- 使用 `[Header("分组名称")]` 对相关字段进行分组
- 示例：
```csharp
[Header("Movement Parameters")]
[Tooltip("玩家移动速度")]
[SerializeField] private float moveSpeed = 5f;
```

### 1.3 私有成员变量
- 使用 camelCase（首字母小写）
- 私有字段前缀使用下划线（可选）
- 示例：`private float _currentHealth;`

### 1.4 方法名
- 使用 PascalCase（首字母大写）
- 使用动词或动词短语
- 示例：`CalculateDamage()`, `UpdateScore()`

### 1.5 局部变量和参数
- 使用 camelCase（首字母小写）
- 示例：`int playerScore`, `string playerName`

## 2. 代码结构

### 2.1 脚本结构顺序
1. 版权和头部注释
2. using 指令
3. 命名空间（如需要）
4. 类注释
5. 类声明
6. 公共字段和属性
7. 私有字段
8. Unity 消息方法（按执行顺序）
   - `Reset()`
   - `Awake()`
   - `OnEnable()`
   - `Start()`
   - `Update()`
   - `FixedUpdate()`
   - `LateUpdate()`
   - `OnDisable()`
   - `OnDestroy()`
9. 公共方法
10. 私有方法
11. Unity 事件方法（如 `OnTriggerEnter()` 等）

### 2.2 使用 Unity 消息方法
- 只在需要时实现 Unity 消息方法
- 使用 `void` 作为返回类型
- 遵循 Unity 的生命周期方法命名

## 3. 注释规范

### 3.1 头部注释
每个脚本文件顶部应包含简要描述脚本功能的注释。

### 3.2 方法注释
- 公共方法应包含注释说明其功能、参数和返回值
- 使用 XML 文档注释格式
- 示例：
```csharp
/// <summary>
/// 计算并应用对玩家的伤害
/// </summary>
/// <param name="damageAmount">伤害值</param>
/// <param name="hitDirection">击中方向</param>
private void ApplyDamage(float damageAmount, Vector3 hitDirection)
{
    // 方法实现
}
```

### 3.3 内联注释
- 在复杂逻辑处添加注释
- 注释应解释"为什么"而不是"是什么"
- 避免过度注释显而易见的代码

## 4. Unity 特定规范

### 4.1 组件引用
- 使用 `[SerializeField]` 而不是 `public` 来暴露字段到 Inspector
- 在 `Start()` 或 `Awake()` 中检查必需组件的引用
- 使用 `GetComponent()` 获取组件引用时进行空值检查

### 4.2 性能考虑
- 避免在 `Update()` 中使用 `GetComponent()`
- 缓存组件引用
- 使用 `FixedUpdate()` 处理物理相关代码
- 使用对象池管理频繁创建和销毁的对象

### 4.3 调试和日志
- 使用 `Debug.Log()` 进行调试
- 在发布版本中移除或禁用调试日志
- 使用适当的日志级别（`Log`, `Warning`, `Error`）

## 5. 其他最佳实践

### 5.1 代码可读性
- 保持方法简短，单一职责
- 使用有意义的变量名
- 适当使用空行分隔代码块
- 保持一致的缩进（推荐 4 个空格）

### 5.2 错误处理
- 实现适当的错误检查和异常处理
- 在可能的情况下提供默认行为或优雅降级

### 5.3 代码复用
- 将通用功能提取到可复用的方法或类中
- 考虑使用继承或组合来实现代码复用

---
*此规范基于项目当前代码风格制定，可根据团队需求进行调整。*