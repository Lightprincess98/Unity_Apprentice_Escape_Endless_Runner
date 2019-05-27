using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRise : MonoBehaviour
{
    private bool rise = false;
    public int speed;
    public bool lvl2;
    public bool lvl3;
    private PlatformManager platformManager;

    // Start is called before the first frame update
    void Start()
    {
        platformManager = GameObject.FindGameObjectWithTag("Platform Manager").GetComponent<PlatformManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rise == true)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if(transform.position.y >= 0)
        {
            rise = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            rise = true;
            if(lvl2 == true && platformManager.level2 == true)
            {
                BossEnemy boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossEnemy>();
                boss.audio.clip = boss.boss2clip;
                boss.audio.Play();
            }
            else if(lvl3 == true && platformManager.level3 == true)
            {
                BossEnemy boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossEnemy>();
                boss.audio.clip = boss.boss3clip;
                boss.audio.Play();
            }

        }
    }
}
