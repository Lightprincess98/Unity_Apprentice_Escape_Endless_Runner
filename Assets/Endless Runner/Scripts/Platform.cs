using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private PlatformManager _platformManager;
    public bool level1;
    public bool level2;
    public bool level3;

    private void OnEnable()
    {
        _platformManager = GameObject.FindObjectOfType<PlatformManager>();
        for (int a = 0; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(true);
        }
    }
    
    private void OnBecameInvisible()
    {
        //Recycle
        gameObject.SetActive(false);
        if (_platformManager.level1 == true && level1 == true)
        {
            Debug.Log("Running level 1");
            _platformManager.addInactivePlatform(this.gameObject);
        }
        else if (_platformManager.level2 == true && level2 == true)
        {
            Debug.Log("Running Level 2");
            _platformManager.addInactivePlatform(this.gameObject);
        }
        else if (_platformManager.level3 == true && level3 == true)
        {
            Debug.Log("Running level 3");
            _platformManager.addInactivePlatform(this.gameObject);
        }
        _platformManager.RecyclePlatform();
    }

    private void OnBecameVisible()
    {

    }
}
