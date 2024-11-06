using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mid_Dialogue : MonoBehaviour
{
    [SerializeField] Typewritter tw;
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tw.set_Dialogue(true);

            if (GameManager._instance != null)
            {

                if (GameManager._instance.currentGame_Level == GameManager.GameLevels.Level_Five)
                {
                    GameManager._instance.shouldend = true;
                }

            }
            Destroy(this.gameObject);
        }
    }
}
