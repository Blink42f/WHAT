using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultingandClimbing : MonoBehaviour
{
    public GameObject vaultCheck;
    public GameObject ledgeCheck;
    public MovementScript movementScript;
    public Rigidbody rb;
    public Transform cameraPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vaultingCheck();
        ledgegrabCheck();
    }

    public void vaultingCheck()
    {
        RaycastHit result;
        if (Physics.Raycast(vaultCheck.transform.position, Vector3.down, out result, 1.2f) && !movementScript.OnSlope() && Input.GetKey(KeyCode.W))
        {
            Vector3 offset = new Vector3(0f, 1.6f, 0f) - (vaultCheck.transform.position - result.point);
            StartCoroutine(vault(offset));
        }
    }

    public IEnumerator vault(Vector3 offset)
    {
        float originalPosY = transform.position.y;
        Vector3 beforeVelocity = rb.velocity;
        rb.velocity = new Vector3(0f, 0f, 0f);
        bool finished = false;
        rb.useGravity = false;
        float count = 0;
        while (finished == false)
        {
            
            yield return new WaitForSeconds(0.01f);
            
            if (transform.position.y < originalPosY + offset.y)
            {
                count ++;
                cameraPos.RotateAroundLocal((Vector3.fwd * 2 + Vector3.right).normalized, 0.0005f);
                rb.MovePosition(transform.position + new Vector3(0f, 0.1f, 0f)+ (transform.forward * 0.1f));
            }
            else 
            {
                finished = true;
            }
            rb.velocity = beforeVelocity;
        }
        rb.useGravity = true;
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(0.01f);
            cameraPos.RotateAroundLocal((Vector3.fwd * 2 + Vector3.right).normalized, -0.0005f);
        }
    }

    public void ledgegrabCheck()
    {

    }
}