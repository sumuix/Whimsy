using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] float force;

    private float bullet_Timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        bullet_Timer = 0;

        //Find direction where player is
        Vector3 direction = player.transform.position - transform.position;

        //Shoot bullet prefab towards player with some force
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        //for rotation of bullet prefab

        //find the necessary rotation towrads the direction and convert it into radian result to degree
        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

        //rotate bullet prefab in z-axis only
        transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
    }

    // Update is called once per frame
    void Update()
    {
        bullet_Timer += Time.deltaTime;

        if (bullet_Timer > 8)
        {
            Destroy(gameObject);
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
            rb.velocity = new Vector2(0, 0);
    }
}
