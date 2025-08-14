using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    // TextMeshProUGUI组件引用
    [SerializeField]
    private TextMeshProUGUI statsText;
    
    // Image组件引用，用于显示燃油余量
    [Header("燃油显示")]
    [Tooltip("用于显示燃油余量的Image组件")]
    [SerializeField] private Image fuelImage;
    
    // 四个方向箭头的引用
    [Header("方向箭头")]
    [Tooltip("左方向箭头")]
    [SerializeField] private GameObject leftArrow;
    [Tooltip("右方向箭头")]
    [SerializeField] private GameObject rightArrow;
    [Tooltip("上方向箭头")]
    [SerializeField] private GameObject upArrow;
    [Tooltip("下方向箭头")]
    [SerializeField] private GameObject downArrow;
    
    // 速度阈值，只有当速度超过这个值时才显示箭头
    [Header("设置")]
    [Tooltip("速度阈值，只有当速度超过这个值时才显示箭头")]
    [SerializeField] private float speedThreshold = 0.1f;
    
    // 分隔符
    private const string SEPARATOR = "\n";
    
    void Start()
    {
        if (statsText == null)
        {
            Debug.LogError("未找到 TextMeshProUGUI 组件！");
        }
    }
    
    void Update()
    {
        // 更新UI显示
        if (statsText != null && GameManager.Instance != null && Lander.Instance != null)
        {
            // 格式化显示信息，只显示数字，每两个数字之间加上分隔符
            string displayText = $"{GameManager.Instance.CoinScore}" + SEPARATOR +
                                $"{GameManager.Instance.GameTime:F1}" + SEPARATOR +
                                $"{Lander.Instance.XVelocity:F2}" + SEPARATOR +
                                $"{Lander.Instance.YVelocity:F2}" + SEPARATOR +
                                $"{Lander.Instance.FuelAmount:F1}";
            
            statsText.text = displayText;
            
            // 控制方向箭头的显示
            UpdateDirectionArrows();
            
            // 更新燃油图像显示
            UpdateFuelImage();
        }
    }
    
    /// <summary>
    /// 更新方向箭头的显示状态
    /// </summary>
    private void UpdateDirectionArrows()
    {
        // 确保Lander实例存在
        if (Lander.Instance != null)
        {
            // 获取飞船的速度
            float xVelocity = Lander.Instance.XVelocity;
            float yVelocity = Lander.Instance.YVelocity;
            
            // 控制左右箭头的显示
            if (leftArrow != null)
            {
                leftArrow.SetActive(xVelocity < -speedThreshold);
            }
            
            if (rightArrow != null)
            {
                rightArrow.SetActive(xVelocity > speedThreshold);
            }
            
            // 控制上下箭头的显示
            if (upArrow != null)
            {
                upArrow.SetActive(yVelocity > speedThreshold);
            }
            
            if (downArrow != null)
            {
                downArrow.SetActive(yVelocity < -speedThreshold);
            }
        }
    }
    
    /// <summary>
    /// 更新燃油图像的显示
    /// </summary>
    private void UpdateFuelImage()
    {
        // 确保Image组件和Lander实例存在
        if (fuelImage != null && Lander.Instance != null)
        {
            // 计算燃油比例
            float fuelRatio = Lander.Instance.FuelAmount / Lander.Instance.InitialFuel;
            
            // 确保比例在0到1之间
            fuelRatio = Mathf.Clamp01(fuelRatio);
            
            // 更新Image的fillAmount
            fuelImage.fillAmount = fuelRatio;
        }
    }
}