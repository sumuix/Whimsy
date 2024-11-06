using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Script : MonoBehaviour
{
    GameObject hook;
    // Start is called before the first frame update
    void Start()
    {
        hook = gameObject.transform.GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Fall()
    {
        hook.GetComponent<HingeJoint2D>().enabled = false;
    }
}
