using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class screenWrap : MonoBehaviour {
    float leftConstraint = 0.0f;
    float rightConstraint = 960.0f;
    float buffer = 10.0f; // set this so the spaceship disappears offscreen before re-appearing on other side

    private Image image;
    public Canvas canvas;
    void Start() {
        // set Vector3 to ( camera left/right limits, spaceship Y, spaceship Z )
        // this will find a world-space point that is relative to the screen
        //   leftConstraint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.05f, 0.0f, 0.0f)).x; // Or set to (0,0,0)
        //   rightConstraint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.95f, 0.0f, 0.0f)).x; // Or set to (Screen.width,0,0)
        leftConstraint = - Screen.width/2;
        rightConstraint = Screen.width/2;
         image =  gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () {
        //ScreenWrap();

        if(gameObject.GetComponent<Image>().transform.localPosition.x < -(Screen.width / 2f ))
        {
            gameObject.GetComponent<Image>().transform.localPosition= Vector2.zero;
        }
    }

    void ScreenWrap() {
        if (transform.position.x < leftConstraint - buffer) { 
            transform.position = new Vector2(rightConstraint + buffer, transform.position.y);
        }

        if (transform.position.x > rightConstraint + buffer) {
            transform.position = new Vector2(leftConstraint - buffer, transform.position.y);
        }
        
    }




}
