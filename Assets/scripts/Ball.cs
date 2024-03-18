using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public static Ball Instance;
    
    
    public Vector3 direction;
    public float redirectIntensity;
    
    [SerializeField]
    private float speed;


    public bool paused;


    private float upperBoundary;
    private float lowerBoundary;
    private float leftBoundary;
    private float rightBoundary;


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
        upperBoundary = GameManager.Instance.upperBoundary;
        lowerBoundary = GameManager.Instance.lowerBoundary;
        leftBoundary = GameManager.Instance.leftBoundary;
        rightBoundary = GameManager.Instance.rightBoundary; 

    }


    private void FixedUpdate()
    {


        if (this.transform.position.x <= leftBoundary)
        {
            GameManager.Instance.addPointToRightSide();
            return;
        }

        if(this.transform .position.x >= rightBoundary)
        {
             GameManager.Instance.addPointToLeftSide();

        }

        if (this.transform.position.y <= lowerBoundary)
        {
            hitWall();

        }

        if (this.transform.position.y >= upperBoundary)
        {
            hitWall();

        }

        this.transform.position = this.transform.position + direction * (speed * 0.1f);

    }

    public void hitWall() {
        direction = new Vector3(direction.x, -direction.y, 0);
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.wall);
    }


    public void hitPlayer(float playerPositionY)
    {
        AudioManager.Instance.AudioSource.PlayOneShot(AudioManager.Instance.paddle);
        float newang = this.transform.position.y - playerPositionY;
        Debug.Log(newang);
        direction = new Vector3(-direction.x, (newang * redirectIntensity), 0);
    }

    public void pauseball(float dur)
    {
        if(!paused)
        {
            GameManager.Instance.pauseBall(dur, direction);
            paused = true;
        }
    }





}
