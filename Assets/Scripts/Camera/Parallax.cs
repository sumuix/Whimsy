using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float sprite_StartingPos, sprite_Length;
    [SerializeField] public Camera scene_Camera;
    [SerializeField] public float parallax_Amount;



    private void Start()
    {
        sprite_StartingPos = transform.position.x;

        sprite_Length = GetComponent<SpriteRenderer>().bounds.size.x;
    }



    private void LateUpdate()
    {
        float cam_Position_X = scene_Camera.transform.position.x;
        float temp = cam_Position_X * (1 - parallax_Amount);
        float dist = cam_Position_X * parallax_Amount;

        Vector3 NewPosition = new Vector3(sprite_StartingPos + dist, transform.position.y, transform.position.z);

        transform.position = NewPosition;

        //Repeatinig Sprites 

        if (temp != 0f)         //If sprit and camera speed is not same i.e. parallex amount 1.
        {
            if (temp > sprite_StartingPos + (sprite_Length / 2))
            {
                sprite_StartingPos += sprite_Length;
            }
            else if (temp < sprite_StartingPos - (sprite_Length / 2))
            {
                sprite_StartingPos -= sprite_Length;
            }
        }
    }

}
