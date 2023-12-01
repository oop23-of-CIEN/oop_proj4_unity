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
        //�������
        Vector3 rotAxis = isTurnClockwise ? Vector3.forward : -Vector3.forward;
        //ȸ�� �߽��� �������� ȸ��
        Vector2 newPos = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, rotAxis) * ((Vector2)objTransform.position - currentRotationPoint);
        objTransform.position = currentRotationPoint + newPos;
    }

    public void SetInfo(PointInfo inputInfo, float speed = int.MaxValue, float rotRadi = int.MaxValue, float currentFlownTime = -int.MaxValue, int index = -int.MaxValue)
    {
        //�ε��� �Է� ������(���� ������) �Է�, ������ 1 ����
        if (index != -int.MaxValue)
        {
            pointIndex = index;
        }
        else
        {
            pointIndex++;
        }
        //�帥 �ð� �Է� ������(���� ������) �Է�, ������ ����
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
        //��ġ������ �ð�����
        float timeDiff = currentFlownTime - inputInfo.startTime;
        //�������� ���� ��ȣ ����
        timeDiff *= isTurnClockwise ? -1 : 1;
        //�������� ���(ó������ + �ð����̷� ������ ����)
        float deltaAngle = rotationSpeed * timeDiff + inputInfo.startAngle;
        //ȸ�� �������
        Vector3 rotAxis = isTurnClockwise ? Vector3.forward : -Vector3.forward;
        //��ġ ���
        Vector2 newPos = (Quaternion.AngleAxis(deltaAngle, rotAxis) * Vector2.right) * rotRadius;
        //��ġ �Է�
        transform.position = currentRotationPoint + newPos;
    }
}
