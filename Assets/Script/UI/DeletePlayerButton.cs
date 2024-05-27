using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeletePlayerButton : MonoBehaviour
{
    GameObject blackScreenPanel;
    Scrollbar scrollbar;

    public void BlackScreen(int scale)
    {
        blackScreenPanel = GameObject.Find("BlackScreen");
        blackScreenPanel.transform.localScale = new Vector3(scale, scale, scale);
    }
    public void ScrollbarEnable(bool isActive)
    {
        scrollbar = GetComponentInParent<Scrollbar>();
        scrollbar.enabled = isActive;
    }

}
