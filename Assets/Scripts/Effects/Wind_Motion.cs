using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_Motion : MonoBehaviour
{
    ParticleSystem partical;
    [SerializeField] float distance_Variation;

    // Start is called before the first frame update
    void Start()
    {
        partical = this.GetComponent<ParticleSystem>();
        StartCoroutine(Spawn_Wind());
    }

    IEnumerator Spawn_Wind()
    {
        float delay = partical.main.duration;
        yield return new WaitForSeconds(delay);
        partical.Stop();

        yield return new WaitForSeconds(delay + 5f);

        float rand = Random.Range(distance_Variation, (-1 * distance_Variation));
        Transform temp = transform;

        transform.position = new Vector3(temp.position.x, rand, temp.position.z);

        partical.Play();
        StartCoroutine(Spawn_Wind());
    }
}
