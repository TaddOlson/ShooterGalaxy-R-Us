using System.Collections;
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
    private int powerupID; //0 Triple Shot 1

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
                if (powerupID == 0)
                {
                    player.TripleShotActive();
                }
                else if (powerupID == 1)
                {
                    player.SpeedActive();
                }
                else if (powerupID == 2)
                {
                    player.ShieldsActive();
                }
            }
            
            Destroy(this.gameObject);
        }
    }
    

     
}
