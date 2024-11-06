using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter_Anim : MonoBehaviour
{
    private bool up;
    private Vector3 temp;
    private float rot_Speed, displacment = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        up = true;
        temp = new Vector3(transform.position.x, transform.position.y + displacment, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position == temp)
        {
            up = !up;

            if (up)
            {
                temp = new Vector3(transform.position.x, transform.position.y + displacment, transform.position.z);
                rot_Speed = 0.1f;
            }
            else
            {
                temp = new Vector3(transform.position.x, transform.position.y - displacment, transform.position.z);
                rot_Speed = -0.1f;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, temp, 0.25f * Time.deltaTime);
            transform.Rotate(Vector3.forward * rot_Speed);
        }
    }
}
