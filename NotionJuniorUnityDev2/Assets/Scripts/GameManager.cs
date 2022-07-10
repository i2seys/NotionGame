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
        //В самом начале сцены ставим счёт равный нулю
        score = 0;
        UpdateScore(0);

        //Увеличиваем гравитацию в 12 раз
        Physics.gravity = new Vector3(0, -9.81f, 0) * 12;
    }

    //При нажатии на кнопку перезагрузки вызывается данный метод
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //Метод для обновления счёта при соприкосновении с сенсором
    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreText.text = score + "";
    }
}
