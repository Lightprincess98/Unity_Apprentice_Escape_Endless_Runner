using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRise : MonoBehaviour
{
    private bool rise = false;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }
}
