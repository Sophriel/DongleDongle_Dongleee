using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //  물리
    Rigidbody rb;
    public int speed;
    public int torque;

    public bool On_beat = false;
    int beat_loop = 0;
    int max_beat = 3;

    //  HP
    public int HP = 5;

    public ParticleSystem death;

    //  공격
    public GameObject MainCharacter;
    public float targeting_range = 20;
    public int atk = 2;
    public float push_force = 200;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        MainCharacter = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //  한번으로 모자라서 3번 주도록함
        if (On_beat && Vector3.Distance(transform.position, MainCharacter.transform.position) < targeting_range)
        {

            if (MainCharacter.transform.position.x > transform.position.x)
            {
                rb.AddForce(speed, 0, 0, ForceMode.Impulse);
                rb.AddTorque(0, 0, -torque, ForceMode.Acceleration);
            }

            else
            {
                rb.AddForce(-speed, 0, 0, ForceMode.Impulse);
                rb.AddTorque(0, 0, torque, ForceMode.Acceleration);
            }

            beat_loop++;
        }

        if (beat_loop > max_beat)
        {
            beat_loop = 0;
            On_beat = false;
        }
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (HP < 1)
        {
            Instantiate<ParticleSystem>(death, new Vector3(transform.position.x, transform.position.y), new Quaternion());  //  이펙트
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Bullet")
            HP -= obj.GetComponent<Bullet>().bullet_atk;
    }

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Player" && !obj.gameObject.GetComponent<MainCharacter>().Is_Onhit)
            obj.rigidbody.AddForceAtPosition((obj.transform.position - transform.position) * push_force, transform.position);
    }
}
