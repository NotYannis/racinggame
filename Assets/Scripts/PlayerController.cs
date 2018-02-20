using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {
    public float acceleration;
    public float maxVelocity;
    public float brakeSpeed;
    public float rollingRotateLinearDrag;
    public float rollingNormalLinearDrag;

    [System.NonSerialized]
    public DebugMenu debug;
    [System.NonSerialized]
    public Rigidbody2D rb;

    public GameObject steeringWheel;

    public Vector2 rotation;
    PlayerSate currentState;

    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody2D>();

        steeringWheel = GameObject.Find("SteeringWheel");

        debug = GameObject.Find("DebugCanvas").GetComponent<DebugMenu>();
        currentState = new RollingState();
    }

    // Update is called once per frame
    void FixedUpdate () {
        currentState.UpdateState(this);

        HandleDebug();
    }

    private void HandleDebug()
    {
        debug.SetSpeedText(rb.velocity.magnitude);
        debug.SetCurrentRotationText(rb.rotation);
    }

    public void BeginRotate()
    {
        currentState.OnBeginRotate(this);
    }

    public void Rotate(float rotation)
    {
        currentState.OnRotate(this, rotation);
    }

    public void EndRotate()
    {
        currentState.OnEndRotate(this);
    }


}


public abstract class PlayerSate
{
    public abstract PlayerSate SwitchToState(PlayerController player, PlayerSate nextState);
    public abstract void UpdateState(PlayerController player); //Acceleration
    public abstract void EnterState(PlayerController player);

    public abstract void OnBeginRotate(PlayerController player);
    public abstract void OnRotate(PlayerController player, float rotation);
    public abstract void OnEndRotate(PlayerController player);
}

public class RollingState : PlayerSate
{
    public override PlayerSate SwitchToState(PlayerController player, PlayerSate nextState)
    {
        return null;
    }

    public override void EnterState(PlayerController player)
    {
    }

    public override void UpdateState(PlayerController player)
    {
        if (player.rb.velocity.sqrMagnitude < player.maxVelocity)
        {
            player.rb.AddForce(player.acceleration * player.transform.right, ForceMode2D.Force);
        }
    }

    public override void OnBeginRotate(PlayerController player)
    {
        player.rb.drag = player.rollingRotateLinearDrag;
    }

    public override void OnRotate(PlayerController player, float rotation)
    {
        player.steeringWheel.transform.Rotate(new Vector3(0.0f, 0.0f, -rotation));
        player.transform.Rotate(0.0f, 0.0f, -rotation * player.brakeSpeed);
    }

    public override void OnEndRotate(PlayerController player)
    {
        player.rb.drag = player.rollingNormalLinearDrag;
    }
}