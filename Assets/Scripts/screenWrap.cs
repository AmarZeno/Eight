using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class screenWrap : MonoBehaviour {

    public Canvas canvas;

    void Update () {
        ScreenWrap();
    }

    void ScreenWrap() {

        // Update local position if game object moves beyond the canvas(Also include offset for gameObject centre anchor point)
        // 1) Take the difference of the position that bounced over the boundary
        // 2) Subtract the difference to reposition the object inside the boundary

        // Left boundary check
        if ((gameObject.transform.localPosition.x - gameObject.GetComponent<RectTransform>().rect.width / 2) < -(canvas.GetComponent<RectTransform>().rect.width / 2))
        {    
            float difference = (gameObject.transform.localPosition.x - gameObject.GetComponent<RectTransform>().rect.width / 2) - (-canvas.GetComponent<RectTransform>().rect.width / 2);
            transform.localPosition = new Vector2(gameObject.transform.localPosition.x - difference, transform.localPosition.y);
        } 
        // Right Boundary Check
        else if((gameObject.transform.localPosition.x + gameObject.GetComponent<RectTransform>().rect.width / 2) > canvas.GetComponent<RectTransform>().rect.width / 2)
        {
            float difference = (gameObject.transform.localPosition.x + gameObject.GetComponent<RectTransform>().rect.width / 2) - canvas.GetComponent<RectTransform>().rect.width / 2;
            transform.localPosition = new Vector2(gameObject.transform.localPosition.x - difference, transform.localPosition.y);
        }
        // Top boundary check
        else if ((gameObject.transform.localPosition.y + gameObject.GetComponent<RectTransform>().rect.height / 2) > canvas.GetComponent<RectTransform>().rect.height / 2)
        {
            float difference = (gameObject.transform.localPosition.y + gameObject.GetComponent<RectTransform>().rect.height / 2) - canvas.GetComponent<RectTransform>().rect.height / 2;
            transform.localPosition = new Vector2(transform.localPosition.x, gameObject.transform.localPosition.y - difference);
        }
        // Bottom boundary check
        else if ((gameObject.transform.localPosition.y - gameObject.GetComponent<RectTransform>().rect.height / 2) < (-canvas.GetComponent<RectTransform>().rect.height / 2))
        {
            float difference = (gameObject.transform.localPosition.y - gameObject.GetComponent<RectTransform>().rect.height / 2) - (-canvas.GetComponent<RectTransform>().rect.height / 2);
            transform.localPosition = new Vector2(transform.localPosition.x, gameObject.transform.localPosition.y - difference);
        }

    }
}
