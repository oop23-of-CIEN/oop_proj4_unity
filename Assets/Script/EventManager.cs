using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager _instance;  // �̱���
    public static EventManager Instance => _instance;
    public void Awake()
    {
        if (_instance == null) // �̱��� ���
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }

    //Ŭ���� ���� ������ ���� Event�� : Ŭ���� ��� ������ ��ȭ�� �߻��ϰų� �Է��� ������ �ش� ������ �ٸ� Ŭ������ ����

    // �÷��̾� �Է¿� ���� ���� �Լ�

    // Item �ű� ����
    public Action CreateItem; 
    // �÷��̾� ���� �߰�
    public Action<GameObject> AddTail;
    // ���� ����
    public Action GameOver;
    // �ְ� ���� ������Ʈ
    public Action<int> UpdateScore;
    //���� ������ ����
    public Func<GameObject> CreateTail;
    // �÷��̾� 
    public Func<Transform> GetHeadPos;
    // �ְ� ���� ��������
    public Func<int> GetHighestScore;


    //�̺�Ʈ ȣ��
    public void CallOnCreateItem()
    {
        CreateItem?.Invoke();
    }
    public GameObject CallOnCreateTail()
    {
        return CreateTail?.Invoke();
    }
    public void CallOnGameOver()
    {
        GameOver?.Invoke();
    }

    public void CallOnAddTail(GameObject tail)
    {
        AddTail?.Invoke(tail);
    }

    public Transform CallOnGetHeadPos()
    {
        return GetHeadPos?.Invoke();
    }

    public void CallOnUpdateScore(int score)
    {
        UpdateScore?.Invoke(score);
    }

    public int CallOnGetHighestScore()
    {
        return (int)(GetHighestScore?.Invoke());
    }

}
