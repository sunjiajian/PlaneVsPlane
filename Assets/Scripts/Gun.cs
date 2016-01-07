using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public GameObject bullet;
    public float rate = 0.3f;

    private bool isFiring = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void fire() {
        GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
    }

    public void SwitchFire(bool open) {
        if (open) {
            if (!isFiring) {
                isFiring = true;
                InvokeRepeating("fire", 0.5f, rate);
            }
        }
        else {
            if (isFiring) {
                isFiring = false;
                CancelInvoke("fire");
            }
        }
    }
}
