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
    private float hangCooldown = 0.1f;
    private float timeSinceHang = 1f;

    private float vaultCoolTime = 1f;
    private float timeSinceVault = 0f;

    void Update()
    {
        vaultingCheck();
        ledgegrabCheck();
        timeSinceVault += Time.deltaTime;
    }

    public void vaultingCheck()
    {
        RaycastHit result;
        if (!movementScript.onLadder&& timeSinceVault>vaultCoolTime && Physics.Raycast(vaultCheck.transform.position, Vector3.down, out result, 1.1f) && !movementScript.OnSlope() && Input.GetKey(KeyCode.W) && movementScript.state != MovementScript.movementState.crouching)
        {  
            if (!result.collider.gameObject.CompareTag("Ladder"))
            {
                timeSinceVault = 0f;
                Vector3 offset = new Vector3(0f, 1.6f, 0f) - (vaultCheck.transform.position - result.point);
                StartCoroutine(vault(offset));
            }
        }
    }

    public IEnumerator vault(Vector3 offset)
    {
        movementScript.vaulting = true;
        movementScript.hanging = false;
        float originalPosY = transform.position.y;
        Vector3 beforeVelocity = rb.velocity;
        rb.velocity = new Vector3(0f, 0f, 0f);
        bool finished = false;
        rb.useGravity = false;
        float count = 0;
        while (finished == false)
        {
            yield return new WaitForSeconds(0.01f);
            if (transform.position.y < originalPosY + offset.y )
            {
                if (Mathf.Abs(cameraPos.localRotation.z) < 0.1f)
                {
                    count++;
                    cameraPos.RotateAroundLocal((Vector3.fwd * 2 + Vector3.right).normalized, 0.005f);
                }
                rb.MovePosition(transform.position + new Vector3(0f, 0.1f, 0f)+ (transform.forward*0.1f));
            }
            else 
            {
                finished = true;
            }
            
        }
        rb.useGravity = true;
        rb.velocity = beforeVelocity;
        movementScript.vaulting = false;
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(0.01f);
            cameraPos.RotateAroundLocal((Vector3.fwd * 2 + Vector3.right).normalized, -0.005f);
        }

    }
public void ledgegrabCheck()

    {
        RaycastHit result;
        if (!movementScript.hanging&&Physics.Raycast(ledgeCheck.transform.position, Vector3.down, out result, 1f) && !movementScript.grounded && Input.GetKey(KeyCode.Space) && timeSinceHang>hangCooldown)
        {
            timeSinceHang = 0f;
            grabLedge();
        }
        if (Physics.Raycast(ledgeCheck.transform.position, Vector3.down, out result, 1f) && Input.GetKey(KeyCode.Space) && timeSinceHang>hangCooldown&&movementScript.hanging)
        {
            timeSinceHang = 0f;
            Vector3 offset = new Vector3(0f, 1.6f, 0f) - (ledgeCheck.transform.position - result.point);
            jumpOnLedge(offset);
        }
        else if (!Physics.Raycast(ledgeCheck.transform.position, Vector3.down, out result, 1f) && Input.GetKey(KeyCode.Space) && timeSinceHang > hangCooldown && movementScript.hanging)
        {
            jumpOffLedge();
        }
        timeSinceHang += Time.deltaTime;
    }

    public void grabLedge()
    { 
        movementScript.hanging = true;                            
        rb.velocity = Vector3.zero;
    }

    public void jumpOnLedge(Vector3 offset)
    {
        StartCoroutine(vault(offset));
    }

    public void jumpOffLedge()
    {
        movementScript.Jump();
        rb.AddForce(transform.forward*3,ForceMode.Impulse);
        movementScript.hanging = false;
    }
}
