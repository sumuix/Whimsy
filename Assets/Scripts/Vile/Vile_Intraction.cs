using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vile_Intraction : MonoBehaviour
{
    Rigidbody2D rb;
    HingeJoint2D hj;

    // Character state on Vile
    [SerializeField] float swing_Force;
    bool is_Attached = false;
    Transform attached_To;
    GameObject disregard;

    //Character Input
    float dir_x;
    float dir_y;
    private bool axis_InUse = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hj = GetComponent<HingeJoint2D>();
    }

    private void Update()
    {
        #region Vile_Input

        dir_x = Input.GetAxisRaw("Horizontal");
        dir_y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && is_Attached)
            Detach();

        #endregion

        //check if Player grounded 
        if (gameObject.GetComponent<Player_Controller>().is_Grounded)
        {
            if (is_Attached)
                Detach();
            attached_To = null;
        }


    }

    private void FixedUpdate()
    {
        CheckInputs();
    }

    void CheckInputs()      //Perform Physical Operation as per inputs
    {

        #region Character_Swing_on_vile

        //Swing Left or Right
        if (dir_x < 0)
            if (attached_To)
                rb.AddRelativeForce(new Vector3(-1, 0, 0) * swing_Force);

            else if (dir_x > 0)
                if (attached_To)
                    rb.AddRelativeForce(new Vector3(1, 0, 0) * swing_Force);

        #endregion

        #region Character_Slide_on_vile

        //Slide Up or Down One Setp At A Time

        if (dir_y > 0 && is_Attached && dir_x == 0)
        {
            StartCoroutine(CallMove(1));
        }
        if (dir_y < 0 && is_Attached && dir_x == 0)
        {
            StartCoroutine(CallMove(-1));
        }

        #endregion
    }

    IEnumerator CallMove(int dir)         // Perform Slide One Setp At A Time
    {
        if (axis_InUse)
        {
            axis_InUse = false;
            Slide(dir);
            yield return new WaitForSeconds(0.15f);
            axis_InUse = true;
        }
        else
        {
            yield return new WaitForSeconds(0f);
        }
    }

    public void Attach(Rigidbody2D attached_To_Bone)      // Enable Hingejoint2D in player and attach it to Vile_Bone
    {
        hj.connectedBody = attached_To_Bone;
        Debug.Log("connected to:" + attached_To_Bone);
        hj.enabled = true;
        is_Attached = true;
        attached_To = attached_To_Bone.gameObject.transform.parent;

        Fall_Script fs = attached_To.gameObject.GetComponent<Fall_Script>();
        if (fs != null)
        {
            fs.Fall();
        }
    }

    void Detach()                        // Disable Hingejoint2D in player and attach Detach from Vile_Bone
    {
        is_Attached = false;
        hj.enabled = false;
        hj.connectedBody = null;
        rb.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
    }

    void Slide(int direction)            // Attach player to upper or lower bone as per user Y-Input direction
    {
        Vile_Segments connected_Segment = hj.connectedBody.gameObject.GetComponent<Vile_Segments>();

        GameObject new_Segment = null;

        if (direction == 1)
        {
            if (hj.connectedBody.gameObject != connected_Segment.ristrict_Top)
            {
                if (connected_Segment.connected_Above != null)
                {
                    if (connected_Segment.connected_Above.gameObject.GetComponent<Vile_Segments>() != null)
                        new_Segment = connected_Segment.connected_Above;
                }
            }
        }

        if (direction == -1)
        {
            if (connected_Segment.connected_Below != null)
            {
                new_Segment = connected_Segment.connected_Below;
            }
            else Detach();
            Debug.Log("Down");
        }

        if (new_Segment != null)
        {
            transform.position = new_Segment.transform.position;
            hj.connectedBody = new_Segment.GetComponent<Rigidbody2D>();
        }


    }

    private void OnTriggerEnter2D(Collider2D other)  // Attach to Vile_Bone when Player touches Vile 
    {
        if (other.gameObject.tag == "Vile" && !is_Attached && attached_To != other.gameObject.transform.parent)
        {
            if (disregard == null || other.gameObject.transform.parent.gameObject != disregard)
            {
                Rigidbody2D attached_To_Bone = other.gameObject.GetComponent<Rigidbody2D>();
                Attach(attached_To_Bone);
            }
        }
    }
}
