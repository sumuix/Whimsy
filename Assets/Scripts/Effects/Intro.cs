using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject Dialogue_Canvas;
    [SerializeField] private TextMeshProUGUI dialogue_context;
    [SerializeField] string[] context;
    private int index;
    private bool is_Dialogue;

    [SerializeField] float context_Speed;


    [Header("Fade_Image")]

    [SerializeField] public Image[] images;
    [SerializeField] public float image_delay;
    private Image curr_Img;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        is_Dialogue = true;
        StartCoroutine(Fade_In_Image(images[0]));
    }

    IEnumerator Show_Image()
    {
        foreach (Image img in images)
        {
            curr_Img = img;
            StartCoroutine(Fade_In_Image(curr_Img));
            yield return new WaitForSeconds(image_delay + 5f);
        }
    }
    IEnumerator Fade_In_Image(Image image)
    {// fade from transparent to opaque

        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            image.color = new Color(image.color.r, image.color.g, image.color.b, i);
            yield return null;
        }
    }

    IEnumerator Fade_Out_Image(Image image)
    {
        // fade from opaque to transparent

        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            image.color = new Color(image.color.r, image.color.g, image.color.b, i);
            yield return null;
        }
    }

    void Update()
    {
        if (is_Dialogue)
        {
            if (!Dialogue_Canvas.activeInHierarchy)  //If not active 
            {
                Dialogue_Canvas.SetActive(true);        //active it and start typing effect
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

        if (index == 2) { StartCoroutine(Fade_Out_Image(images[0])); StartCoroutine(Fade_In_Image(images[1])); }
        if (index == 4) { StartCoroutine(Fade_Out_Image(images[1])); StartCoroutine(Fade_In_Image(images[2])); }
        if (index == 8) { StartCoroutine(Fade_Out_Image(images[2])); StartCoroutine(Fade_In_Image(images[3])); }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager._instance != null)
                GameManager._instance.Game_MainMenu();
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
            if (GameManager._instance != null)
                GameManager._instance.Game_MainMenu();
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
