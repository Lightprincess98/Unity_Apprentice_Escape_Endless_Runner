using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int speed;
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("Score Manager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
       
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Running");
        if(col.gameObject.tag == "Enemy" && scoreManager.Score >= scoreManager.fireballLimit)
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossEnemy>().Health -= 20;
            Debug.Log("Removed Health");
            col.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            col.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
