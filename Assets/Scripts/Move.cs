using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    bool isTurnClockwise = true;

    [SerializeField, Tooltip("Head obj")]
    GameObject head;

    [SerializeField, Tooltip("Last tail obj")]
    GameObject lastTail;

    [SerializeField, Tooltip("Tail objs")]
    List<GameObject> tails;

    Dictionary<float, Vector2> timeNRotPoint;

    Transform headTrans;
    /// <summary>
    /// ratation point when game start
    /// </summary>
    Vector2 startRotationPos = Vector2.zero;

    Vector2 currentRotationPoint;
    float flownTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Moving()
    {
        //���ο� ȸ�� �߽� ���
        Vector2 newPoint = currentRotationPoint + 2 * ((Vector2)head.GetComponent<Transform>().transform.position - currentRotationPoint);
        //���� ȸ�� �߽��� ���� ȸ�� �߽����� ����
        timeNRotPoint.Add(flownTime, newPoint);
        //ȸ�� �߽� ����
        currentRotationPoint = newPoint;
        //���� �ٲٱ�
        isTurnClockwise = isTurnClockwise ? false : true;
    }
}
