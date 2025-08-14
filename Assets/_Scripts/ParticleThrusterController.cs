using UnityEngine;

public class ParticleThrusterController : MonoBehaviour
{
    [Header("Thruster Particle Systems")]
    [Tooltip("左推进器粒子系统")]
    public ParticleSystem leftThruster;
    
    [Tooltip("中推进器粒子系统")]
    public ParticleSystem middleThruster;
    
    [Tooltip("右推进器粒子系统")]
    public ParticleSystem rightThruster;
    
    [Header("Rocket Controller")]
    [Tooltip("火箭控制组件引用")]
    public Lander lander;
    
    // Start is called before the first frame update
    void Start()
    {
        // 验证引用是否已设置
        if (leftThruster == null)
        {
            Debug.LogError("未设置左推进器粒子系统引用！", this);
        }
        
        if (middleThruster == null)
        {
            Debug.LogError("未设置中推进器粒子系统引用！", this);
        }
        
        if (rightThruster == null)
        {
            Debug.LogError("未设置右推进器粒子系统引用！", this);
        }
        
        if (lander == null)
        {
            Debug.LogError("未设置火箭控制组件引用！", this);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // 控制粒子系统的播放和停止
        ControlParticleSystems();
    }
    
    /// <summary>
    /// 根据输入状态控制粒子系统的播放和停止
    /// </summary>
    private void ControlParticleSystems()
    {
        // 检查引用是否有效
        if (lander == null)
            return;
        
        // 获取输入状态
        bool isLeftThrust = lander.isLeftThrustActive;
        bool isRightThrust = lander.isRightThrustActive;
        bool isMainThrust = lander.isMainThrustActive;
        
        // 根据控制逻辑控制粒子系统的播放和停止
        if (isMainThrust)
        {
            // 按下前进键
            if (isLeftThrust && !isRightThrust)
            {
                // 前进+左键：播放左和中粒子系统，停止右粒子系统
                PlayParticleSystem(leftThruster);
                PlayParticleSystem(middleThruster);
                StopParticleSystem(rightThruster);
            }
            else if (isRightThrust && !isLeftThrust)
            {
                // 前进+右键：播放右和中粒子系统，停止左粒子系统
                PlayParticleSystem(rightThruster);
                PlayParticleSystem(middleThruster);
                StopParticleSystem(leftThruster);
            }
            else
            {
                // 仅前进键：播放所有三个粒子系统
                PlayParticleSystem(leftThruster);
                PlayParticleSystem(middleThruster);
                PlayParticleSystem(rightThruster);
            }
        }
        else
        {
            // 未按下前进键
            if (isLeftThrust && !isRightThrust)
            {
                // 仅左键：播放左粒子系统，停止右粒子系统
                PlayParticleSystem(leftThruster);
                StopParticleSystem(rightThruster);
                // 中粒子系统保持当前状态
            }
            else if (isRightThrust && !isLeftThrust)
            {
                // 仅右键：播放右粒子系统，停止左粒子系统
                PlayParticleSystem(rightThruster);
                StopParticleSystem(leftThruster);
                // 中粒子系统保持当前状态
            }
            else if (!isLeftThrust && !isRightThrust)
            {
                // 无按键：停止所有粒子系统
                StopParticleSystem(leftThruster);
                StopParticleSystem(middleThruster);
                StopParticleSystem(rightThruster);
            }
            // 其他情况（左右键同时按下）保持当前状态
        }
    }
    
    /// <summary>
    /// 播放粒子系统
    /// </summary>
    /// <param name="particleSystem">要播放的粒子系统</param>
    private void PlayParticleSystem(ParticleSystem particleSystem)
    {
        if (particleSystem != null && !particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
    }
    
    /// <summary>
    /// 停止粒子系统
    /// </summary>
    /// <param name="particleSystem">要停止的粒子系统</param>
    private void StopParticleSystem(ParticleSystem particleSystem)
    {
        if (particleSystem != null && particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }
    }
}