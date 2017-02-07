using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    int speed = 20;
float friction = 0.7f;
float lerpSpeed = 5;
float xDeg;
float yDeg;
    Quaternion fromRotation;
Quaternion toRotation;
 
void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            xDeg -= Input.GetAxis("Mouse X") * speed * friction;
            yDeg += Input.GetAxis("Mouse Y") * speed * friction;
            fromRotation = transform.rotation;
            toRotation = Quaternion.Euler(yDeg, xDeg, 0);
            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
        }
#endif
        if (Input.touchCount > 0 &&
      Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            xDeg -= Input.touches[0].deltaPosition.x * speed * friction;
            yDeg += Input.touches[0].deltaPosition.y * friction;
            fromRotation = transform.rotation;
            toRotation = Quaternion.Euler(yDeg, xDeg, 0);
            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
        }
   }
}
