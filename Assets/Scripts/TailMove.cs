using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TailMove : HeadMove
{
    public int pointIndex;
    public float endTime;

    private void OnEnable()
    {
        objTransform = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        flownTime += Time.deltaTime;
        Move();
    }

    void Move()
    {
        //방향결정
        Vector3 rotAxis = isTurnClockwise ? Vector3.forward : -Vector3.forward;
        //회전 중심을 기준으로 회전
        Vector2 newPos = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, rotAxis) * ((Vector2)objTransform.position - currentRotationPoint);
        objTransform.position = currentRotationPoint + newPos;
    }

    public void SetInfo(PointInfo inputInfo, float speed = int.MaxValue, float rotRadi = int.MaxValue, float currentFlownTime = -int.MaxValue, int index = -int.MaxValue)
    {
        //인덱스 입력 있으면(최초 생성시) 입력, 없으면 1 증가
        if (index != -int.MaxValue)
        {
            pointIndex = index;
        }
        else
        {
            pointIndex++;
        }
        //흐른 시간 입력 있으면(최초 생성시) 입력, 없으면 무시
        if (currentFlownTime != -int.MaxValue)
        {
            flownTime = currentFlownTime;
        }

        if (speed != int.MaxValue)
        {
            rotationSpeed = speed;
        }
        if(rotRadi != int.MaxValue)
        {
            rotRadius = rotRadi;
        }
        isTurnClockwise = inputInfo.isTurnClockwise;
        currentRotationPoint = inputInfo.rotationCenter;
        endTime = inputInfo.endTime;
        //위치보정용 시간차이
        float timeDiff = currentFlownTime - inputInfo.startTime;
        //방향으로 각도 부호 결정
        timeDiff *= isTurnClockwise ? -1 : 1;
        //보정각도 계산(처음각도 + 시간차이로 보정한 각도)
        float deltaAngle = rotationSpeed * timeDiff + inputInfo.startAngle;
        //회전 방향결정
        Vector3 rotAxis = isTurnClockwise ? Vector3.forward : -Vector3.forward;
        //위치 계산
        Vector2 newPos = (Quaternion.AngleAxis(deltaAngle, rotAxis) * Vector2.right) * rotRadius;
        //위치 입력
        transform.position = currentRotationPoint + newPos;
    }
}
