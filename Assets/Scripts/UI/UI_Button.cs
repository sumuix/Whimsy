using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image img;
    [SerializeField] private Sprite default_Sprite, pressed_Sprite;
    [SerializeField] AudioClip clicked_Audio, click_Release_Audio;
    //[SerializeField] AudioSource audio_Source;                       For Audio
    private GameManager.GameLevels nextLevel;
    public void OnPointerDown(PointerEventData eventData)        //When Mouse clicking on Button
    {
        img.sprite = pressed_Sprite;
        // audio_Source.PlayOneShot(clicked_Audio);                          Audio
    }

    public void OnPointerUp(PointerEventData eventData)         //When Mouse clicked on Button
    {
        img.sprite = default_Sprite;
        // audio_Source.PlayOneShot(click_Release_Audio);                    Audio
    }

    private void Disable_canvas()                                //Disable the canvas
    {
        gameObject.transform.root.gameObject.SetActive(false);
    }
    public void Start_Game()                                        //Play Button
    {
        if (GameManager._instance != null)
        {
            GameManager._instance.Start_Game();
            Disable_canvas();
        }
    }
    public void Game_MainMenu()                                     //Main Menu Button
    {

        if (GameManager._instance != null)
        {
            GameManager._instance.Game_MainMenu();
        }

    }

    public void Game_LevelSelection()                                     //Level Selection Page
    {

        if (GameManager._instance != null)
        {
            GameManager._instance.Game_LevelSelection();
        }

    }

    public void Level_Restart()                                     //Level Restart Button
    {

        if (GameManager._instance != null)
        {
            GameManager._instance.Restart_Level();
        }
    }


    public void Quit_Game()                                        //Exit Button
    {
        if (GameManager._instance != null)
        {
            GameManager._instance.Quit_Game();
        }
    }


    public void Manage_Level(int Level)                                        //Move Level
    {
        switch (Level)
        {
            case 1:
                nextLevel = GameManager.GameLevels.Level_One;
                break;
            case 2:
                nextLevel = GameManager.GameLevels.Level_Two;
                break;
            case 3:
                nextLevel = GameManager.GameLevels.Level_Three;
                break;
            case 4:
                nextLevel = GameManager.GameLevels.Level_Four;
                break;
            case 5:
                nextLevel = GameManager.GameLevels.Level_Five;
                break;
        }
        if (GameManager._instance != null)
        {
            GameManager._instance.Set_NextLevel(nextLevel);
            Disable_canvas();
        }

    }
}