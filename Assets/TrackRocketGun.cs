using UnityEngine;
using System.Collections;

public class TrackRocketGun : MonoBehaviour
{
    public GameObject bullet;
    public float rate = 1f;

    private bool isFiring = false;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("fire", 0.5f, rate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void fire()
    {
        GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
