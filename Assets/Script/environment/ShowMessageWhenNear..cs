
using UnityEngine;

public class ShowMessageWhenNear : MonoBehaviour
{
    public GameObject object1; // Rương
    public GameObject object2; // Thông báo "Press E to open"
    public float detectionRadius = 5f; // Bán kính phát hiện khoảng cách

    private Transform player; // Biến để lưu vị trí người chơi

    void Start()
    {
        // Tìm đối tượng người chơi trong scene (cần tag người chơi là "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Ẩn thông báo khi bắt đầu
        if (object2 != null)
        {
            object2.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null || object1 == null || object2 == null)
        {
            return;
        }

        // Tính khoảng cách giữa người chơi và object1 (rương)
        float distance = Vector3.Distance(player.position, object1.transform.position);

        // Nếu khoảng cách nhỏ hơn bán kính phát hiện, hiển thị object2
        if (distance < detectionRadius)
        {
            object2.SetActive(true);
        }
        else
        {
            object2.SetActive(false);
        }
    }
}
