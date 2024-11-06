using System.Collections;
using UnityEngine;
using TMPro;

public class Typewritter : MonoBehaviour
{
    [SerializeField] private GameObject Dialogue_Canvas, Player;
    [SerializeField] private TextMeshProUGUI dialogue_context;
    [SerializeField] string[] context;
    private int index;
    private bool is_Dialogue;

    [SerializeField] float context_Speed;


    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        is_Dialogue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_Dialogue)
        {
            Debug.Log("Making active");
            if (!Dialogue_Canvas.activeInHierarchy)  //If not active 
            {

                if (Player != null)
                {
                    Player.GetComponent<Player_Controller>().enabled = false;
                    Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                }

                Dialogue_Canvas.SetActive(true);        //active it and start typing effect
                Debug.Log("Activated");
                StartCoroutine(Writing_Effect());
            }
            else                                    //If active
            {
                if (dialogue_context.text == context[index])
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F))
                    {
                        Next_Dailouge();
                    }
            }

        }


    }

    IEnumerator Writing_Effect()
    {

        foreach (char letter in context[index].ToCharArray())
        {
            dialogue_context.text += letter;
            yield return new WaitForSeconds(context_Speed);
        }
    }

    void Next_Dailouge()
    {
        if (index < context.Length - 1)
        {
            index++;
            dialogue_context.text = "";
            StartCoroutine(Writing_Effect());
        }
        else
        {
            Close_Dialogue();

            if (Player != null)
            {
                Player.GetComponent<Player_Controller>().enabled = true;
                Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            if (GameManager._instance != null)
            {

                if (GameManager._instance.currentGame_Level == GameManager.GameLevels.Level_Five && GameManager._instance.shouldend)
                {
                    GameManager._instance.Set_NextLevel(GameManager.GameLevels.End);
                }
                else
                if (GameManager._instance.currentGame_Level == GameManager.GameLevels.End)
                {
                    GameManager._instance.Set_NextLevel(GameManager.GameLevels.MainMenu);
                }
            }

        }
    }

    void Close_Dialogue()
    {
        dialogue_context.text = "";
        index = 0;
        Dialogue_Canvas.SetActive(false);
        is_Dialogue = false;
    }

    public void set_Dialogue(bool setIt)
    {
        is_Dialogue = setIt;
    }
}
