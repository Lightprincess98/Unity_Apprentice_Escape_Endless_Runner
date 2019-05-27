using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool Phase;

    public bool Jump;

    public bool Fireball;

    public bool Dash;

    public AudioSource audio;

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Phase)
            {
                playerController.setPhase();
                audio.Play();
            }

            if (Jump)
            {
                playerController.setJump();
                audio.Play();
            }

            if (Fireball)
            {
                playerController.setFire();
                audio.Play();
            }

            if (Dash)
            {
                playerController.setDash();
                audio.Play();
            }

            this.gameObject.SetActive(false);
        }
    }
}
