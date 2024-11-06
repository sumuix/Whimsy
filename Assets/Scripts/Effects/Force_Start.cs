using System.Collections;
using UnityEngine;

public class Force_Start : MonoBehaviour
{
    [SerializeField] Typewritter tw;
    // Start is called before the first frame update
    private void Start()
    {
        Invoke("Call_Delay", 2f);
    }

    void Call_Delay()
    {
        tw.set_Dialogue(true);
        Debug.Log("Setting");
        //yield return new WaitForSeconds(2f);
    }
}
