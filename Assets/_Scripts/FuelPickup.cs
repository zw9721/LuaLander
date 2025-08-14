using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    [Header("Fuel Pickup Parameters")]
    [Tooltip("碰撞时补给的燃油量")]
    [SerializeField] private float fuelAmount = 25f;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // 检查碰撞的物体上是否有Lander组件
        Lander lander = other.GetComponent<Lander>();
        if (lander != null)
        {
            // 补给燃油
            lander.AddFuel(fuelAmount);            
            // 销毁燃油补给物体
            Destroy(gameObject);
        }
    }
}