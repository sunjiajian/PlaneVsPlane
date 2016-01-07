using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public GameObject enemy0;
    public GameObject enemy1;
    public GameObject enemy2;

    public float enemy0Rate = 1f;
    public float enemy1Rate = 3f;
    public float enemy2Rate = 5f;

    public GameObject award0;
    public GameObject award1;

    public float award0Rate = 8f;
    public float award1Rate = 10f;


	// Use this for initialization
	void Start () {
        InvokeRepeating("createEnemy0", 1, enemy0Rate);
        InvokeRepeating("createEnemy1", 1, enemy1Rate);
        InvokeRepeating("createEnemy2", 1, enemy2Rate);

        InvokeRepeating("createAward0", 3, award0Rate);
        InvokeRepeating("createAward1", 3, award1Rate);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void createEnemy0() {
        float x = Random.Range(-2.66f, 2.66f);
        GameObject.Instantiate(enemy0, new Vector3(x, transform.position.y, 0), Quaternion.identity);
    }

    public void createEnemy1() {
        float x = Random.Range(-2.57f, 2.57f);
        GameObject.Instantiate(enemy1, new Vector3(x, transform.position.y, 0), Quaternion.identity);
    }

    public void createEnemy2() {
        float x = Random.Range(-2.15f, 2.15f);
        GameObject.Instantiate(enemy2, new Vector3(x, transform.position.y, 0), Quaternion.identity);
    }

    public void createAward0() {
        float x = Random.Range(-2.57f, 2.57f);
        GameObject.Instantiate(award0, new Vector3(x, transform.position.y, 0), Quaternion.identity);
    }

    public void createAward1() {
        float x = Random.Range(-2.57f, 2.57f);
        GameObject.Instantiate(award1, new Vector3(x, transform.position.y, 0), Quaternion.identity);
    }
}
