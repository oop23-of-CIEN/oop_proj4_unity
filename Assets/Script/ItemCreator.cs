using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreator : MonoBehaviour
{
    [SerializeField] private float _up;
    [SerializeField] private float _down;
    [SerializeField] private float _left;
    [SerializeField] private float _right;

    [SerializeField] private float _distance;

    [SerializeField] private GameObject _Item;
    [SerializeField] private Transform _head;

    public void CreateItem()
    {
        Vector3 position = new Vector3(Random.Range(_left, _right), Random.Range(_down, _up), 0);
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

        GameObject item = Instantiate(_Item);
        item.transform.position = position;
    }

}
