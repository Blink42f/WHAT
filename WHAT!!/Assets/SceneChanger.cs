using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject sceneName;

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
