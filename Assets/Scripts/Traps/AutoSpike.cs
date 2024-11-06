using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpike : MonoBehaviour
{
    [SerializeField] float up_Speed;
    [SerializeField] float down_Speed;
    [SerializeField] float active_Y_Position, deactive_Y_Position;
    [SerializeField] float delay;

    bool activate;

    // Start is called before the first frame update
    void Start()
    {
        activate = false;

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CheckPos());
    }

    IEnumerator CheckPos()
    {
        if (transform.localPosition.y >= active_Y_Position)
        {

            activate = false;
        }

        if (transform.localPosition.y <= deactive_Y_Position)
            activate = true;

        if (activate)
        {
            yield return new WaitForSeconds(delay);
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(transform.localPosition.x, active_Y_Position), up_Speed * Time.deltaTime);

        }
        else
        {
            yield return new WaitForSeconds(delay);
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(transform.localPosition.x, deactive_Y_Position), down_Speed * Time.deltaTime);

        }

    }
}
