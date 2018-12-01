using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [Header("Characteristics")]
    public float speed = 1;

    private void Update()
    {
        transform.position += speed * Time.deltaTime;
    }
}
