using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    private static EventManager _instance;


    public static EventManager Instance => _instance;


    public void Awake()
    {
        if (_instance == null)
        {
            _instance = new EventManager();
        }
        else Destroy(this);
    }

    public delegate void Event();

    public Event CreateItem;

    public void CallOnCreateItem()
    {
        CreateItem?.Invoke();
    }



}
