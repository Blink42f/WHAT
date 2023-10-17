using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string scenename;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Touched");
            SceneManager.LoadScene(scenename);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    
}

