using System;
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
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }

    public delegate void Event();
    public delegate GameObject GOEvent();

    public Action CreateItem;
    public Action<GameObject> AddTail;
    public Action GameOver;
    public Func<GameObject> CreateTail;
    public void CallOnCreateItem()
    {
        CreateItem?.Invoke();
    }

    public GameObject CallOnCreateTail()
    {
        return CreateTail?.Invoke();
    }

    public void CallOnAddTail(GameObject tail)
    {
        AddTail?.Invoke(tail);
    }

    public void CallOnGameOver()
    {
        GameOver?.Invoke();
    }




}
