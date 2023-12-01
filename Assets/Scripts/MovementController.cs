using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MovementController : MonoBehaviour
{
    /*
    //minimum time between each turn
    float turnInterval;
    //flown time after last turn
    float currentInterval;
    */
    //truning clockwise?
    bool isTurnClockwise = true;

    [SerializeField, Range(0f, 360f), Tooltip("degree/sec")]
    float rotationSpeed;

    [SerializeField, Range(0f,2f), Tooltip("Rotation Radius")]
    float radius;

    [SerializeField, Tooltip("Head obj")]
    GameObject head;

    [SerializeField, Tooltip("Tail objs")]
    List<GameObject> tails;

    Dictionary<float, Vector2> timeNRotPoint = new Dictionary<float, Vector2>();

    [SerializeField]
    Transform headTrans;
    /// <summary>
    /// ratation point when game start
    /// </summary>
    Vector2 startRotationPos = Vector2.zero;

    Vector2 currentRotationPoint;
    float flownTime;
    // Start is called before the first frame update
    void OnEnable()
    {
        //float headRadius = head.GetComponent<CircleCollider2D>().radius;
        //필요 최소 각도
        //float theta = Mathf.Asin(headRadius / radius) * 2;
        //turnInterval = theta / rotationSpeed;
        //시작 후 바로 회전할 수 있게 하기.
        //currentInterval = int.MaxValue;
        currentRotationPoint = startRotationPos;
        timeNRotPoint.Add(0f, startRotationPos);
        headTrans = head.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        flownTime += Time.deltaTime;
        Move();
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //새로운 회전 중심 계산
            Vector2 newPoint = currentRotationPoint + 2 * ((Vector2)headTrans.position - currentRotationPoint);
            Debug.Log(flownTime);
            Debug.Log(newPoint);
            //현재 회전 중심을 이전 회전 중심으로 저장
            timeNRotPoint.Add(flownTime, newPoint);
            //회전 중심 변경
            currentRotationPoint = newPoint;
            //방향 바꾸기
            isTurnClockwise = isTurnClockwise ? false : true;
        }
        
        float currentAngle = Vector2.Angle(Vector2.right, (Vector2)headTrans.position - currentRotationPoint);
        Vector3 rotAxis = isTurnClockwise ? Vector3.forward : -Vector3.forward;
        Vector2 newPos = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, rotAxis) * ((Vector2)headTrans.position - currentRotationPoint);
        //회전 중심을 기준으로 회전
        headTrans.position = currentRotationPoint + newPos;


    }

}
