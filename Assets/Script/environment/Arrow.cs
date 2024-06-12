using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;



    private float damage;
    private void Start()
    {
        damage = 1;
        Destroy(gameObject, 0.5f);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyMain enemy = collision.gameObject.GetComponent<EnemyMain>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        if (Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Ground")))
        {
            Destroy(gameObject);
        }
    }

}
