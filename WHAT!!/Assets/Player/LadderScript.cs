using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LadderHandler player;
            other.TryGetComponent<LadderHandler>(out player);
            player.onLadder = true;
            MovementScript playerMovement;
            other.TryGetComponent<MovementScript>(out playerMovement);
            playerMovement.onLadder = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LadderHandler player;
            other.TryGetComponent<LadderHandler>(out player);
            player.onLadder = false;
            player.jumpOffLadder();
            MovementScript playerMovement;
            other.TryGetComponent<MovementScript>(out playerMovement);
            playerMovement.onLadder = false ;
        }
    }
}
