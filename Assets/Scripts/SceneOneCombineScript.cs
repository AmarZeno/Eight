using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class SceneOneCombineScript : mouseDrag {


    //public float speed;
    //public Transform target;

    //private Vector3 zAxis = new Vector3(0, 0, 1);
    void FixedUpdate() {
   // transform.RotateAround(target.position, zAxis, speed);
}

    void OnTriggerEnter2D(Collider2D other) {

        GameObject draggedObject = gameObject;
        GameObject collidedObject = other.GetComponent<Collider2D>().gameObject;


        if (draggedObject.name == "Hydrogen1" && collidedObject.name == "Hydrogen2") {

        } else if (draggedObject.name == "Hydrogen2" && collidedObject.name == "Hydrogen1") {

        } else if (collidedObject.name == "Hydrogen1")
        {
            collidedObject.GetComponent<RelativeJoint2D>().enabled = true;
        }
        else if (collidedObject.name == "Hydrogen2")
        {
            collidedObject.GetComponent<RelativeJoint2D>().enabled = true;
        }
        else if (collidedObject.name == "Oxygen")
        {
            draggedObject.GetComponent<RelativeJoint2D>().enabled = true;
        }
    }

    public override void OnEndDrag(PointerEventData eventData) {
        if (gameObject.name == "Hydrogen2")
        {
            // ANimate to show the default electron position state
        }
        else if (gameObject.name == "Oxygen") {

        }
    }
}
