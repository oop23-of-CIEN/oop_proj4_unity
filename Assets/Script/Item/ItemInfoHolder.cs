using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoHolder : MonoBehaviour
{
    [SerializeField] GameObject[] tails;
    [SerializeField] int tailNumber;

    public GameObject getTail { get { return Instantiate(tails[tailNumber]);} }
}
