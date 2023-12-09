using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _items = new List<GameObject>();
    // 아이템 생성 범위 range
    [SerializeField] private float _up;
    [SerializeField] private float _down;
    [SerializeField] private float _left;
    [SerializeField] private float _right;
    [SerializeField] private float _distance;


    private GameObject _item;

    private void Start()
    {
        //이벤트 등록
        EventManager.Instance.CreateItem += CreateItem;
        EventManager.Instance.GameOver += GameOver;
        //초기 아이템 생성
        CreateItem();
    }

    private void OnDisable()
    {
        //이벤트 해제
        EventManager.Instance.CreateItem -= CreateItem;
        EventManager.Instance.GameOver -= GameOver;
    }

    //아이템 프리팹 생성
    private GameObject GetItem()
    {
        var index = Random.Range(0, _items.Count);
        GameObject i = Instantiate(_items[index]);
        return i;
    }

    //아이템 생성, 랜덤 위치에 배치
    public void CreateItem()
    {
        Vector3 position = new Vector3(Random.Range(_left, _right), Random.Range(_down, _up), 0);

        //현재 플레이어의 위치와의 거리가 기준 이하로 가까울 경우 위치 조정
        if (Vector3.Magnitude(position - EventManager.Instance.CallOnGetHeadPos().position) < _distance + 0.01f)
        {
            position += new Vector3(_distance, 0, 0);
            if (position.x > _right)
            {
                position = new Vector3(_left + 0.5f, position.y, 0);
            }
        }

        GameObject item = GetItem();
        _item = item;
        item.transform.position = position;

    }

    private void GameOver()
    {
        //게임 종료 시 현재 존재하는 아이템 오브젝트 파괴
        if (_item != null) { Destroy(_item); }
    }


}
    