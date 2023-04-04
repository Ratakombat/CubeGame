using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Vector3 rotation;
    void Start()
    {
        rotation = new Vector3(0,0,90f);
    }

    
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }
}
