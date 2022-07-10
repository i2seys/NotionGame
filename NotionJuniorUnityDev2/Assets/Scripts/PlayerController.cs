using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Vector3 startPosition;//�������, � ������� ����� ��������������� ����� ����������� ������ ������
    public GameObject startPlatform;
    private GameManager gameManager;
    private float yOffset = 9f;
    
    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //��� ��������������� � �������� ���������� ������ � ��������� ������� � ��������� 1 � �����
        startPosition = new Vector3(startPlatform.transform.position.x, startPlatform.transform.position.y + yOffset, startPlatform.transform.position.z);
        transform.position = startPosition;
        gameManager.UpdateScore(1);
    }
   
}
