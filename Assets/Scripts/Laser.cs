﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable of 8
    
    private float _speed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //translate laser up
        transform.Translate (Vector3.up * _speed * Time.deltaTime);

        //if position on y is > than 8
        //destroy the laser
        if (transform.position.y > 8)
        {
            Destroy(this.gameObject);
        }


    }
}
