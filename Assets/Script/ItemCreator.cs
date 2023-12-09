using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreator : MonoBehaviour
{
    [SerializeField] private float _up;
    [SerializeField] private float _down;
    [SerializeField] private float _left;
    [SerializeField] private float _right;

    /// <summary>
    /// 경계 위치 보정용 변수
    /// </summary>
    [SerializeField] private Vector3 _positionDiff;
    [SerializeField] GameObject _wall;
    [SerializeField] private float _distance;

    [SerializeField] private GameObject[] _Item;
    [SerializeField] private Transform _head;

    public void CreateItem()
    {
        Vector3 position = new Vector3(Random.Range(_left, _right), Random.Range(_down, _up), 0) + _positionDiff;
        Debug.Log(position);
        Debug.Log(Vector3.Magnitude(position - _head.position));

        if(Vector3.Magnitude(position - _head.position) < _distance + 0.01f)
        {
            position += new Vector3(_distance, 0, 0);
            if(position.x > _right)
            {
                position = new Vector3(_left + 0.5f, position.y, 0);
            }
        }
        //랜덤한 아이템 출력
        int randN = Random.Range(0, 3);
        GameObject item = Instantiate(_Item[randN]);
        item.transform.position = position;
    }

    private void Start()
    {
        _positionDiff = _wall.transform.position;
        Vector3[] coners = { new Vector3(_left, _up, 0) + _positionDiff,
                             new Vector3(_right, _up, 0) + _positionDiff,
                             new Vector3(_right, _down, 0) + _positionDiff,
                             new Vector3(_left, _down, 0) + _positionDiff};
        Debug.DrawLine(coners[0], coners[1], Color.red, Mathf.Infinity);
        Debug.DrawLine(coners[1], coners[2], Color.red, Mathf.Infinity);
        Debug.DrawLine(coners[2], coners[3], Color.red, Mathf.Infinity);
        Debug.DrawLine(coners[3], coners[0], Color.red, Mathf.Infinity);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            CreateItem();
        }
    }
}
