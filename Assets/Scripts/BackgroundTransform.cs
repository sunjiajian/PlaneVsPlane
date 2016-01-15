using UnityEngine;
using System.Collections;

public class BackgroundTransform : MonoBehaviour {

    private float speed = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -10.65f) {
            transform.position = new Vector3(transform.position.x, transform.position.y + 10.65f * 2, transform.position.z);
        }
	}
}
