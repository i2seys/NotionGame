using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsRotation : MonoBehaviour
{
    private Vector3 mousePosition;//������� ���� �� ������
    private Vector3 point1;//��������� � �������� ������� ����
    private Vector3 point2;
    public GameObject player;
    private Quaternion startRotation;
    private Vector3 startPosition;

    private bool swiping = false;//��������� ��������� ��� ���(������ �� ��� ��� ���?)
    private float forwardRotation;//�������� �������� �������� �� ����
    private float rightRotation;

    private float maxRotation = 0.25f;
    private float rotationSpeed = 600f;//�������� �������� ��������
    private float rotationAmplitude = 35f;//������������� ���������� �������� � ��������
    private float startRotationX;//��������� �������� ��������� ������������ ��� X

    private void Start()
    {
        //��������� �������� ��������� ������������ ��� X, �� ���� ����� ������� ���������
        startRotationX = transform.rotation.eulerAngles.x;
    }
    private void Update()
    {
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))//����� ������������ �������� �������� ����, �������� ������� ���������
        {
            swiping = true;
            point1 = mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))//����� ������������ ��������� ����, �������� ������� ���������
        {
            swiping = false;
        }
        //��������: ��������� ������� ���� (��������) ��� �������� �����, ������� � ��������� ������� (���������) ��� ���������� �����
        if (swiping == true)
        {
            point2 = mousePosition;
            RotatePlatforms(point1, point2);
            point1 = mousePosition;
        }
    }
    private void RotatePlatforms(Vector3 point1, Vector3 point2)
    {
        
        //���������, ������� �� ���� ������������ ����� �������/�����
        forwardRotation = -(point2.x - point1.x);
        rightRotation = point2.y - point1.y;
        CheckForMaxRotation(ref forwardRotation, ref rightRotation);


        //������� ��������� ������������ ������
        startRotation = transform.rotation;
        startPosition = transform.position;
        transform.RotateAround(player.transform.position, Vector3.forward, forwardRotation * rotationSpeed * Time.deltaTime);
        transform.RotateAround(player.transform.position, Vector3.right, rightRotation * rotationSpeed * Time.deltaTime);
        OverRotation();
    }
    //�������� �� ���������� ����������� �������� ���������, ���� ��������� - ���������� �������� �������� �� ����������� ����������
    private void OverRotation()
    {
        if (transform.rotation.eulerAngles.x > startRotationX + rotationAmplitude)
        {
            transform.rotation = startRotation;
            transform.position = startPosition;
            //transform.rotation = Quaternion.Euler(startRotationX + rotationAmplitude, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        if (transform.rotation.eulerAngles.x < startRotationX - rotationAmplitude)
        {
            transform.rotation = startRotation;
            transform.position = startPosition;
            //transform.rotation = Quaternion.Euler(startRotationX - rotationAmplitude, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        return;
    }
    private void CheckForMaxRotation(ref float forwardRotation, ref float rightRotation)
    {
        int positiveOrNegative;
        if (Mathf.Abs(forwardRotation) > maxRotation)
        {
            positiveOrNegative = forwardRotation < 0 ? -1 : 1;
            forwardRotation = maxRotation * positiveOrNegative;
        }
        if(rightRotation > maxRotation)
        {
            positiveOrNegative = rightRotation < 0 ? -1 : 1;
            rightRotation = maxRotation * positiveOrNegative;
        }
    }
}
