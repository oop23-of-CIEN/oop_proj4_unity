using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailInstatntiater : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tails;

    private void Start()
    {
        EventManager.Instance.CreateTail += GetNewTail;
        Debug.Log("����");
    }

    private void OnDestroy()
    {
        EventManager.Instance.CreateTail -= null;
    }

    public GameObject GetNewTail()
    {
        Debug.Log("���� �߰�");
        return Instantiate(_tails[Random.Range(0, _tails.Count)]);
    }

    

}
