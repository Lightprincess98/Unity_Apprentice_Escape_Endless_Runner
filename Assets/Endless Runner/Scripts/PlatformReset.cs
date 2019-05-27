using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformReset : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        originalPosition = transform.localPosition;
    }

    private void OnBecameInvisible()
    {
        Reset();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            SetKinematicFalse();
        }
    }

    void SetKinematicFalse()
    {
        Debug.Log("Making Kinematic or Dynamic");
        rb.isKinematic = false;
        BossEnemy boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossEnemy>();
        boss.audio.clip = boss.boss1Clip;
        boss.audio.Play();

    }

    void SetKinematicTrue()
    {
        rb.isKinematic = true;
    }

    private void Reset()
    {
        Debug.Log("Resetting");
        SetKinematicTrue();
        transform.localPosition = originalPosition;
    }
}
