using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vile_Segments : MonoBehaviour
{
    public GameObject connected_Above, connected_Below, ristrict_Top;

    private void Start()
    {
        connected_Above = gameObject.GetComponent<HingeJoint2D>().connectedBody.gameObject;
    }
}
