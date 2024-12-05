using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameObject PauseMenu;
    public GameObject scene;

    public float upperBoundary;
    public float lowerBoundary;
    public float leftBoundary;
    public float rightBoundary;

    public AudioManager audioManager;

    public GameObject WinScreen;
    [SerializeField]
    private TMP_Text winText;


    private playerLeft _playerLeft;
    [SerializeField]
    private Transform playerLeft;
    [SerializeField]
    private Transform playerRight;
    [SerializeField]
    private Transform bot;

    private int leftScore;
    private int rightScore;

    [SerializeField]
    private TMP_Text rightScoreText;
    [SerializeField]
    private TMP_Text leftScoreText;

    public bool inGame;
    public bool singlePlayer;


    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    void Start()
    {
        _playerLeft = playerLeft.GetComponent<playerLeft>();
    }

    void Update()
    {

        if (!inGame)return;
        
         
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();



        }

        if (Input.GetKeyDown("p"))
        {
            togglePause();



        }


    }

    //reset the game
    public void setGame(bool singlePlayer)
    {
        //reset Player's Position
        
        playerLeft.position = new Vector3(leftBoundary + 3f, 0, 0);

        if (singlePlayer)
        {
            _playerLeft.singlePlayerMode = true;
            bot.position = new Vector3(rightBoundary - 3f, 0, 0);
            bot.gameObject.SetActive(true);
            playerRight.gameObject.SetActive(false);

        }
        else
        {
            _playerLeft.singlePlayerMode = false;
            bot.gameObject.SetActive(false);
            playerRight.position = new Vector3(rightBoundary - 3f, 0, 0);
            playerRight.gameObject.SetActive(true);


        }

        //reset score

        leftScore = 0;
        rightScore = 0;

        leftScoreText.text = "0";
        rightScoreText.text = "0";


        // Activate Scene
        scene.SetActive(true);
        inGame = true;


        // reset ball
        Ball.Instance.transform.position = Vector3.zero;
        Ball.Instance.direction = new Vector3(1, 0.2f, 0);
        Ball.Instance.pauseball(0.5f);


     



    }


    
    public void StopGame()
    {
        inGame = false;
    }

    public void TogglePause()
    {
        togglePause();
    }

    //Pause Game
    public bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            PauseMenu.SetActive(false);
            scene.SetActive(true);
            Time.timeScale = 1f;
            Ball.Instance.pauseball(0.5f);
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            PauseMenu.SetActive(true);
            scene.SetActive(false);
            return (true);
        }
    }

    //Pause Ball
    public void pauseBall(float dur, Vector3 dir)
    {
        StartCoroutine(PauseBallForSeconds(dur, dir));
    }


    public IEnumerator PauseBallForSeconds(float dur, Vector3 dir)
    {
        Ball.Instance.direction = Vector3.zero;
        // Debug.Log("saved dir" + dir);
        yield return new WaitForSecondsRealtime(dur);
        //   Debug.Log("finished Co");
        Ball.Instance.direction = dir;
        Ball.Instance.paused = false;



    }

    // add points
    public void addPointToRightSide()
    {

        if (rightScore == 10)
        {
            scene.SetActive(false);
            winText.text = "RIGHT WINS!";
            WinScreen.SetActive(true);
            inGame = false;


        }
        else
        {

            AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.score);

            rightScore += 1;
            rightScoreText.text = rightScore.ToString();

            Ball.Instance.direction = new Vector3(-1, Random.Range(-10, 10) * (Ball.Instance.redirectIntensity * 0.08f), 0);

            Ball.Instance.transform.position = Vector3.zero;

            Ball.Instance.pauseball(0.5f);
        }

    }

    public void addPointToLeftSide()
    {
        if (leftScore == 10)
        {
            scene.SetActive(false);
            winText.text = "LEFT WINS!";
            WinScreen.SetActive(true);
            inGame = false;

        }
        else
        {
            AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.score);
            leftScore += 1;
            leftScoreText.text = leftScore.ToString();

            Ball.Instance.direction = new Vector3(1, Random.Range(-10, 10) * (Ball.Instance.redirectIntensity * 0.08f), 0);
            //Ball.Instance.direction = Vector3.zero;
            Ball.Instance.transform.position = Vector3.zero;
            Ball.Instance.pauseball(0.5f);

        }


    }


  



    public void closeApp()
    {
        Application.Quit();
    }


}
