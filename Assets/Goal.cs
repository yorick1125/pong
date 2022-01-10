using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayerGoal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            if (!isPlayerGoal)
            {
                Debug.Log("Player 1 Scored...");
                GameObject.Find("Game").GetComponent<Game>().Player1Scored();
            }
            else
            {
                Debug.Log("Player 2 Scored...");
                GameObject.Find("Game").GetComponent<Game>().Player2Scored();
            }
        }
    }
}
