using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SwipeHandler : MonoBehaviour
{
    private Vector2 previousDelta;
    public Vector2 delta;
    public bool touch;

    private void Start()
    {
        Graphic gr = GetComponent<Graphic>();
        print(gr);
    }
}
