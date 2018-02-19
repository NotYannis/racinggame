using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript;

public class PlayerController : MonoBehaviour {
    public float acceleration;
    public float maxVelocity;
    public float brakeSpeed;
    private Rigidbody2D rb;
    public DebugMenu debug;

    float xScreenCenter;
    GameObject steeringWheel;
    float steeringWheelBaseRotation;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        xScreenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.0f, 0.0f)).x;
        steeringWheel = GameObject.Find("SteeringWheel");
        steeringWheelBaseRotation = steeringWheel.transform.localEulerAngles.z;
        debug = GameObject.Find("DebugCanvas").GetComponent<DebugMenu>();
    }

    // Update is called once per frame
    void FixedUpdate () {
		if(rb.velocity.sqrMagnitude < maxVelocity)
        {
            rb.AddForce(acceleration * transform.TransformDirection(Vector2.right), ForceMode2D.Force);
        }
        float rotate = steeringWheel.transform.localEulerAngles.z;
        debug.SetTargetRotationText(rotate * brakeSpeed);
        rb.AddTorque(rotate * brakeSpeed);
        debug.SetSpeedText(rb.velocity.magnitude);
        debug.SetCurrentRotationText(rb.rotation);
    }

    private void LateUpdate()
    {
    }
}
