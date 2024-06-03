
using UnityEngine;

public class Coin : MonoBehaviour
{
    UpdateUI updateUI;
    private AudioSource speaker;
    void Start()
    {
        updateUI = FindObjectOfType<UpdateUI>();
        speaker = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DataManager.Instance.currentPlayer.CoinUp();
            DataManager.Instance.currentPlayer.LifeUp();
            updateUI.UpdateValue();
            PickUp();
            Destroy(gameObject, speaker.clip.length);    
        }
    }

    private void PickUp()
    {   
        speaker.Play();
    }
}