using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailInstatntiater : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tails;

    private void Awake()
    {
        EventManager.Instance.CreateTail += GetNewTail;
    }

    private void OnDestroy()
    {
        EventManager.Instance.CreateTail -= GetNewTail;
    }

    public GameObject GetNewTail()
    {
        return Instantiate(_tails[Random.Range(0, _tails.Count)]);
    }
}
