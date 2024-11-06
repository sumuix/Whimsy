using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass_Movement : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            animator.SetInteger("Shake", 1);
    }
    public void Stop_Shake()
    {
        animator.SetInteger("Shake", 0);
    }
}
