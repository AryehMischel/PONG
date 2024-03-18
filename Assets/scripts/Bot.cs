using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float upperLimit;

    [SerializeField]
    private float lowerLimit;




    void FixedUpdate()
    {


        if(Ball.Instance.direction.x == 1)
        {
            if (UnityEngine.Random.Range(0.1f, 1f) > 0.5f)
            {
                if (Ball.Instance.transform.position.y - this.transform.position.y > 0.5 && this.transform.position.y < upperLimit)
                {
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (speed * UnityEngine.Random.Range(0.3f, 1f)), this.transform.position.z);
                }

                if (Ball.Instance.transform.position.y - this.transform.position.y < -0.5 && this.transform.position.y > lowerLimit)
                {
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (speed * UnityEngine.Random.Range(0.3f, 1f)), this.transform.position.z);

                }
            }
          

        }

      


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Ball.Instance.hitPlayer(this.transform.position.y);
    }
}
