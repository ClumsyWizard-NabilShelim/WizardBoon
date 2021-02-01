using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed = 20.0f;

    public Vector2 minScreenBound;
    public Vector2 maxScreenBound;

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetButton("Up"))
        {
            pos.y += movementSpeed * Time.deltaTime;
        }
        if (Input.GetButton("Down"))
        {
            pos.y -= movementSpeed * Time.deltaTime;
        }
        if (Input.GetButton("Left"))
        {
            pos.x -= movementSpeed * Time.deltaTime;
        }
        if (Input.GetButton("Right"))
        {
            pos.x += movementSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, minScreenBound.x, maxScreenBound.x);
        pos.y = Mathf.Clamp(pos.y, minScreenBound.y, maxScreenBound.y);

        transform.position = pos;
    }
}
