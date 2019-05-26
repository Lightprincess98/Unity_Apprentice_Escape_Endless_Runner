using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private CharacterController _controller;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpHeight = 10.0f;
    [SerializeField] private float _gravity = 1.0f;
    private float _yVelocity = 0.0f;
    [SerializeField] private float _xVelocity = 0.0f;
    private ScoreManager scoreManager;
    private PlatformManager platformManager;
    public bool canDash = false;
    public bool canshootFire = false;
    public bool canPhase = false;
    public bool canJump;
    public bool canMove = false;
    private bool center = true;
    private bool left = false;
    private bool right = false;
    private bool changingLane;
    public Object prefab;
    private GameObject[] obstacles;
    private GameObject[] enemies;
    private GameObject Endscreen;
    public Text dashText;
    public Text phaseText;
    public Text jumpText;
    public Text fireballText;

    // Start is called before the first frame update
    void Start()
    {
        //Get reference to the Character Controller Component
        _controller = GetComponent<CharacterController>();
        scoreManager = GameObject.FindGameObjectWithTag("Score Manager").GetComponent<ScoreManager>();
        platformManager = GameObject.FindGameObjectWithTag("Platform manager").GetComponent<PlatformManager>();
        Endscreen = GameObject.FindGameObjectWithTag("EndScreen");
        Endscreen.SetActive(false);
        canMove = true;
        jumpText.text = "Super Jump: Not Ready";
        phaseText.text = "Phase: Not Ready";
        dashText.text = "Dash: Not Ready";
        fireballText.text = "Fireball:Not Ready";
    }

    // Update is called once per frame
    void Update()
    {
        //Direction to Travel
        Vector3 direction = Vector3.forward;
        //Velocity = Direction * Speed
        Vector3 velocity = direction * _speed;

        //if can jump
        //velocity on Y = jumpHeight
        if (_controller.isGrounded == true)
        {
            //if space key
            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && changingLane == false)
            {
                _yVelocity = _jumpHeight;
            }

            if (Input.GetKeyDown(KeyCode.W) && canDash == true)
            {
                StartCoroutine(Dash());
                canDash = false;
            }

            if (Input.GetKeyDown(KeyCode.Q) && canshootFire == true)
            {
                Instantiate(prefab,
                    new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z + 5),
                    Quaternion.identity);
                fireballText.text = "Fireball:Not Ready";
                canshootFire = false;
            }

            if (Input.GetKeyDown(KeyCode.E) && canPhase == true)
            {
                StartCoroutine(Phase());
                canPhase = false;
            }

            if (Input.GetKeyDown(KeyCode.S) && canJump == true)
            {
                StartCoroutine(JumpHeight());
                canJump = false;
            }

            if (_xVelocity == 5 || _xVelocity == -5)
            {
                if (transform.position.x >= 0 && transform.position.x <= 0.49f && center == true || transform.position.x <= -4 && left == true ||
                    transform.position.x >= 4 && right == true)
                {
                    changingLane = false;
                    _xVelocity = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (center)
                {
                    center = false;
                    left = true;
                    _xVelocity = -5;
                    changingLane = true;
                }
                else if (right)
                {
                    right = false;
                    center = true;
                    _xVelocity = -5;
                    changingLane = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (center)
                {
                    center = false;
                    right = true;
                    _xVelocity = 5;
                    changingLane = true;
                }
                else if (left)
                {
                    left = false;
                    center = true;
                    _xVelocity = 5;
                    changingLane = true;
                }
            }
        }
        else
        {
            _yVelocity -= _gravity;

        }

        if (canMove == false)
        {
            Endscreen.SetActive(true);
        }

        if (canMove == true)
        {
            velocity.x = _xVelocity;
            //Take the velocity.y = cached y value
            velocity.y = _yVelocity;
            //Move (velocity * timeDelta)
            _controller.Move(velocity * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
            //StartCoroutine(Restart());
        }
    }

    public void setDash()
    {
        if (canDash == false)
        {
            canDash = true;
            dashText.text = "Dash: Ready";
        }
    }

    public void setFire()
    {
        if (canshootFire == false)
        {
            canshootFire = true;
            fireballText.text = "Fireball: Ready";
        }
    }

    public void setPhase()
    {
        if (canPhase == false)
        {
            canPhase = true;
            phaseText.text = "Phase: Ready";
        }
    }

    public void setJump()
    {
        if (canJump == false)
        {
            canJump = true;
            jumpText.text = "Super Jump: Ready";
        }
    }

    IEnumerator Dash()
    {
        _speed = 24;
        dashText.text = "Dash: Active";
        yield return new WaitForSecondsRealtime(2);
        dashText.text = "Dash: Not Ready";
        _speed = 14;
    }

    IEnumerator Phase()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var obstacle in obstacles)
        {
            BoxCollider[] Colliders = obstacle.GetComponents<BoxCollider>();
            foreach (var collider in Colliders)
            {
                collider.enabled = false;
            }
        }
        foreach (var enemy in enemies)
        {
            BoxCollider[] Colliders = enemy.GetComponents<BoxCollider>();
            foreach (var collider in Colliders)
            {
                collider.enabled = false;
            }
        }
        phaseText.text = "Phase: Active";
        yield return new WaitForSecondsRealtime(5);
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var obstacle in obstacles)
        {
            BoxCollider[] Colliders = obstacle.GetComponents<BoxCollider>();
            foreach (var collider in Colliders)
            {
                collider.enabled = true;
            }
        }
        foreach (var enemy in enemies)
        {
            BoxCollider[] Colliders = enemy.GetComponents<BoxCollider>();
            foreach (var collider in Colliders)
            {
                collider.enabled = true;
            }
        }
        phaseText.text = "Phase: Not Ready";
    }

    IEnumerator JumpHeight()
    {
        _jumpHeight = 25;
        jumpText.text = "Super Jump: Active";
        yield return new WaitForSecondsRealtime(3);
        jumpText.text = "Super Jump: Not Ready";
        _jumpHeight = 20;
    }
}

