using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private GameManager.GameLevels nextLevel;
    private void OnTriggerEnter2D(Collider2D other)                                     //Level Complete
    {

        if (other.gameObject.tag == "Player" && GameManager._instance != null)
        {
            GameManager._instance.Set_NextLevel(nextLevel);
        }
    }
}
