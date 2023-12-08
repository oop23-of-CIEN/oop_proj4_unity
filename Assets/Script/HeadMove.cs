using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

    [SerializeField, Range(0.5f, 1.5f), Tooltip("����ٴ� ���� ������ ����")]
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
            AddTail(go.GetComponent<CircleCollider2D>());
        }
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //���� ������ ȸ���� ������ �����ð� �Է�
            rotPointInfo[rotPointInfo.Count() - 1].endTime = flownTime;
            //���ο� ȸ�� �߽� ���
            Vector2 newPoint = currentRotationPoint + 2 * ((Vector2)objTransform.position - currentRotationPoint);
            //���� �ٲٱ�
            isTurnClockwise = isTurnClockwise ? false : true;
            //ȸ�� �߽� ����
            currentRotationPoint = newPoint;
            //Vector2.right�� ���� �߽ɿ��� �Ӹ��� ���ϴ� ���� ������ ����(��ȣo)
            float nextAngle = Vector2.SignedAngle(Vector2.right, (Vector2)objTransform.position - currentRotationPoint);
            //���ο� ȸ���� ���� �Է�
            PointInfo newPointInfo = new PointInfo(isTurnClockwise, nextAngle, newPoint, flownTime);
            rotPointInfo.Add(newPointInfo);
        }
        //ȸ�� �������
        Vector3 rotAxis = isTurnClockwise ? Vector3.forward : -Vector3.forward;
        //ȸ�� �߽��� �������� ȸ��
        Vector2 newPos = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, rotAxis) * ((Vector2)objTransform.position - currentRotationPoint);
        objTransform.position = currentRotationPoint + newPos;
        
        Debug.DrawLine(transform.position, currentRotationPoint, Color.blue);
    }
    void TailMove()
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

    public void AddTail(CircleCollider2D newTail)
    {
        /*���� ������Ʈ �����ϸ鼭 ���� ���� ������Ʈ ������ ������� �����
        ������ �ð��� �������� PointINfo �Է�.
        */
        //�ʿ� �ּ� ����(deg)
        float theta = Mathf.Asin(radius / rotRadius) * 2 * Mathf.Rad2Deg;

        //�󸶳� �� ��ġ�� �־�� �ϴ��� �ð� ���
        float timeBefore = (tails.Count()+1) * (theta / rotationSpeed) * gapValue;
        //���ɼ� ���� ������ 
        if(timeBefore >= flownTime)
        {
            timeBefore = flownTime;
        }
        //Debug.Log("�ּ� ���� = "+ theta +", " + timeBefore + "�� ���� ����.");
        PointInfo returnInfo = new PointInfo();
        int i = 0;
        for (; i < rotPointInfo.Count;  i++)
        {
            //flownTime - timeBefore = �Ӹ��� �� ������ ��ġ�� ��ǥ�� ������ ��ġ�� ��ǥ
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

    public void DeleteTail()
    {
        
    }

}
