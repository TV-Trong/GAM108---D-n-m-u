
using UnityEngine;

public class Coin : MonoBehaviour
{
    void Start()
    {
        // Thiết lập ban đầu nếu cần
    }

    void Update()
    {
        // Logic cần cập nhật liên tục (nếu có)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);    
        }
    }
}