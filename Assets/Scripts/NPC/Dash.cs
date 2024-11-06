using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] float dash_Force;
    [SerializeField] float correction_Angle;

    private bool should_Destroy;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        should_Destroy = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (should_Destroy)
            timer += Time.deltaTime;

        if (timer > 5)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
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
            Dash_Towards_Player();
            should_Destroy = true;
        }
    }

    void Dash_Towards_Player()
    {
        //Find direction where player is
        Vector3 direction = player.transform.position - transform.position;

        //Shoot bullet prefab towards player with some force
        rb.velocity = new Vector2(direction.x, direction.y).normalized * dash_Force;

        //for rotation of bullet prefab

        //find the necessary rotation towrads the direction and convert it into radian result to degree
        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

        //rotate bullet prefab in z-axis only
        rb.transform.rotation = Quaternion.Euler(0, 0, rotation - correction_Angle);
    }
}
