using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Vector2 acceleration;
    public Vector2 maxVelocity;
    public Vector2 brakeSpeed;
    public Rigidbody2D rb;
    public bool rotate;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(rb.velocity.sqrMagnitude < maxVelocity.sqrMagnitude)
        {
            rb.AddForce(acceleration, ForceMode2D.Force);
        }
	}


}
