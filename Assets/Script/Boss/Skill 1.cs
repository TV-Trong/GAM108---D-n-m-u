using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    
    void Start()
    {
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
            collision.SendMessage("TakeDamage", 10);
        }
    }
}
