using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemValue : MonoBehaviour
{
    [SerializeField]private int _value;
    public int GetValue() { return _value; }

    public void SetValue(int v) { _value = v; }
}
