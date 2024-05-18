using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

}
