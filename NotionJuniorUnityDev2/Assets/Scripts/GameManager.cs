using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int score;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        //� ����� ������ ����� ������ ���� ������ ����
        score = 0;
        UpdateScore(0);

        //����������� ���������� � 12 ���
        Physics.gravity = new Vector3(0, -9.81f, 0) * 12;
    }

    //��� ������� �� ������ ������������ ���������� ������ �����
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //����� ��� ���������� ����� ��� ��������������� � ��������
    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreText.text = score + "";
    }
}
