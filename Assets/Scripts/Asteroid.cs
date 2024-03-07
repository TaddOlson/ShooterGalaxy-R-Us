using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 19.13f;
    [SerializeField]
    private GameObject _asteroid;
    [SerializeField]
    private GameObject _explosionPrefab;

    private Animator _anim;

    // Update is called once per frame
    void Update()
    {
        _asteroid.transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }


    // check for laser collision (trigger)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.5f);
            _anim.SetTrigger("OnAsteroidDestroyed");
        }

       
    }
    //instantiate explosion at the position of the asteroid (us)
    //destroy explosion after 3 seconds
}
