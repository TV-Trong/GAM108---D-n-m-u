using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    TextMeshProUGUI lifeText;
    private void Start()
    {
        lifeText = GetComponentInChildren<TextMeshProUGUI>();
        lifeText.text = "X " + DataManager.Instance.currentPlayer.life.ToString();
        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
