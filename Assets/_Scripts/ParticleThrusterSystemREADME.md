# 火箭粒子推进系统使用说明

## 1. 概述

本系统为火箭添加了视觉粒子推进效果，与物理推进系统分离。系统包含三个独立的粒子系统（左、中、右），并根据用户的按键输入控制这些粒子系统的播放和停止。

## 2. 组件说明

### 2.1 Lander.cs
已修改以暴露输入状态：
- `isLeftThrustActive`: 左推进器激活状态
- `isRightThrustActive`: 右推进器激活状态
- `isMainThrustActive`: 主推进器激活状态

### 2.2 ParticleThrusterController.cs
粒子推进系统管理器，负责控制三个粒子系统的播放和停止。

## 3. 设置步骤

### 3.1 添加ParticleThrusterController组件
1. 在Unity编辑器中，选择场景中的火箭对象（Lander）
2. 在Inspector窗口中点击"Add Component"按钮
3. 搜索并添加"Particle Thruster Controller"组件

### 3.2 设置组件引用
在Inspector窗口中，为ParticleThrusterController组件设置以下引用：

1. **Left Thruster**: 
   - 将火箭对象的左推进器粒子系统拖拽到此字段
   - 通常命名为"ThrusterParticleSystem (1)"

2. **Middle Thruster**: 
   - 将火箭对象的中推进器粒子系统拖拽到此字段
   - 通常命名为"ThrusterParticleSystem"

3. **Right Thruster**: 
   - 将火箭对象的右推进器粒子系统拖拽到此字段
   - 通常命名为"ThrusterParticleSystem (2)"

4. **Lander**: 
   - 将火箭对象的Lander组件拖拽到此字段
   - 或者点击圆圈图标，从弹出的窗口中选择Lander组件

## 4. 控制逻辑

| 按键组合 | 播放的粒子系统 | 停止的粒子系统 |
|---------|---------------|---------------|
| 仅前进键 (W/↑) | 左、中、右 | 无 |
| 仅左键 (A/←) | 左 | 右 |
| 仅右键 (D/→) | 右 | 左 |
| 前进+左键 | 左、中 | 右 |
| 前进+右键 | 右、中 | 左 |
| 无按键 | 无 | 左、中、右 |

## 5. 注意事项

1. 确保所有引用都已正确设置，否则系统将无法正常工作
2. 粒子系统会自动播放和停止，无需手动控制
3. 如果需要调整粒子效果，请修改ThrusterParticleSystem预制体