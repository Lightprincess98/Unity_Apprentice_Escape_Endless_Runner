using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour
{
    private int _health = 100;
    public Text healthText;
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("Score Manager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreManager.Score >= scoreManager.bossLimit)
        {
            healthText.gameObject.SetActive(true);
            healthText.text = "Boss Health: " + _health;
        }
        else
        {
            healthText.gameObject.SetActive(false);
        }
        
        if(_health == 0)
        {
            healthText.gameObject.SetActive(false);
        }

    }

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }
}
