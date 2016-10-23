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
        //if (other.GetComponent<CircleCollider2D>().name == "Hydrogen1")
        //{
        //    other.GetComponent<HingeJoint2D>().enabled = true;
        //}
        //else if (other.GetComponent<CircleCollider2D>().name == "Hydrogen2")
        //{
        //    other.GetComponent<HingeJoint2D>().enabled = true;
        //}
        //else if (other.GetComponent<CircleCollider2D>().name == "Oxygen") {
        //    gameObject.GetComponent<HingeJoint2D>().enabled = true;
        //}
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
