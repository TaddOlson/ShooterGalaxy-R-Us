using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    [SerializeField]
    private float _speed = 4f;
    public Enemy prefab;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 12, 0);
    }

   
    // Update is called once per frame
    void Update()
    {
        //move down at 4m/s
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if bottom of screen
        //respawn at top with a random x position
        if (transform.position.y < -6f)
        {
            Vector3 position = new Vector3(Random.Range(12.0f, 12.0f), 0, 0);
            Instantiate(prefab, position, Quaternion.identity);
        }
        
    }
}
