
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    Boss bossCS;
    void Start()
    {
        bossCS = FindObjectOfType<Boss>();
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.Translate(Vector2.right * 10f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int damage;
            if (bossCS.EnrageMode())
            {
                damage = Random.Range(30, 45);
            }
            else
            {
                damage = Random.Range(15, 25);
            }
            collision.SendMessage("TakeDamage", damage);
        }
    }
}
