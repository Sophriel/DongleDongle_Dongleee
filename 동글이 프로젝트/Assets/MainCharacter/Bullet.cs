using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 direction;
    Vector3 start_pos;

    public int bullet_atk = 0;

    public int speed = 1000;
    public float range = 8.0f;

    public void Init(Direction dir, Direction sub_dir, int atk, float size)
    {
        switch(dir)
        {
            case Direction.Up:
                direction.y = speed;
                break;

            case Direction.Down:
                direction.y = -speed;
                break;

            case Direction.Left:
                direction.x = -speed;
                break;

            case Direction.Right:
                direction.x = speed;
                break;
        }

        switch (sub_dir)
        {
            case Direction.Up:
                direction.y = speed;
                break;

            case Direction.Down:
                direction.y = -speed;
                break;

            case Direction.Left:
                direction.x = -speed;
                break;

            case Direction.Right:
                direction.x = speed;
                break;
        }

        gameObject.GetComponent<Rigidbody>().AddForce(direction);

        start_pos = transform.position;

        bullet_atk = atk;
        transform.localScale = new Vector3(size, size, size);
    }

    void Update()
    {
        float len = Vector3.Distance(start_pos, transform.position);

        if (len > range)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag != "Player" && obj.gameObject.tag != "Bullet")
            Destroy(gameObject);
    }
}
