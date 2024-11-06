using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    //Refrences
    private Rigidbody2D rb;


    [Header("Movement")]                       //For Movement
    [SerializeField] float movement_Speed;
    [SerializeField] float acceleration;
    [SerializeField] float power;
    float x_Input;



    [Space(10)]

    [Header("Jump")]                        //For Jump
    [SerializeField] float jump_Force;
    [SerializeField] float gravityScale;
    [SerializeField] float fall_GravityScale;
    [SerializeField] float jump_Time;
    float jump_Time_Counter;
    [SerializeField] float coyoto_Time;
    private float coyoto_Time_Counter;
    private bool is_Jumping;
    public bool is_Grounded;
    [SerializeField] Transform ground_Check;
    [SerializeField] float check_Circle_Radius;
    [SerializeField] LayerMask ground_LayerMask;


    [Header("WallClimb")]
    [SerializeField] Transform wall_Check;
    [SerializeField] LayerMask Wall_LayerMask;
    public bool is_wall;
    bool wall_Jumping;





    [Space(10)]
    //For Animatioin
    private Animator animator;
    public bool right_Facing;

    //For Death To Fall
    private Vector3 respawn_Point, reset_camera_Pos;
    [SerializeField] GameObject fall_Detector;
    [SerializeField] GameObject reset_Camera;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        right_Facing = true;
        wall_Jumping = false;

        respawn_Point = transform.position;
        reset_camera_Pos = reset_Camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        x_Input = Input.GetAxisRaw("Horizontal");

        #region Animation

        if (x_Input != 0 && rb.velocity.y == 0)
            animator.SetBool("is_Running", true);
        else
            animator.SetBool("is_Running", false);

        if (rb.velocity.y == 0)
        {
            animator.SetBool("is_jumping", false);
            animator.SetBool("is_falling", false);
        }
        if (rb.velocity.y > 0)
        {
            animator.SetBool("is_jumping", true);
        }
        if (rb.velocity.y < 0)
        {
            animator.SetBool("is_jumping", false);
            animator.SetBool("is_falling", true);
        }

        #endregion

        is_Grounded = Physics2D.OverlapCircle(ground_Check.position, check_Circle_Radius, ground_LayerMask);

        is_wall = Physics2D.OverlapCircle(wall_Check.position, check_Circle_Radius, Wall_LayerMask);


        #region Player_Jump

        if (is_Grounded)
        {
            rb.gravityScale = gravityScale;
            coyoto_Time_Counter = coyoto_Time;
        }
        else
        {
            coyoto_Time_Counter -= Time.deltaTime;
        }

        if (coyoto_Time_Counter > 0f && Input.GetKeyDown(KeyCode.Space) && is_Grounded)
        {
            is_Jumping = true;
            rb.AddForce(Vector2.up * jump_Force, ForceMode2D.Impulse);
            jump_Time_Counter = jump_Time;
        }
        if (is_Jumping && Input.GetKey(KeyCode.Space) && is_Grounded)
        {

            if (jump_Time_Counter > 0)
            {
                rb.AddForce(Vector2.up * jump_Force, ForceMode2D.Impulse);
                jump_Time_Counter = -Time.deltaTime;
            }
            else
            {
                is_Jumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.gravityScale = fall_GravityScale;
            rb.AddForce(Vector2.down * jump_Force, ForceMode2D.Impulse);
            coyoto_Time_Counter = 0f;
            is_Jumping = false;
        }

        #endregion

        #region Move_Fall_Detector w.r.t Player

        fall_Detector.transform.position = new Vector2(transform.position.x, fall_Detector.transform.position.y);

        #endregion

        #region Player_Climb

        if (Input.GetKeyDown(KeyCode.Space) && is_wall && !is_Grounded)
        {
            wall_Jumping = true;
            Invoke(nameof(Set_Wall_Jumping_False), 0.5f);
        }

        if (wall_Jumping)
        {
            rb.velocity = new Vector2(acceleration, jump_Force);
        }

        #endregion
    }

    private void FixedUpdate()
    {

        #region Player_Movement

        float desired_Speed = x_Input * movement_Speed;

        float speed_Diff = desired_Speed - rb.velocity.x;

        //Apply force to change of speed and increase acceleratiion with higher speed thus Mathf.Pow
        float movement = Mathf.Pow(Mathf.Abs(speed_Diff) * acceleration, power);

        //To Apply the Drection
        movement *= Mathf.Sign(speed_Diff);

        //Apply force to Rigidbody for movement (x-axis only)
        rb.AddForce(movement * Vector2.right);

        #endregion

    }

    private void LateUpdate()
    {

        #region Character_Sprite_Flip

        Vector3 local_Scale = rb.transform.localScale;

        if (x_Input > 0)
            right_Facing = true;
        else if (x_Input < 0)
            right_Facing = false;

        if ((right_Facing && (local_Scale.x < 0)) || (!right_Facing && (local_Scale.x > 0)) && Time.timeScale > 0f)
            local_Scale.x *= -1;

        rb.transform.localScale = local_Scale;

        #endregion

    }


    #region Player_Fall_To_Death
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FallDetector")
        {
            Restart();
        }
    }

    void Restart()
    {
        if (GameManager._instance != null)
        {
            GameManager._instance.Restart_Level();
        }
    }

    #endregion

    void Set_Wall_Jumping_False()
    {
        wall_Jumping = false;
    }

    public void Player_Death()
    {
        Restart();
    }
}

