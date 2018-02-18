using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures.TransformGestures;
using TouchScript.Gestures;


public class SteeringWheel : MonoBehaviour {
    public float forceFeedback;
    public float rotationSpeed;

    TransformGesture swipe;
    ReleaseGesture release;
    PressGesture press;

    Coroutine forceFeedbackCoroutine;
    Quaternion baseRotation;
    private void Awake()
    {
        swipe = GetComponent<TransformGesture>();
        release = GetComponent<ReleaseGesture>();
        press = GetComponent<PressGesture>();
    }

    private void OnEnable()
    {
        swipe.Transformed += Rotate;
        release.Released += Release;
        press.Pressed += Press;
    }

    private void OnDisable()
    {
        swipe.Transformed -= Rotate;
        release.Released -= Release;
        press.Pressed -= Press;
    }

    public void Rotate(object sender, System.EventArgs e)
    {
        print(swipe.DeltaPosition);
        this.transform.Rotate(new Vector3(0.0f, 0.0f, swipe.DeltaPosition.x * rotationSpeed));
    }

    public void Press(object sender, System.EventArgs e)
    { 
        if(forceFeedbackCoroutine != null)
        {
            StopCoroutine(forceFeedbackCoroutine);
        }
    }

    public void Release(object sender, System.EventArgs e)
    {
        if (forceFeedbackCoroutine != null)
        {
            StopCoroutine(forceFeedbackCoroutine);
        }
        forceFeedbackCoroutine = StartCoroutine(ForceFeedback());
    }


    IEnumerator ForceFeedback()
    {
        while(this.transform.localEulerAngles.z != 0.0f)
        {
            this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(this.transform.localEulerAngles.z, 0.0f, forceFeedback));
            yield return null;
        }

        yield return null;
    }
}
