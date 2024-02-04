using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    private bool _instantDestroy = false;
    private GameObject _tripleShotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPower());
    }

    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //when we leave the screen, destroy this object
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
            //communicate with the player script
            Destroy(this.gameObject);
        }

    }
    //Only be collectible by the Player (HINT: Use Tags)
    //on collected, destroy

    IEnumerator SpawnPower()
    {
        while (_instantDestroy == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newPowerup = Instantiate(_tripleShotPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(15.0f);
        }
    }

    public void OnPlayerDeath()
    {
        _instantDestroy = true;
        Destroy(this.gameObject);
    }
}
