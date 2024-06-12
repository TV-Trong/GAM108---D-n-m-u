
using UnityEngine;
using UnityEngine.UI;

public class BossSlider : MonoBehaviour
{
    Slider bossSlider;
    Boss bossCS;
    private void Start()
    {
        bossCS = FindObjectOfType<Boss>();
        bossSlider = GetComponent<Slider>();
    }
    private void Update()
    {
        bossSlider.value = bossCS.health / 35;
    }
}
