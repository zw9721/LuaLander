using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // 单例实例
    public static GameManager Instance { get; private set; }
    
    // 金币得分
    [Tooltip("玩家当前的金币得分")]
    private int coinScore = 0;
    
    // 公共属性，用于访问金币得分
    public int CoinScore { get { return coinScore; } }
    
    // 游戏时间（以秒为单位）
    [Tooltip("游戏时间（以秒为单位）")]
    private float gameTimer = 0f;
    
    // 公共属性，用于访问游戏时间
    public float GameTime { get { return gameTimer; } }
    
    // 定义拾取金币事件
    public event Action<int> OnCoinPickedUp;
    
    void Awake()
    {
        // 确保只有一个GameManager实例
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
    
    void Start()
    {
        // 监听拾取金币事件
        OnCoinPickedUp += AddCoinsToScore;
    }
    
    void Update()
    {
        // 更新游戏时间
        gameTimer += Time.deltaTime;
    }
    
    // 触发拾取金币事件
    public void PickupCoin(int coinValue)
    {
        OnCoinPickedUp?.Invoke(coinValue);
    }
    
    // 增加金币到得分
    private void AddCoinsToScore(int coinValue)
    {
        coinScore += coinValue;
        Debug.Log($"拾取了 {coinValue} 个金币！当前总分: {coinScore}");
    }
}