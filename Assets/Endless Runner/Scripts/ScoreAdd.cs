using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAdd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            ScoreManager addSCore = GameObject.FindGameObjectWithTag("Score Manager").GetComponent<ScoreManager>();
            addSCore.AddScore();
        }
    }
}
