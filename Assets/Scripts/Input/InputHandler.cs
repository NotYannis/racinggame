using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputHandler : MonoBehaviour {
    public PlayerController player;

    public Graphic swipeZone;

    CommandBeginRotate startSwipe = new CommandBeginRotate();
    CommandRotate swipe = new CommandRotate();
    CommandEndRotate stopSwipe = new CommandEndRotate();


    float xScreenCenter;

	// Use this for initialization
	void Start () {
        xScreenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.0f, 0.0f)).x;
	}
	
	// Update is called once per frame
	void Update () {
        Command command = HandleInput();
        if(command != null)
        {
            command.Execute(player);
        }
	}

    public Command HandleInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                return startSwipe;
            }
            if (Input.GetTouch(i).position.x < xScreenCenter)
            {
                swipe.deltaPosition = Input.GetTouch(i).deltaPosition;
                return swipe;
            }
            if(Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                return stopSwipe;
            }
        }

        if (Input.GetButtonDown("HorizontalButton"))
        {
            return startSwipe;
        }
        if (Input.GetButtonUp("HorizontalButton"))
        {
            return stopSwipe;
        }

        if (Input.GetAxis("HorizontalAxis") != 0.0f)
        {
            swipe.deltaPosition.x = Input.GetAxis("HorizontalAxis") * 8;
            return swipe;
        }


        return null;
    }
}

public abstract class Command
{
    public abstract void Execute(PlayerController player);
}

public class CommandRotate : Command
{
    public Vector2 deltaPosition;

    public override void Execute(PlayerController player)
    {
        player.Rotate(deltaPosition.x);
    }
}

public class CommandEndRotate : Command
{
    public override void Execute(PlayerController player)
    {
        player.EndRotate();
    }
}

public class CommandBeginRotate : Command
{
    public override void Execute(PlayerController player)
    {
        player.BeginRotate();
    }
}