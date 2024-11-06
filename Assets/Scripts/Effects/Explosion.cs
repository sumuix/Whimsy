using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject explosion_Effect;
    GameObject explode;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            explode = Instantiate(explosion_Effect, other.gameObject.transform.position, Quaternion.identity);

            //Play explosion sound
            FindObjectOfType<AudioManager>().Play_Sound("Explosion");

            //make player stop at the point of explosion
            other.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            //Restart after prefab is played completely and distroyed
            StartCoroutine(Restart(0.6f));
        }
    }

    IEnumerator Restart(float time)
    {


        yield return new WaitForSeconds(time);



        Destroy(explode);

        if (GameManager._instance != null)
        {
            GameManager._instance.Restart_Level();
        }
    }
}
