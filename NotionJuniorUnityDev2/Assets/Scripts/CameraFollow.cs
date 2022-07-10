using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera mainCam;//������� ������
    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        //������ ���� ������ ������� �� �������
        mainCam.transform.position = new Vector3(transform.position.x, transform.position.y + 11, transform.position.z - 12);
    }
}
