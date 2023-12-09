using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class HeadMove : MonoBehaviour
{

    [SerializeField] private TailInstatntiater _tailInstantiater;
    protected bool isTurnClockwise = true;
    [SerializeField]
    protected float rotationSpeed = 90f;

    [SerializeField]
    protected float rotRadius = 0.77f;
    protected float radius;

    [SerializeField]  protected GameObject head;

    [SerializeField]
    protected float gapValue = 1.2f;

    [SerializeField, Tooltip("Tail objs")]
    public List<GameObject> tails = new List<GameObject>();

    protected List<PointInfo> rotPointInfo = new List<PointInfo>();

    public Transform objTransform;
    /// <summary>
    /// rotation point when game start
    /// </summary>
    private Vector2 startRotationPos = Vector2.zero;

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
        EventManager.Instance.AddTail += AddTail;
        EventManager.Instance.GameOver += GameOver;
        EventManager.Instance.GetHeadPos += GetHeadPos;

    }

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        EventManager.Instance.AddTail -= AddTail;

        EventManager.Instance.GameOver -= GameOver;
        EventManager.Instance.GetHeadPos -= GetHeadPos;
    }

    // Update is called once per frame
    private void Update()
    {
        flownTime += Time.deltaTime;
        Move();
        TailMove();
        
    }

    private void Move()
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
            //Vector2.right과 현재 중심에서 머리를 향하는 방향 사이의 각도(부호o)
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
        
        Debug.DrawLine(transform.position, currentRotationPoint, Color.blue);
    }
    private void TailMove()
    {
        for(int i = 0; i < tails.Count; i++)
        {
            TailMove tailMoveScript = tails[i].GetComponent<TailMove>();
            PointInfo inputInfo = new PointInfo();
            int j = 0;
            for (; j < rotPointInfo.Count; j++)
            {
                if (tailMoveScript.flownTime >= rotPointInfo[j].startTime)
                {
                    inputInfo = rotPointInfo[j];
                }
                else
                    break;
            }
            if (tailMoveScript.pointIndex >= j)
                continue;
            else
            {
                tailMoveScript.SetInfo(inputInfo);
            }
        }
    }

    public void AddTail()
    {
        
        GameObject go = EventManager.Instance.CallOnCreateTail();
        go.SetActive(true);
        CircleCollider2D newTail = go.GetComponent<CircleCollider2D>();
       


        /*
        꼬리 오브젝트 생성하면서 현재 꼬리 오브젝트 개수를 기반으로 계산한
        생성될 시간을 바탕으로 PointInfo 입력.
        */
        //필요 최소 각도(deg)
        float theta = Mathf.Asin(radius / rotRadius) * 2 * Mathf.Rad2Deg;

        //얼마나 전 위치에 넣어야 하는지 시간 계산
        float timeBefore = (tails.Count()+1) * (theta / rotationSpeed) * gapValue;
        //가능성 적긴 하지만 
        if(timeBefore >= flownTime)
        {
            timeBefore = flownTime;
        }
        //Debug.Log("최소 각도 = "+ theta +", " + timeBefore + "초 전에 넣음.");
        PointInfo returnInfo = new PointInfo();
        int i = 0;
        for (; i < rotPointInfo.Count;  i++)
        {
            //flownTime - timeBefore = 머리가 이 순간에 위치한 좌표가 꼬리가 위치할 좌표
            if (rotPointInfo[i].startTime <= flownTime - timeBefore)
            {
                returnInfo = rotPointInfo[i];
            }
            else
            {
                break;
            }
        }
        TailMove newTailScript = newTail.GetComponent<TailMove>();
        newTailScript.SetInfo(returnInfo, i, rotationSpeed, rotRadius, flownTime - timeBefore);
        tails.Add(newTail.gameObject);

    }

    public void AddTail(GameObject tail)
    {
        //꼬리 오브젝트 생성하면서 현재 꼬리 오브젝트 개수를 기반으로 계산한
        //생성될 시간을 바탕으로 PointInfo 입력.
        
        //필요 최소 각도(deg)
        float theta = Mathf.Asin(radius / rotRadius) * 2 * Mathf.Rad2Deg;

        //얼마나 전 위치에 넣어야 하는지 시간 계산
        float timeBefore = (tails.Count() + 1) * (theta / rotationSpeed) * gapValue;
        //가능성 적긴 하지만 
        if (timeBefore >= flownTime)
        {
            timeBefore = flownTime;
        }
        //Debug.Log("최소 각도 = "+ theta +", " + timeBefore + "초 전에 넣음.");
        PointInfo returnInfo = new PointInfo();
        int i = 0;
        for (; i < rotPointInfo.Count; i++)
        {
            //flownTime - timeBefore = 머리가 이 순간에 위치한 좌표가 꼬리가 위치할 좌표
            if (rotPointInfo[i].startTime <= flownTime - timeBefore)
            {
                returnInfo = rotPointInfo[i];
            }
            else
            {
                break;
            }
        }
        TailMove newTailScript = tail.GetComponent<TailMove>();
        newTailScript.SetInfo(returnInfo, i, rotationSpeed, rotRadius, flownTime - timeBefore);
        tails.Add(tail.gameObject);

    }

    private Transform GetHeadPos()
    {
        return objTransform;
    }

    private void GameOver()
    {
        int num = tails.Count;
        for(int i =0;i<num; ++i)
        {
            Destroy(tails[i]);
        }
        Destroy(gameObject);
    }
}
