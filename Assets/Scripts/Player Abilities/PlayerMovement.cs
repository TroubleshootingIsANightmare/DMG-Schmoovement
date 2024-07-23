using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    public Rigidbody rb;
    public Transform orientation;
    public float playerHeight = 1.4f;
    public Transform player;

    public static PlayerMovement instance;

    [Header("Keys")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode slideKey = KeyCode.LeftShift;

    [Header("Grounded")]
    public LayerMask ground;
    public float counterThreshold;
    public bool grounded;

    [Header("Movement")]
    public bool canSlide, canJump, sliding;
    public float jumpForce, slideForce;
    public float counterMovement, slideCounterMovement = 2f;
    public float maxSpeed = 30f, speed;
    public float multiplier = 1f, vMult = 1f;
    public float jumpCooldown;
    public float horizontalInput, verticalInput;

    Vector3 playerScale = new Vector3(1f, 1f, 1f);
    Vector3 slideScale = new Vector3(1f, 0.5f, 1f);

    Vector3 moveDirection;
    Vector3 inputDirection;




    [Header("Particles")]
    public ParticleSystem speedParticles;

    [Header("Spawn Boolean")]
    public bool spawned = false;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Grab the rigidbody from the player
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;

        //Check for spawn
        if (SceneManager.GetActiveScene().buildIndex != 0 && !spawned) SetPosition();
        if (SceneManager.GetActiveScene().buildIndex == 0) spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Check grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight*0.5f + 0.35f, ground);
        SpeedParticles();

        //Check for spawn
        if (SceneManager.GetActiveScene().buildIndex != 0 && !spawned) SetPosition();
        if (SceneManager.GetActiveScene().buildIndex == 0) spawned = false;

    }

    public void SetSpawned(bool spawn)
    {
        spawned = spawn;
    }

    private void FixedUpdate()
    {
        Move();
        MyInput();
        //Check for spawn
        if (SceneManager.GetActiveScene().buildIndex != 0 && !spawned) SetPosition();
        if (SceneManager.GetActiveScene().buildIndex == 0) spawned = false;
    }

    void Move()
    {
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;
        CounterMovement(horizontalInput, verticalInput, mag);
        moveDirection = vMult * orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (horizontalInput > 0 && xMag > maxSpeed) horizontalInput = 0;
        if (horizontalInput < 0 && xMag < -maxSpeed) horizontalInput = 0;
        if (verticalInput > 0 && yMag > maxSpeed) verticalInput = 0;
        if (verticalInput < 0 && yMag < -maxSpeed) verticalInput = 0;
        rb.AddForce(Vector3.down * Time.deltaTime * 10f);

        if (grounded)
        {

            if (!sliding)
            {
                multiplier = 1f;
                vMult = 1f;
            }
            if(sliding)
            {
                playerHeight = 0.7f;
                vMult = 0;
                multiplier = 0f;
                if(canJump)
                {
                    rb.AddForce(Vector3.down * Time.fixedDeltaTime * 3000f);
                }
            }
        }
        if (!grounded)
        {
            inputDirection = orientation.forward;
            multiplier = 0.5f;
            vMult = 0.5f;
        }

        rb.AddForce(orientation.right * horizontalInput * speed * multiplier * 0.5f * Time.deltaTime);
        rb.AddForce(vMult * orientation.forward * verticalInput * speed * 0.5f * Time.deltaTime);
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        JumpAndSlideLogic();
    }

    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce * 1.5f);
        rb.AddForce(moveDirection * jumpForce * 0.025f);
    }

    void ResetJump()
    {
        canJump = true;
    }

    void Slide()
    {
        if(grounded) rb.AddForce(inputDirection.normalized * slideForce);
    }

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!grounded) return;

        if (sliding)
        {

            rb.AddForce(speed * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);

            rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(0, 0, 0), 0.01f);

            return;
        }

        if (horizontalInput == 0 && verticalInput == 0) rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(0, 0, 0), 0.5f);
        //Counter movement
        if (Mathf.Abs(mag.x) > counterThreshold && Mathf.Abs(x) < 0.05f || (mag.x < -counterThreshold && x > 0) || (mag.x > counterThreshold && x < 0))
        {
            rb.AddForce(speed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Mathf.Abs(mag.y) > counterThreshold && Mathf.Abs(y) < 0.05f || (mag.y < -counterThreshold && y > 0) || (mag.y > counterThreshold && y < 0))
        {
            rb.AddForce(speed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        //Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > maxSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    void SpeedParticles()
    {
        //Particles rate of emission based on rb speed
        var emit = speedParticles.emission;
        Vector3 velocityForward = rb.velocity;
        velocityForward.y = 0f;
        velocityForward.Normalize();

        //Float to check if facing in the same direction as moving
        float angle = Vector3.Angle(orientation.forward, velocityForward);

        //Number of degrees which count as facing forward
        float threshold = 30f;

        //Detect if angle is less than threshold
        if (angle <= threshold && Input.GetAxisRaw("Vertical") > 0) emit.rateOverTime = rb.velocity.magnitude;
        else emit.rateOverTime = 0;
    }

    
    //Eldritch abomination code(DO NOT MESS WITH OR IT BREAKS)
    void JumpAndSlideLogic()
    {
        if (Input.GetKey(jumpKey) && canJump && grounded)
        {
            canJump = false;

            Jump();
            Invoke("ResetJump", jumpCooldown);
        }

        if (Input.GetKey(slideKey) && !sliding && canSlide && (verticalInput != 0 || horizontalInput != 0))
        {

            sliding = true;
            inputDirection = orientation.forward * verticalInput + horizontalInput * orientation.right;
        }

        if (!Input.GetKey(slideKey)) sliding = false;

        if (sliding) Slide(); player.localScale = slideScale; transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (!sliding) player.localScale = playerScale; playerHeight = 1.4f; transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    public void SetPosition()
    {
        spawned = true;
        Transform newPos = GameObject.Find("Spawn").GetComponent<Transform>();
        gameObject.transform.position = newPos.position;
        rb.velocity = Vector3.zero;
    }
}
