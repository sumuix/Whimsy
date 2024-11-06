using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{

    [SerializeField] float up_Speed;
    [SerializeField] float down_Speed;
    [SerializeField] Transform upward;
    [SerializeField] Transform downward;
    bool crush;

    // Start is called before the first frame update
    void Start()
    {
        crush = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= upward.position.y)
            crush = true;

        if (transform.position.y <= downward.position.y)
            crush = false;

        if (crush)
            transform.position = Vector2.MoveTowards(transform.position, downward.position, down_Speed * Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, upward.position, up_Speed * Time.deltaTime);




    }
}
