using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    [SerializeField]
    private float _speed = 1.0f;
    public Enemy prefab;
   
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 8, 0);
        
    }
       
   
    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.0f)
        {
            float randomX = Random.Range(-10f, 10f);
            transform.position = new Vector3(randomX, 8, 0);
        }
        

    }

    [SerializeField]
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.tag == "Player")
        {
           
            Player player = other.transform.GetComponent<Player>();
            
            if(player != null)
            {
                player.Damage();
            }


            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        
    }

  
}
