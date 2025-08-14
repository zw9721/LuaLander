# Unity 项目命名规范

## 1. 文件和文件夹命名

### 1.1 脚本文件
- 使用 PascalCase（首字母大写）
- 文件名应与类名完全一致
- 示例：`PlayerController.cs`

### 1.2 场景文件
- 使用 PascalCase（首字母大写）
- 添加描述性名称
- 示例：`MainMenu.unity`, `Level01.unity`

### 1.3 Prefab 文件
- 使用 PascalCase（首字母大写）
- 添加描述性名称
- 示例：`PlayerShip.prefab`, `EngineParticleSystem.prefab`

### 1.4 材质文件
- 使用 PascalCase（首字母大写）
- 添加描述性名称
- 示例：`PlayerShipMaterial.mat`, `GroundTexture.mat`

### 1.5 纹理文件
- 使用 PascalCase（首字母大写）
- 添加描述性名称
- 示例：`PlayerShipDiffuse.png`, `GroundAlbedo.tga`

### 1.6 文件夹结构
- 使用 PascalCase（首字母大写）
- 使用描述性名称
- 示例：
  ```
  Assets/
  ├── Scripts/
  ├── Scenes/
  ├── Prefabs/
  ├── Materials/
  ├── Textures/
  └── Audio/
  ```

## 2. GameObject 命名

### 2.1 场景中的 GameObject
- 使用描述性名称
- 使用 PascalCase（首字母大写）
- 示例：`PlayerShip`, `MainCamera`, `DirectionalLight`

### 2.2 UI 元素
- 使用前缀标识类型
- 示例：
  - `btn` - 按钮：`btnStart`, `btnExit`
  - `txt` - 文本：`txtScore`, `txtPlayerName`
  - `img` - 图像：`imgHealthBar`, `imgLogo`
  - `panel` - 面板：`panelMainMenu`, `panelSettings`

## 3. 组件和变量命名

### 3.1 组件变量
- 使用描述性名称
- 使用 camelCase（首字母小写）
- 示例：
  ```csharp
  private Rigidbody2D rb;
  private ParticleSystem leftThruster;
  private AudioSource engineSound;
  ```

### 3.2 标签 (Tags)
- 使用 PascalCase（首字母大写）
- 示例：`Player`, `Enemy`, `Collectible`

### 3.3 层 (Layers)
- 使用 PascalCase（首字母大写）
- 示例：`Player`, `Enemy`, `IgnoreRaycast`

## 4. 资源命名

### 4.1 动画文件
- 使用描述性名称
- 示例：`Player_Run.anim`, `Door_Open.anim`

### 4.2 动画控制器
- 使用描述性名称
- 示例：`PlayerAnimator.controller`

### 4.3 音频文件
- 使用描述性名称
- 示例：`EngineLoop.wav`, `ExplosionSFX.mp3`

## 5. 版本控制命名

### 5.1 临时文件
- 在提交前删除临时或测试文件
- 避免将临时文件提交到版本控制

### 5.2 分支命名
- 使用描述性名称
- 使用 kebab-case（小写字母，连字符分隔）
- 示例：`feature/player-controller`, `bugfix/collision-detection`

---
*此命名规范旨在提高项目的一致性和可维护性。*