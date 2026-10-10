using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Camera camera;
    public Collider2D playerCollider;
    public Rigidbody2D body;
    bool trackingMouse = false;
    Vector3 lastTrackedPosition;
    float lastTrackedTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("W player controller");
    }

    // Update is called once per frame
    void Update()
    {
        var speed = Time.deltaTime*3.0f;
        var movement = new Vector3();
        if (Keyboard.current.aKey.isPressed)
        {
            movement += new Vector3(-speed, 0);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            movement += new Vector3(speed, 0);
        }
        if (Keyboard.current.wKey.isPressed)
        {
            movement += new Vector3(0, speed);
        }
        if (Keyboard.current.sKey.isPressed)
        {
            movement += new Vector3(0, -speed);
        }
        this.gameObject.transform.position += movement;
        
        Vector3 targetPosition;
        if (trackingMouse)
        {
            Vector3 mouseWorldPosition = ToVec3(camera.ScreenToWorldPoint(ToVec3(Mouse.current.position.ReadValue(), 0)), 0);
            targetPosition = (ClampedDistance(mouseWorldPosition, gameObject.transform.position, 5.0f) + this.gameObject.transform.position) / 2f;
        // } else if (Time.time-lastTrackedTime < 1f && lastTrackedPosition != null)
        // {
        //     float time = (Time.time-lastTrackedTime)/2f + 0.5f;
        //     targetPosition = Vector3.Lerp(lastTrackedPosition, gameObject.transform.position, Mathf.Pow(time, 4f));
        } else
        {
            targetPosition = gameObject.transform.position;
        }
        Vector3 targetZ = ToVec3(targetPosition, camera.transform.position.z);
        Vector3 cameraDelta = targetZ-camera.transform.position;
        camera.transform.position += cameraDelta*0.01f;
    }

    void OnMouseDown()
    {
        trackingMouse = true;
        Debug.Log("onMouseDown");
    }

    void OnMouseUp()
    {
        if (trackingMouse)
        {
            Vector3 mouseWorldPosition = ToVec3(camera.ScreenToWorldPoint(ToVec3(Mouse.current.position.ReadValue(), 0)), 0);
            lastTrackedPosition = mouseWorldPosition;
            lastTrackedTime = Time.time;
            Vector3 delta = mouseWorldPosition-gameObject.transform.position;
            body.AddForce(delta*50.0f);
        }
        trackingMouse = false;
        Debug.Log("onMouseUp");

    }

    Vector3 ClampedDistance(Vector3 position, Vector3 from, float distance)
    {
        Vector3 delta = position-from;
        if (delta.magnitude == 0)
        {
            return position;
        }
        return from + delta.normalized * Math.Min(delta.magnitude, distance);
    }

    Vector3 ToVec3(Vector2 xy, float z)
    {
        return new Vector3(xy.x, xy.y, z);
    }
}