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
        //�������
        Vector3 rotAxis = isTurnClockwise ? Vector3.forward : -Vector3.forward;
        //ȸ�� �߽��� �������� ȸ��
        Vector2 newPos = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, rotAxis) * ((Vector2)objTransform.position - currentRotationPoint);
        objTransform.position = currentRotationPoint + newPos;
        Debug.DrawLine(transform.position, currentRotationPoint,Color.red);
    }

    /// <summary>
    /// ���� ������ ����ϴ� �Լ�.
    /// </summary>
    /// <param name="inputInfo">//������ ��ġ�� ȸ���߽� ����</param>
    /// <param name="index">//ȸ���߽� ������ �迭�� inputInfo ����</param>
    /// <param name="speed">�ӵ� �����ϰ� �Է�</param>
    /// <param name="rotRadi">ȸ�� �Ÿ� �����ϰ� �Է�</param>
    /// <param name="currentFlownTime">//���� ������ �������� ����</param>
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
        //��ġ������ �ð�����
        float timeDiff = currentFlownTime - inputInfo.startTime;
        flownTime = currentFlownTime;
        //ȸ�� ����� ������ ��ȣ ����
        timeDiff *= isTurnClockwise ? 1 : -1;
        //�������� ���(ó������ + �ð����̷� ������ ����)
        float deltaAngle = rotationSpeed * timeDiff + inputInfo.startAngle;
        //��ġ ���
        Vector2 newPos = (Quaternion.AngleAxis(deltaAngle, Vector3.forward) * Vector2.right) * rotRadius;
        //��ġ �Է�
        transform.position = currentRotationPoint + newPos;
    }

    public void SetInfo(PointInfo inputInfo)
    {
        //ȸ�� �߽� ���� 1 ����
        pointIndex++;
        //ȸ�� ���� �缳��
        isTurnClockwise = inputInfo.isTurnClockwise;
        //ȸ�� �߽� �缳��
        currentRotationPoint = inputInfo.rotationCenter;
        //��ġ������ �ð����� ���
        float timeDiff = flownTime - inputInfo.startTime;
        //ȸ�� ����� ������ ��ȣ ����
        timeDiff *= isTurnClockwise ? 1 : -1;
        //�������� ���(ó������ + �ð����̷� ������ ����)
        float deltaAngle = rotationSpeed * timeDiff + inputInfo.startAngle;
        //��ġ ���
        Vector2 newPos = (Quaternion.AngleAxis(deltaAngle, Vector3.forward) * Vector2.right) * rotRadius;
        //��ġ �Է�
        transform.position = currentRotationPoint + newPos;
    }
}
