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
        //새로운 회전 중심 계산
        Vector2 newPoint = currentRotationPoint + 2 * ((Vector2)head.GetComponent<Transform>().transform.position - currentRotationPoint);
        //현재 회전 중심을 이전 회전 중심으로 저장
        timeNRotPoint.Add(flownTime, newPoint);
        //회전 중심 변경
        currentRotationPoint = newPoint;
        //방향 바꾸기
        isTurnClockwise = isTurnClockwise ? false : true;
    }
}
