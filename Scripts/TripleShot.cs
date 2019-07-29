using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
    private float  _speed = 10f;
    void Start()
    {
        
    }

    void Update()
    {
        // move up at 10 speed
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y >= 6)
        {
            Destroy(this.gameObject);
        }
    }    
}
