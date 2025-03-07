﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Took this from https://answers.unity.com/questions/29183/2d-camera-smooth-follow.html and updated it to our unity version.
public class CameraFollow : MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    private Camera cam;

    private void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 point = cam.WorldToScreenPoint(target.position);
            Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}
