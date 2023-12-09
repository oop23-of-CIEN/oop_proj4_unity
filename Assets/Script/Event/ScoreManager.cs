using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _bestScore;
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;
    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }

    private void Start()
    {
        EventManager.Instance.GetHighestScore += GetHighestScore;
        EventManager.Instance.UpdateScore += UpdateScore;
    }

    private void OnDisable()
    {
        EventManager.Instance.GetHighestScore -= GetHighestScore;
        EventManager.Instance.UpdateScore -= UpdateScore;
    }



    private int GetHighestScore()
    {
        return _bestScore;
    }

    private void UpdateScore(int score)
    {
        if (_bestScore < score)
            _bestScore = score;
    }


}
