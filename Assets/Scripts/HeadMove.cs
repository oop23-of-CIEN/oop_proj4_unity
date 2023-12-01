using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class HeadMove : MonoBehaviour
{
    public GameObject tail;

    //truning clockwise?
    public bool isTurnClockwise = true;

    [Range(0f, 360f), Tooltip("degree/sec")]
    public float rotationSpeed;

    [Range(0f,2f), Tooltip("Rotation Radius")]
    public float rotRadius;
    float radius;

    [SerializeField, Range(0.5f, 1.5f), Tooltip("따라다닐 간격 조정용 변수")]
    float gapValue = 1f;
    [SerializeField, Tooltip("Head obj")]
    GameObject head;

    [SerializeField, Tooltip("Tail objs")]
    List<GameObject> tails = new List<GameObject>();

    List<PointInfo> rotPointInfo = new List<PointInfo>();

    public Transform objTransform;
    /// <summary>
    /// ratation point when game start
    /// </summary>
    Vector2 startRotationPos = Vector2.zero;

    public Vector2 currentRotationPoint;
    public float flownTime;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentRotationPoint = startRotationPos;
        objTransform.position = new Vector2(rotRadius, 0);
        PointInfo newinfo = new PointInfo();
        rotPointInfo.Add(newinfo);
        radius = head.GetComponent<CircleCollider2D>().radius;
        objTransform = head.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        flownTime += Time.deltaTime;
        Move();
        TailMove();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject go = Instantiate(tail);
            go.SetActive(true);
            AddTail(tail.GetComponent<CircleCollider2D>());
        }
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //기존 마지막 회전점 정보에 끝날시간 입력
            rotPointInfo[rotPointInfo.Count() - 1].endTime = flownTime;
            //새로운 회전 중심 계산
            Vector2 newPoint = currentRotationPoint + 2 * ((Vector2)objTransform.position - currentRotationPoint);
            //방향 바꾸기
            isTurnClockwise = isTurnClockwise ? false : true;
            //회전 중심 변경
            currentRotationPoint = newPoint;
            //(1,0)과 현재 중심에서 머리를 향하는 방향 사이의 각도(부호o)
            float nextAngle = Vector2.SignedAngle(Vector2.right, (Vector2)objTransform.position - currentRotationPoint);
            //새로운 회전점 정보 입력
            PointInfo newPointInfo = new PointInfo(isTurnClockwise, nextAngle, newPoint, flownTime);
            rotPointInfo.Add(newPointInfo);
        }
        //회전 방향결정
        Vector3 rotAxis = isTurnClockwise ? Vector3.forward : -Vector3.forward;
        //회전 중심을 기준으로 회전
        Vector2 newPos = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, rotAxis) * ((Vector2)objTransform.position - currentRotationPoint);
        objTransform.position = currentRotationPoint + newPos;
    }
    void TailMove()
    {
        for(int i = 0; i < tails.Count; i++)
        {
            TailMove tailMoveScript = tails[i].GetComponent<TailMove>();
            for(int j = 0; j < rotPointInfo.Count; j++)
            {
                if (tailMoveScript.flownTime >= rotPointInfo[j].endTime)
                {
                    tailMoveScript.SetInfo(rotPointInfo[j]);
                }
            }
        }
    }

    public void AddTail(CircleCollider2D newTail)
    {
        /*꼬리 오브젝트 생성하면서 현재 꼬리 오브젝트 개수를 기반으로 계산한
        생성될 시간을 바탕으로 PointINfo 입력.
        */
        //필요 최소 각도
        float theta = Mathf.Asin(radius / rotRadius) * 2 * Mathf.Rad2Deg;
        //얼마나 전 위치에 넣어야 하는지 시간 계산

        float timeBefore = (tails.Count()+1) * (theta / rotationSpeed) * gapValue;
        PointInfo returnInfo = new PointInfo();
        int i = 0;
        for (; i < rotPointInfo.Count;  i++)
        {
            if (rotPointInfo[i].startTime <= timeBefore)
            {
                returnInfo = rotPointInfo[i];
            }
            else
            {
                break;
            }
        }
        TailMove newTailScript = newTail.GetComponent<TailMove>();
        newTailScript.SetInfo(returnInfo, rotationSpeed, rotRadius, timeBefore, i);
        tails.Add(newTail.gameObject);

    }

    public void DeleteTail()
    {
        
    }

}
