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
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }

    public delegate void Event();
    public delegate GameObject GOEvent();

    public Event CreateItem;
    public GOEvent CreateTail;

    public void CallOnCreateItem()
    {
        CreateItem?.Invoke();
    }

    public GameObject CallOnCreateTail()
    {
        return CreateTail?.Invoke();
    }



}
