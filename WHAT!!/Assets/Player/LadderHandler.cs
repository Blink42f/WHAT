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
            rb.velocity = new Vector3(rb.velocity.x, Input.GetAxis("Vertical")*2f, rb.velocity.z)+transform.right*Input.GetAxis("Horizontal")*0.01f;
            RaycastHit hit;
            if (!Physics.SphereCast(transform.position, 0.2f, transform.forward,out hit, 1f) && Input.GetKey(KeyCode.W))
            {
                launchOffLadder();
                    movementScript.onLadder = false;
            }
        }
    }

    public void jumpOffLadder()
    {

        rb.velocity = new Vector3(0, 0, 0);

    }
    public void launchOffLadder()
    {

        rb.velocity = transform.forward*5;

    }
}
