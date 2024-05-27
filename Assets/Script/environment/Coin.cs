
using UnityEngine;

public class Coin : MonoBehaviour
{
    UpdateUI updateUI;
    void Start()
    {
        updateUI = FindObjectOfType<UpdateUI>();
                          }

    void Update()
    {
        // Logic cần cập nhật liên tục (nếu có)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DataManager.Instance.currentPlayer.CoinUp();
            updateUI.UpdateValue();
            Destroy(gameObject);    
        }
    }
}