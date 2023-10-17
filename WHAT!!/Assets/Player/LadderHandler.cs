using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderHandler : MonoBehaviour
{
    public bool onLadder = false;
    public MovementScript movementScript;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movementScript.onLadder == false && onLadder == true)
        {
            onLadder = false;
        }
        if (onLadder)
        {

            rb.velocity = new Vector3(rb.velocity.x, Input.GetAxis("Vertical")*2, rb.velocity.z)+transform.right*Input.GetAxis("Horizontal")*0.1f;

            
        }
    }

    public void jumpOffLadder()
    {

        rb.velocity = new Vector3(0, 0, 0);

    }
}
