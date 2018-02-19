using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour {
    Text speed;
    Text targetRotation;
    Text currentRotation;


	// Use this for initialization
	void Start () {
        speed = GameObject.Find("Speed").GetComponent<Text>();
        targetRotation = GameObject.Find("TargetRotation").GetComponent<Text>();
        currentRotation = GameObject.Find("CurrentRotation").GetComponent<Text>();
    }
	
	public void SetSpeedText(float speedValue)
    {
        speed.text = "Speed : " + speedValue.ToString();
    }

    public void SetTargetRotationText(float targetRotationValue)
    {
        targetRotation.text = "TargetRotation : " + targetRotationValue.ToString();
    }

    public void SetCurrentRotationText(float currentRotationValue)
    {
        currentRotation.text = "CurrentRotation : " + currentRotationValue.ToString();
    }
}
