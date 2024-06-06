using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlayer : MonoBehaviour
{
    public void RemovePlayerFromList()
    {
        DataManager.Instance.DeletePlayer();
    }
}
