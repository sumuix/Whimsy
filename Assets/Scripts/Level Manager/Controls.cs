using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] private GameObject Controls_Canvas;

    private void Start()
    {
        if (GameManager._instance && GameManager._instance.is_control)
        {
            Controls_Canvas.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>().enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Return))
        {
            Controls_Canvas.SetActive(false);
            GameManager._instance.is_control = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>().enabled = true;
        }
    }
}
