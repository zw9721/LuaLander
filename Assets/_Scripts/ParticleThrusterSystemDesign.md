# 火箭粒子推进系统设计方案

## 1. 概述

本方案设计了一个与火箭物理推进系统分离的粒子推进系统。该系统包含三个独立的粒子系统（左、中、右），并根据用户的按键输入控制这些粒子系统的播放和停止。

## 2. 系统架构

### 2.1 核心组件

1. **ParticleThrusterController** - 粒子推进系统管理器组件
2. **三个粒子系统** - 使用现有的推进器粒子系统预制体
3. **Lander** - 现有的火箭控制组件

### 2.2 组件关系

```
mermaid
graph TD
    A[Rocket GameObject] --> B[Lander Component]
    A --> C[ParticleThrusterController Component]
    C --> D[Left Thruster Particle System]
    C --> E[Middle Thruster Particle System]
    C --> F[Right Thruster Particle System]
```

### 2.3 数据流

1. Lander组件检测用户输入并存储在公共变量中
2. ParticleThrusterController组件每帧读取Lander的输入状态
3. ParticleThrusterController根据预定义的控制逻辑控制粒子系统的播放和停止

## 3. 控制逻辑

| 按键组合 | 播放的粒子系统 | 停止的粒子系统 |
|---------|---------------|---------------|
| 仅前进键 | 左、中、右 | 无 |
| 仅左键 | 左 | 右 |
| 仅右键 | 右 | 左 |
| 前进+左键 | 左、中 | 右 |
| 前进+右键 | 右、中 | 左 |
| 无按键 | 无 | 左、中、右 |

## 4. 组件设计

### 4.1 ParticleThrusterController组件

#### 4.1.1 属性
- `leftThruster`: ParticleSystem - 左推进器粒子系统引用
- `middleThruster`: ParticleSystem - 中推进器粒子系统引用
- `rightThruster`: ParticleSystem - 右推进器粒子系统引用
- `lander`: Lander - 火箭控制组件引用

#### 4.1.2 生命周期方法
- `Start()`: 获取组件引用
- `Update()`: 每帧检查输入状态并控制粒子系统

#### 4.1.3 核心方法
- `ControlParticleSystems()`: 根据输入状态控制粒子系统的播放和停止

### 4.2 Lander组件修改

为了使ParticleThrusterController能够访问输入状态，需要在Lander组件中添加以下公共属性：
- `isLeftThrustActive`: bool - 左推进器激活状态
- `isRightThrustActive`: bool - 右推进器激活状态
- `isMainThrustActive`: bool - 主推进器激活状态

## 5. 实现计划

1. 修改Lander脚本，添加公共属性以暴露输入状态
2. 创建ParticleThrusterController脚本
3. 在场景中为火箭对象添加ParticleThrusterController组件
4. 在Inspector中为ParticleThrusterController组件分配三个推进器粒子系统的引用
5. 在Inspector中为ParticleThrusterController组件分配Lander组件的引用
6. 测试粒子推进系统，确保控制逻辑正确实现