using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bullet_Pos;
    [SerializeField] float bullet_Interval;

    private float timer;
    private bool is_Shooting;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        is_Shooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_Shooting)
        {
            timer += Time.deltaTime;

            if (timer > bullet_Interval)
            {
                timer = 0;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, bullet_Pos.position, Quaternion.identity);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            is_Shooting = true;
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            is_Shooting = false;
        }
    }
}
