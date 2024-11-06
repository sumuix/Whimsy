using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nav_Points : MonoBehaviour
{
    [SerializeField] Transform left_Nav, right_Nav;
    [SerializeField] float speed;
    bool next_nav;

    [SerializeField] bool flip_Character = false;
    private Rigidbody2D rb;
    private Vector3 local_Scale;

    // Start is called before the first frame update
    void Start()
    {
        next_nav = false;
        if (flip_Character)
        {
            rb = GetComponent<Rigidbody2D>();
            local_Scale = rb.transform.localScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x >= right_Nav.position.x)
            next_nav = true;

        if (transform.localPosition.x <= left_Nav.position.x)
            next_nav = false;

        if (next_nav)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, left_Nav.position, speed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, right_Nav.position, speed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        if (transform.localPosition.x == left_Nav.position.x)
            Flip();
        if (transform.localPosition.x == right_Nav.position.x)
            Flip();
    }

    private void Flip()
    {
        if (flip_Character)
        {
            local_Scale.x *= -1;

            rb.transform.localScale = local_Scale;
        }
    }
}
