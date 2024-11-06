using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Message : MonoBehaviour
{
    [Header("Message")]
    [SerializeField] private GameObject Message_Canvas;
    private bool show;
    private bool inRange;

    [SerializeField] Typewritter tw;

    private void Start()
    {
        show = false;
        inRange = false;
    }

    private void Update()
    {
        if (inRange && !show && Input.GetKeyDown(KeyCode.F))
        {
            Show_Letter();
        }
        else
        if (show && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space)))
        {
            Close_Letter();
        }

    }




    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            inRange = true;
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            inRange = false;
    }

    void Show_Letter()
    {
        Message_Canvas.SetActive(true);
        show = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>().enabled = false;
        Time.timeScale = 0f;

        if (GameManager._instance != null)
        {
            GameManager._instance.is_LetterCanvas = true;
        }
    }

    void Close_Letter()
    {
        Message_Canvas.SetActive(false);
        show = false;
        Time.timeScale = 1f;
        if (GameManager._instance != null)
        {
            GameManager._instance.is_LetterCanvas = false;
        }
        tw.set_Dialogue(true);
        Destroy(this.transform.parent.gameObject);
    }


}
