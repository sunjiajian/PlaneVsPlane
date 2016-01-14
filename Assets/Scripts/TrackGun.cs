using UnityEngine;
using System.Collections;

public class TrackGun : MonoBehaviour {

    public float rate = 2f;
    private float tick = 0;

    public float maxDistance = 1f;

    public TrackBullet bullet;

    GameObject minDisEnemy;

	// Use this for initialization
	void Start () {
        rate = 2f;
        tick = rate;
	}
	
	// Update is called once per frame
	void Update () {
        tick -= Time.deltaTime;
        if (tick <= 0) { 
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemys.Length > 0) {
                float minDis = 1000000f;

                foreach (var enemy in enemys){
                    float dis = Vector3.Distance(transform.position, enemy.transform.position);
                    minDis = minDis < dis ? dis : minDis;
                    minDisEnemy = enemy;
                }

                if (minDis < maxDistance) {
                    /*TrackBullet gobj = (TrackBullet)*/GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
                    //gobj.printss();
                    print("finish born");
                    //gobj.SetTarget(minDisEnemy);

                    tick = rate;
                }
            }
        }
	}
}
