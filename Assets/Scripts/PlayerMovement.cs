using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRB;
    // Use this for initialization
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        HandleMovement(horizontal);

    }
    void HandleMovement(float horizontal)
    {
        myRB.velocity = new Vector2(horizontal * speed, myRB.velocity.y);
    }
}
