using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    
    private int _score;
    private Text scoreText;
    private int currentLevelscoreLimit;
    private int levelmultiplier;
    private PlatformManager platformLimit;
    public bool levelUp;
    public int fireballLimit;
    public int bossLimit;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("Score Text").GetComponent<Text>();
        platformLimit = GameObject.FindGameObjectWithTag("Platform manager").GetComponent<PlatformManager>();
        levelmultiplier = 1;
        currentLevelscoreLimit = 100;
        fireballLimit = 20;
        bossLimit = 20;
    }

    // Update is called once per frame
    void Update()
    {   
        scoreText.text = "Score: " + _score;
        Debug.Log("Updating Score");

        if(levelUp == true)
        {
            levelUp = false;
            fireballLimit = fireballLimit + 100;
            bossLimit = bossLimit + 100;
        }

        if(_score == currentLevelscoreLimit)
        {
            currentLevelscoreLimit = currentLevelscoreLimit + 100;
        }

        if(_score == levelmultiplier * 100)
        {
            platformLimit.ScoreLimit1 = platformLimit.ScoreLimit1 + 300;
            platformLimit.ScoreLimit2 = platformLimit.ScoreLimit2 + 300;
            platformLimit.ScoreLimit3 = platformLimit.ScoreLimit3 + 300;
        }
    }

    public void AddScore()
    {
        _score += 1;
    }

    public void Reset()
    {
        _score = 0;
    }

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }
}
