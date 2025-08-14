using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [Header("Coin Pickup Parameters")]
    [Tooltip("拾取时获得的金币数量")]
    [SerializeField] private int coinValue = 1;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // 检查碰撞的物体上是否有Lander组件
        Lander lander = other.GetComponent<Lander>();
        if (lander != null)
        {
            // 通过GameManager触发拾取金币事件
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PickupCoin(coinValue);
            }
            
            // 销毁金币物体
            Destroy(gameObject);
        }
    }
}