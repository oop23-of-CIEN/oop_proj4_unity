using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstantiater : MonoBehaviour
{
    private void Awake()
    {
        EventManager.Instance.CreateItem += CreateItem;

    }

    private void OnDestroy()
    {
        EventManager.Instance.CreateItem -= CreateItem;
    }

    private void CreateItem()
    {

    }
}
