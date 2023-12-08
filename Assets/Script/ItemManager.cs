using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _items = new List<GameObject>();
    ///range
    [SerializeField] private float _up;
    [SerializeField] private float _down;
    [SerializeField] private float _left;
    [SerializeField] private float _right;
    [SerializeField] private float _distance;

    [SerializeField] private Transform _head;
    private void Start()
    {
        EventManager.Instance.CreateItem += CreateItem;
        EventManager.Instance.UseItem += UseItem;

    }

    private void OnDestroy()
    {
        EventManager.Instance.CreateItem -= CreateItem;
        EventManager.Instance.UseItem -= UseItem;
    }

    private GameObject GetItem()
    {
        var index = Random.Range(0, _items.Count);
        GameObject i = Instantiate(_items[index]);
        i.GetComponent<ItemValue>().SetValue(index+1);
        Debug.Log("Item index is " + i);
        return i;
    }
    public void CreateItem()
    {
        Vector3 position = new Vector3(Random.Range(_left, _right), Random.Range(_down, _up), 0);
       

        if (Vector3.Magnitude(position - _head.position) < _distance + 0.01f)
        {
            position += new Vector3(_distance, 0, 0);
            if (position.x > _right)
            {
                position = new Vector3(_left + 0.5f, position.y, 0);
            }
        }

        GameObject item = GetItem();
        item.transform.position = position;

    }

    public void UseItem(GameObject item)
    {
        
    }

}
    