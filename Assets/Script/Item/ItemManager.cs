using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _items = new List<GameObject>();
    // ������ ���� ���� range
    [SerializeField] private float _up;
    [SerializeField] private float _down;
    [SerializeField] private float _left;
    [SerializeField] private float _right;
    [SerializeField] private float _distance;


    private GameObject _item;

    private void Start()
    {
        //�̺�Ʈ ���
        EventManager.Instance.CreateItem += CreateItem;
        EventManager.Instance.GameOver += GameOver;
        //�ʱ� ������ ����
        CreateItem();
    }

    private void OnDisable()
    {
        //�̺�Ʈ ����
        EventManager.Instance.CreateItem -= CreateItem;
        EventManager.Instance.GameOver -= GameOver;
    }

    //������ ������ ����
    private GameObject GetItem()
    {
        var index = Random.Range(0, _items.Count);
        GameObject i = Instantiate(_items[index]);
        return i;
    }

    //������ ����, ���� ��ġ�� ��ġ
    public void CreateItem()
    {
        Vector3 position = new Vector3(Random.Range(_left, _right), Random.Range(_down, _up), 0);

        //���� �÷��̾��� ��ġ���� �Ÿ��� ���� ���Ϸ� ����� ��� ��ġ ����
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
        //���� ���� �� ���� �����ϴ� ������ ������Ʈ �ı�
        if (_item != null) { Destroy(_item); }
    }


}
    