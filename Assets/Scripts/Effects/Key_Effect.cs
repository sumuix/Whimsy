using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Effect : MonoBehaviour
{

    Vector3 objectScale, newScale;            // To store the local scale of a game object
    [SerializeField] float transform_Speed;
    bool upscale;

    // Start is called before the first frame update
    void Start()
    {
        objectScale = transform.localScale;
        upscale = true;
        newScale = new Vector3(1.3f, 1.3f, 1.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (upscale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, transform_Speed * Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, objectScale, transform_Speed * Time.deltaTime);
        }
        if (transform.localScale == newScale)
        {
            upscale = !upscale;
        }
        else
        if (transform.localScale == objectScale)
        {
            upscale = !upscale;
        }

    }
}
