﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    // ID for Powerups
    //0 = Triple Shot
    //1 = Speed
    //2 = Shield
    [SerializeField]
    private int powerupID; //0 Triple Shot 1 Speed 2 Shields

    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    
        if (transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    //OnTriggerCollision
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {   
                switch(powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            
            Destroy(this.gameObject);
        }
    }
    

     
}
