using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public Text text;
    public Text highestLabel;
    public Text highestScoreText;
    public Text flashingText;
    public Text exitText;

    private Coroutine flashingCoroutine;

    int score = 0;
    int highestScore = 0;

    bool isGameFinished = false;

    private void Start()
    {
        SetScoreText();
        EventManager.Instance.GameOver += SetGameOver;
    }

    private void OnDestroy()
    {
        EventManager.Instance.GameOver -= SetGameOver;

    }

    public void GetScore()
    {
        if (!isGameFinished)
        {
            score += 100;
            SetScoreText();
        }
    }

    public void SetScoreText()
    {
        text.text = score.ToString();
    }

    public void SetGameOver()
    {
        if (!isGameFinished)
        {
            isGameFinished = true;
            flashingCoroutine = StartCoroutine(BlinkText());
        }
        
        if (highestScore < score) { highestScore = score; }

        highestLabel.text = "highest score";
        highestScoreText.text = highestScore.ToString();
    }

    public void Reset()
    {
        flashingText.text = "";
        exitText.text = "";

        highestLabel.text = "";
        highestScoreText.text = "";
        
        score = 0;
        SetScoreText();

        StopCoroutine(flashingCoroutine);
        isGameFinished = false;
    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            flashingText.text = "Press Spacebar to restart";
            exitText.text = "Press ESC to exit";
            yield return new WaitForSeconds(.5f);
            flashingText.text = "";
            exitText.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }

    public void Update()
    {
        if (isGameFinished)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Reset();
                SceneManager.LoadScene("GameScene");

            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
