using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager _instance;  // 싱글톤
    public static EventManager Instance => _instance;
    public void Awake()
    {
        if (_instance == null) // 싱글톤 등록
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }

    //클래스 간의 소통을 위한 Event들 : 클래스 멤버 변수에 변화가 발생하거나 입력이 들어오면 해당 정보를 다른 클래스로 전달

    // 플레이어 입력에 대한 반응 함수

    // Item 신규 생성
    public Action CreateItem; 
    // 플레이어 꼬리 추가
    public Action<GameObject> AddTail;
    // 게임 오버
    public Action GameOver;
    // 최고 점수 업데이트
    public Action<int> UpdateScore;
    //꼬리 프리팹 생성
    public Func<GameObject> CreateTail;
    // 플레이어 
    public Func<Transform> GetHeadPos;
    // 최고 점수 가져오기
    public Func<int> GetHighestScore;


    //이벤트 호출
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
