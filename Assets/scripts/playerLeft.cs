using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLeft : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float upperLimit;

    [SerializeField]
    private float lowerLimit;

    
    public string upInputKey;
    public string downInputKey;
    public string altUpInputKey;
    public string altDownInputKey;
    
    private GameManager gameManager;
    public bool singlePlayerMode;
    
    private playerLeft _instance;
    public playerLeft Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(upInputKey) && this.transform.position.y < upperLimit || singlePlayerMode &&  Input.GetKey(altUpInputKey) && this.transform.position.y < upperLimit)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + speed, this.transform.position.z);
        }
        if (Input.GetKey(downInputKey) && this.transform.position.y > lowerLimit || singlePlayerMode && Input.GetKey(altDownInputKey) && this.transform.position.y > lowerLimit)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - speed, this.transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // col.gameObject.GetComponent<simpleBallScript>().hitWall();
        Ball.Instance.hitPlayer(this.transform.position.y);
    }
}
