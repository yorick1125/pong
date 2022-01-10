using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class Game : MonoBehaviour
{

    // DATA FIELDS
    private int player1Score;
    private int player2Score;
    private int servingPlayer;
    private int winningPlayer;
    private int victoryScore;

    // BALL
    public GameObject ball;

    // PLAYER 1
    public GameObject player1Paddle;
    public GameObject player1Goal;

    // PLAYER 2
    public GameObject player2Paddle;
    public GameObject player2Goal;

    // UI
    public GameObject title;
    public GameObject player1Text;
    public GameObject player2Text;

    // GAME STATE
    public enum State
    {
        Start,
        Serve,
        Play,
        Victory
    }
    private State currentState;

    // STRINGS
    private string startMessage;
    private string serveMessage;
    private string victoryMessage;


    // Start is called before the first frame update
    void Start()
    {
        // numbers
        player1Score = 0;
        player2Score = 0;
        servingPlayer = 1;
        winningPlayer = 1;
        victoryScore = 5;
        currentState = State.Start;
        // strings
        startMessage = "Welcome to Pong\nPress Enter to Begin ...";
        serveMessage = "Player " + servingPlayer + "'s serve...\nPress Enter to serve!";
        victoryMessage = "Player " + winningPlayer + " wins!\nPress Enter to restart!";
        ChangeTitle(startMessage);
    }

    // Update is called once per frame
    void Update()
    {
        print(currentState);
        if (Input.GetKeyDown("return"))
        {
            if (currentState == State.Start)
            {
                ChangeState(State.Serve);
                ChangeTitle(serveMessage);
            }
            else if (currentState == State.Serve)
            {
                ChangeState(State.Play);
                ChangeTitle(" ");
                ball.GetComponent<Ball>().Launch(servingPlayer);

            }
            else if (currentState == State.Victory)
            {
                ChangeTitle(serveMessage);
                ChangeState(State.Serve);
                ResetGame();
            }

        }




    }

    public void ChangeState(State newState)
    {
        currentState = newState;
    }

    public void ChangeTitle(string newTitle)
    {
        title.GetComponent<TextMeshProUGUI>().text = newTitle;
    }

    void ResetGame()
    {
        player1Score = 0;
        player2Score = 0;
        servingPlayer = winningPlayer;
        player1Text.GetComponent<TextMeshProUGUI>().text = "0";
        player2Text.GetComponent<TextMeshProUGUI>().text = "0";
        ResetPositions();
    }

    public int Player1Scored()
    {
        player1Score++;
        servingPlayer = 1;
        player1Text.GetComponent<TextMeshProUGUI>().text = player1Score.ToString();
        ResetBall();

        // check if player has won the final round
        if(player1Score >= victoryScore)
        {
            winningPlayer = 1;
            ChangeTitle(victoryMessage);
            ChangeState(State.Victory);
        }
        else
        {
            ChangeTitle(serveMessage);
            ChangeState(State.Serve);
        }
        return player1Score;
    }

    public int Player2Scored()
    {
        player2Score++;
        servingPlayer = 2;
        player2Text.GetComponent<TextMeshProUGUI>().text = player2Score.ToString();
        ResetBall();

        // check if player has won the final round
        if (player2Score >= victoryScore)
        {
            winningPlayer = 2;
            ChangeTitle(victoryMessage);
            ChangeState(State.Victory);
        }
        else
        {
            ChangeTitle(serveMessage);
            ChangeState(State.Serve);
        }
        return player2Score;
    }

    private void ResetPositions()
    {
        ball.GetComponent<Ball>().Reset();
        player1Paddle.GetComponent<Paddle>().Reset();
        player2Paddle.GetComponent<Paddle>().Reset();
    }

    private void ResetBall()
    {
        ball.GetComponent<Ball>().Reset();
    }





}
