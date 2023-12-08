using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyInstatntiater : MonoBehaviour
{
    [SerializeField] private List<GameObject> _jellys;

    public GameObject GetNewJelly()
    {

        return Instantiate(_jellys[Random.Range(0, _jellys.Count)]);
    }
}
