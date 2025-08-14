using UnityEngine;

public class Lander : MonoBehaviour
{
    // 单例实例
    public static Lander Instance { get; private set; }
    
    private Rigidbody2D rb;
    
    [Header("Thrust Parameters")]
    [Tooltip("主推进器的推力大小")]
    [SerializeField] private float thrustForce = 5f;
    [Tooltip("旋转推进器的旋转速度")]
    [SerializeField] private float rotationSpeed = 100f;
    
    [Header("Landing Parameters")]
    [Tooltip("最大允许降落速度")]
    [SerializeField] private float maxLandingSpeed = 2f;
    [Tooltip("最大允许降落角度（相对于垂直方向，单位：度）")]
    [SerializeField] private float maxLandingAngle = 15f;
    
    [Header("Fuel Parameters")]
    [Tooltip("初始燃油量")]
    [SerializeField] private float initialFuel = 100f;
    [Tooltip("燃油消耗速度（每秒消耗的燃油量）")]
    [SerializeField] private float fuelConsumptionRate = 10f;
    
    // 燃油系统
    private float currentFuel;
    
    // 输入状态
    private bool leftThrust;
    private bool rightThrust;
    private bool mainThrust;
    
    // 公共属性，用于暴露输入状态给粒子系统控制器
    public bool isLeftThrustActive => leftThrust;
    public bool isRightThrustActive => rightThrust;
    public bool isMainThrustActive => mainThrust;

    
    // 公共属性，用于暴露飞船速度信息
    public float XVelocity => rb != null ? rb.linearVelocity.x : 0f;
    public float YVelocity => rb != null ? rb.linearVelocity.y : 0f;
    
    // 公共属性，用于暴露燃油数量
    public float FuelAmount => currentFuel;
    
    // 公共属性，用于暴露初始燃油数量
    public float InitialFuel => initialFuel;
    
    /// <summary>
    /// 消耗指定数量的燃油
    /// </summary>
    /// <param name="amount">要消耗的燃油量</param>
    public void ConsumeFuel(float amount)
    {
        currentFuel -= amount;
        // 确保燃油量不会低于0
        if (currentFuel < 0)
        {
            currentFuel = 0;
        }
    }
    
    /// <summary>
    /// 增加指定数量的燃油
    /// </summary>
    /// <param name="amount">要增加的燃油量</param>
    public void AddFuel(float amount)
    {
        currentFuel += amount;
        // 确保燃油量不会超过初始值
        if (currentFuel > initialFuel)
        {
            currentFuel = initialFuel;
        }
    }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // 确保只有一个Lander实例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 获取Rigidbody2D组件引用
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("未找到 Rigidbody2D 组件！");
        }
        
        // 初始化燃油量
        currentFuel = initialFuel;
    }

    // Update is called once per frame
    // 用于处理输入检测
    void Update()
    {
        // 先检查燃油是否充足
        if (currentFuel > 0)
        {
            // 检查左箭头键或A键是否被按下
            leftThrust = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);

            // 检查右箭头键或D键是否被按下
            rightThrust = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

            // 检查上箭头键或W键是否被按下
            mainThrust = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
            
            // 检查是否有任何按键按下，如果有则消耗燃油
            if (leftThrust || rightThrust || mainThrust)
            {
                ConsumeFuel(fuelConsumptionRate * Time.deltaTime);
            }
        }
        else
        {
            // 燃油不足时，重置所有推力状态
            leftThrust = false;
            rightThrust = false;
            mainThrust = false;
        }
    }

    // FixedUpdate以固定时间间隔调用，适合物理更新
    void FixedUpdate()
    {
        // 应用左转力矩
        if (leftThrust && rb != null)
        {
            rb.AddTorque(rotationSpeed * Time.fixedDeltaTime);
        }

        // 应用右转力矩
        if (rightThrust && rb != null)
        {
            rb.AddTorque(-rotationSpeed * Time.fixedDeltaTime);
        }

        // 应用主推力
        if (mainThrust && rb != null)
        {
            rb.AddForce(transform.up * thrustForce);
        }
    }

    // 碰撞检测
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞到的物体上是否有 LandingPad 脚本
        LandingPad landingPad = collision.gameObject.GetComponent<LandingPad>();
        if (landingPad != null)
        {
            Debug.Log("碰撞到了 Landing Pad!");
            
            // 获取飞船的速度和角度
            Vector2 velocity = collision.relativeVelocity;
            float speed = velocity.magnitude;
            float angle = Vector2.Angle(Vector2.up, transform.up);

            // 打印降落速度和角度，方便调试
            Debug.Log($"降落速度: {speed:F2}, 降落角度: {angle:F2}°");

            // 检查降落速度和角度是否超出限制
            if (speed > maxLandingSpeed)
            {
                Debug.Log($"降落失败：速度过快！当前速度：{speed:F2}，最大允许速度：{maxLandingSpeed:F2}");
                // 这里可以添加爆炸效果或游戏结束逻辑
            }
            else if (angle > maxLandingAngle)
            {
                Debug.Log($"降落失败：角度过于陡峭！当前角度：{angle:F2}°，最大允许角度：{maxLandingAngle:F2}°");
                // 这里可以添加爆炸效果或游戏结束逻辑
            }
            else
            {
                // 安全降落，计算并输出得分
                CalculateAndLogLandingScore(speed, angle);
                Debug.Log($"安全降落！速度：{speed:F2}，角度：{angle:F2}°");
                // 这里可以添加成功降落的逻辑
            }
        }
        else
        {
            // 碰撞到的不是 Landing Pad，飞船坠毁
            Debug.Log("飞船坠毁！碰撞到了非 Landing Pad 物体。");
            // 这里可以添加爆炸效果或游戏结束逻辑
        }
    }

    // 计算并输出降落得分
    private void CalculateAndLogLandingScore(float speed, float angle)
    {
        // 计算角度得分 (满分100分)
        float angleScore = 0f;
        if (angle < maxLandingAngle)
        {
            angleScore = 100f * (1f - angle / maxLandingAngle);
        }

        // 计算速度得分 (满分100分)
        float speedScore = 0f;
        if (speed < maxLandingSpeed)
        {
            speedScore = 100f * (1f - speed / maxLandingSpeed);
        }

        // 输出得分
        Debug.Log($"角度得分: {angleScore:F2}, 速度得分: {speedScore:F2}");
    }
}
