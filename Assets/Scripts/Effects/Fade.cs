using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] public Image[] images;
    [SerializeField] public float image_delay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Show_Image());
    }

    IEnumerator Show_Image()
    {
        foreach (Image img in images)
        {
            StartCoroutine(Fade_In_Image(img));
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
        yield return new WaitForSeconds(image_delay);

        StartCoroutine(Fade_Out_Image(image));
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

}
