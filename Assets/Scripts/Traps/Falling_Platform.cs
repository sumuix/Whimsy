using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Platform : MonoBehaviour
{
    [SerializeField] private float fall_Delay;
    [SerializeField] private float destroy_Object_Time;
    private Rigidbody2D rb;


    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fall_Delay);

        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(this.gameObject, destroy_Object_Time);
    }
}
