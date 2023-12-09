using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class TailMove : HeadMove
{
    public int pointIndex;

    private void OnEnable()
    {
        objTransform = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        flownTime += Time.deltaTime;
        Move();
        Mirror();
    }

    void Move()
    {
        //방향결정
        Vector3 rotAxis = isTurnClockwise ? Vector3.forward : -Vector3.forward;
        //회전 중심을 기준으로 회전
        Vector2 newPos = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, rotAxis) * ((Vector2)objTransform.position - currentRotationPoint);
        objTransform.position = currentRotationPoint + newPos;
        Debug.DrawLine(transform.position, currentRotationPoint,Color.red);
    }

    /// <summary>
    /// 꼬리 생성시 사용하는 함수.
    /// </summary>
    /// <param name="inputInfo">//생성될 위치의 회전중심 정보</param>
    /// <param name="index">//회전중심 저장한 배열의 inputInfo 순서</param>
    /// <param name="speed">속도 동일하게 입력</param>
    /// <param name="rotRadi">회전 거리 동일하게 입력</param>
    /// <param name="currentFlownTime">//언제 시점에 시작할지 결정</param>
    public void SetInfo(PointInfo inputInfo, int index, float speed, float rotRadi, float currentFlownTime)
    {
        pointIndex = index;
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
        //위치보정용 시간차이
        float timeDiff = currentFlownTime - inputInfo.startTime;
        flownTime = currentFlownTime;
        //회전 방향로 보정값 부호 결정
        timeDiff *= isTurnClockwise ? 1 : -1;
        //보정각도 계산(처음각도 + 시간차이로 보정한 각도)
        float deltaAngle = rotationSpeed * timeDiff + inputInfo.startAngle;
        //위치 계산
        Vector2 newPos = (Quaternion.AngleAxis(deltaAngle, Vector3.forward) * Vector2.right) * rotRadius;
        //위치 입력
        transform.position = currentRotationPoint + newPos;
    }

    public void SetInfo(PointInfo inputInfo)
    {
        //회전 중심 순서 1 증가
        pointIndex++;
        //회전 방향 재설정
        isTurnClockwise = inputInfo.isTurnClockwise;
        //회전 중심 재설정
        currentRotationPoint = inputInfo.rotationCenter;
        //위치보정용 시간차이 계산
        float timeDiff = flownTime - inputInfo.startTime;
        //회전 방향로 보정값 부호 결정
        timeDiff *= isTurnClockwise ? 1 : -1;
        //보정각도 계산(처음각도 + 시간차이로 보정한 각도)
        float deltaAngle = rotationSpeed * timeDiff + inputInfo.startAngle;
        //위치 계산
        Vector2 newPos = (Quaternion.AngleAxis(deltaAngle, Vector3.forward) * Vector2.right) * rotRadius;
        //위치 입력
        transform.position = currentRotationPoint + newPos;
    }
}
