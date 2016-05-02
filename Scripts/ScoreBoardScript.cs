using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBoardScript : MonoBehaviour {

    public int score;

    Text scoreBoard;

    void Awake()
    {
        scoreBoard = GetComponent<Text>();
    }

    public void updateScoreBoard()
    {
        scoreBoard.text = score.ToString();
    }

    public void alterScore(int amount)
    {
        score += amount;
        if (score < 0)
        {
            score = 0;
        }
        updateScoreBoard();
    }
	
}
