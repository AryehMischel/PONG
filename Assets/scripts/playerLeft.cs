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




    void FixedUpdate()
    {
        if (Input.GetKey(upInputKey) && this.transform.position.y < upperLimit)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + speed, this.transform.position.z);
        }
        if (Input.GetKey(downInputKey) && this.transform.position.y > lowerLimit)
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
