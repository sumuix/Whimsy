using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager _instance = null;
    public enum GameLevels
    {
        Intro,
        MainMenu,
        LevelSelection,
        Level_One,
        Level_Two,
        Level_Three,
        Level_Four,
        Level_Five,
        End
    }

    public GameLevels currentGame_Level = GameLevels.MainMenu;

    [SerializeField] private GameObject loading_Canvas, Pause_Canvas;
    [SerializeField] private Image progress_Bar;
    private float progressbar_Target;
    private bool is_Pause;//, is_Transition;
    public bool is_LetterCanvas, is_control;
    public bool shouldend;

    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);

            is_Pause = false;
            //is_Transition = false;
            shouldend = false;
            Start_CurrentLevel();
        }
        else
        {
            Destroy(gameObject);
        }

        is_LetterCanvas = false;
        is_control = true;
    }
    public void Set_NextLevel(GameLevels next_Level)
    {
        End_CurrentLevel();
        currentGame_Level = next_Level;
        Start_CurrentLevel();
    }

    void Start_CurrentLevel()                                        //Open the desired Scene level
    {
        if (is_Pause)                                                 //Make sure game is unpaused
            Game_UnPause();

        switch (currentGame_Level)
        {

            case GameLevels.Intro:
                if (SceneManager.GetActiveScene().buildIndex != 0)
                    LoadScene(0);
                break;
            case GameLevels.MainMenu:
                //Check,Save or Load data then 
                LoadScene(1);
                break;
            case GameLevels.LevelSelection:
                LoadScene(2);
                break;
            case GameLevels.Level_One:
                LoadScene(3);
                break;
            case GameLevels.Level_Two:
                LoadScene(4);
                break;
            case GameLevels.Level_Three:
                LoadScene(5);
                break;
            case GameLevels.Level_Four:
                LoadScene(6);
                break;
            case GameLevels.Level_Five:
                LoadScene(7);
                break;
            case GameLevels.End:
                LoadScene(8);
                break;
        }

    }
    void End_CurrentLevel()                                         //Do neccessary stuff before colseing the Scene level
    {
        switch (currentGame_Level)
        {
            case GameLevels.MainMenu:
                break;
            case GameLevels.Level_One:
                break;
            case GameLevels.Level_Two:
                break;
            case GameLevels.Level_Three:
                break;
            case GameLevels.Level_Four:
                break;
            case GameLevels.Level_Five:
                break;
        }

    }
    public void Restart_Level()                          //Restart current level
    {
        Start_CurrentLevel();
    }
    public void Quit_Game()                              //Close the game
    {
        //UnityEditor.EditorApplication.isPlaying = false;   //Quit game during development in editor - Test
        Application.Quit();                                 //Quit game Application - Final Build
    }

    public void Start_Game()                                  //Need to start the game
    {
        Set_NextLevel(GameLevels.Level_One);
    }

    public void Game_MainMenu()                                 //Need to go Main Menu
    {
        Set_NextLevel(GameLevels.MainMenu);
    }
    public void Game_LevelSelection()                                 //Need to go Level Menu
    {
        Set_NextLevel(GameLevels.LevelSelection);
    }

    public void Game_Pause()                                      //Game Paused
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Time.timeScale = 0f;
        is_Pause = true;
        Pause_Canvas.SetActive(true);
    }

    void Game_UnPause()                                    //Game Un-Paused
    {
        is_Pause = false;
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Pause_Canvas.SetActive(false);
    }

    //private async 
    private void LoadScene(int scene_No)                   //Loading Scene between game Scene
    {

        SceneManager.LoadScene(scene_No);

        /*
                var Scene = SceneManager.LoadSceneAsync(scene_No);
                Scene.allowSceneActivation = false;

                progress_Bar.fillAmount = 0;
                progressbar_Target = 0;

                is_Transition = true;

                loading_Canvas.SetActive(true);

                do
                {
                    await Task.Delay(1000); //Doesn't work on WebGL as browser doesn't support multi thread

                    progressbar_Target = Scene.progress;        // Calculating fill amount for progress bar

                } while (Scene.progress < 0.9f);



                Scene.allowSceneActivation = true;

                await Task.Delay(1000);

                is_Transition = false;

                loading_Canvas.SetActive(false);*/
        shouldend = false;
    }

    private void Update()
    {
        // Animating Calculated fill amount in progress bar
        //progress_Bar.fillAmount = Mathf.MoveTowards(progress_Bar.fillAmount, progressbar_Target, 3 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape) && !is_LetterCanvas && !is_control)// && !is_Transition)
        {
            if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1 && SceneManager.GetActiveScene().buildIndex != 2)
            {
                if (!is_Pause)
                    Game_Pause();
                else
                    Game_UnPause();
            }
        }
    }
}
