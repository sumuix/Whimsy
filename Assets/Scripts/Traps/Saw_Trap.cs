using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw_Trap : MonoBehaviour
{
    [SerializeField] private float rotating_speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0, 0, 360 * rotating_speed * Time.deltaTime);

    }
}
