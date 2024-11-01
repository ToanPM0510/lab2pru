using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float minX;
    public float maxX;

    private bool isFacingRight = true;
    private float lastPositionX;
    private float direction;


    void Start()
    {
        lastPositionX = transform.position.x;
    }

    void Update()
    {
        float pingPong = Mathf.PingPong(Time.time * moveSpeed, maxX - minX) + minX;
        transform.position = new Vector3(pingPong, transform.position.y, transform.position.z);

        direction = transform.position.x - lastPositionX;
        lastPositionX = transform.position.x;

        Flip();
    }

    void Flip()
    {
        if (isFacingRight && direction < 0 || !isFacingRight && direction > 0)
        {
            isFacingRight = !isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
