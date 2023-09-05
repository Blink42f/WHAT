using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private float speed = 0.2f;

    void LateUpdate()
    {
        transform.Translate(0, 1  * Time.deltaTime * speed, 0);
    }
}
