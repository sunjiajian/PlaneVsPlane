using UnityEngine;
using System.Collections;

enum TrackRocketState {
    NOTARGET,
    HASTARGET,
}

//追踪导弹
//实现了初始速度，加速度，最小最大追踪距离，最大追踪角度
//     自行寻找目标，对目标转向
public class TrackRocket : MonoBehaviour
{
    public float edgeX;
    public float edgeY;

    private float speed = 2f;
    private float maxSpeed = 8f;
    private float acceleration = 2f;

    private Enemy target = null;
    private TrackRocketState trackState = TrackRocketState.NOTARGET;

    private Vector3 myDirection = Vector3.up;
    private float minTargetDis = 0.5f;
    private float maxTargetDis = 20f;
    private float maxTrackAngle = 30f;

    private float myAngle = 0f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        if (speed < maxSpeed)
        {
            speed += acceleration * dt;
            speed = speed > maxSpeed ? maxSpeed : speed;
        }

        if (hasTarget() || findTarget())
        {
            myDirection = target.transform.position - transform.position;
            myDirection.Normalize();

            float angle = Vector3.Angle(Vector3.up, myDirection) * fuhao(myDirection.x) * -1;
            transform.Rotate(Vector3.forward * (angle - myAngle));
            myAngle = angle;
        }
        transform.position += myDirection * speed * dt;

        if (transform.position.y > edgeY || transform.position.y < -edgeY || transform.position.x > edgeX || transform.position.x < -edgeX)
        {
            Destroy(gameObject);
        }
    }

    private float fuhao(float x)
    {
        return x < 0 ? -1 : 1;
    }

    private bool hasTarget()
    {
        if (trackState == TrackRocketState.HASTARGET && target != null && !target.isDead)
        {
            float ang = Vector3.Angle(myDirection, target.transform.position - transform.position);
            float dis = Vector3.Distance(transform.position, target.transform.position);
            if (ang <= maxTrackAngle && dis >= minTargetDis && dis <= maxTargetDis)
            {
                return true;
            }
        }
        return false;
    }

    private bool findTarget()
    {
        float minDis = maxTargetDis;
        GameObject minDisEnemy = null;
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemys)
        {
            if (enemy.GetComponent<Enemy>() != null && !enemy.GetComponent<Enemy>().isDead)
            {
                float ang = Vector3.Angle(myDirection, enemy.transform.position - transform.position);
                float dis = Vector3.Distance(transform.position, enemy.transform.position);
                if (ang <= maxTrackAngle && dis >= minTargetDis && dis < minDis)
                {
                    minDis = dis;
                    minDisEnemy = enemy;
                }
            }
        }

        if (minDisEnemy != null)
        {
            target = minDisEnemy.GetComponent<Enemy>();
            trackState = TrackRocketState.HASTARGET;
            return true;
        }

        target = null;
        trackState = TrackRocketState.NOTARGET;
        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (!other.GetComponent<Enemy>().isDead)
            {
                other.gameObject.SendMessage("OnHit");
                Destroy(gameObject);
            }
        }
    }

    //public void SetTarget(GameObject t)
    //{
    //    target = t.GetComponent<Enemy>();

    //    diretion = target.transform.position - transform.position;
    //    diretion.Normalize();
    //}
}
