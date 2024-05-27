
using Player;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    // Lượng máu hồi lại khi nhân vật tiếp xúc với vật phẩm
    public int healingAmount = 20;

    // Hàm này sẽ được gọi khi một collider khác chạm vào collider của vật phẩm này
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem collider kia có thuộc về đối tượng Player hay không
        if (other.CompareTag("Player"))
        {
            // Lấy component Health từ đối tượng Player
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // Hồi lại máu cho nhân vật
                playerMovement.Heal(healingAmount);

                // Huỷ vật phẩm sau khi sử dụng
                Destroy(gameObject);
            }
        }
    }
}