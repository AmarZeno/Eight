using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class LevelOneBond : mouseDrag {

    // Constants
    readonly Vector2 hydrogenAtomLinearOffset = new Vector2(0, 11);

    // Variables
    private GameObject draggedAtom;
    private bool isBondingFailed = false;

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        draggedAtom = eventData.pointerDrag.gameObject;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Bond(other);
    }

    void Bond(Collider2D otherAtomCollider) {

        // Prevent bonding when the user is not dragging an atom and when he makes a wrong bonding
        if (draggedAtom == null || isBondingFailed == true)
            return;

        GameObject collidedAtom = otherAtomCollider.GetComponent<Collider2D>().gameObject;

        // Handle three collision cases
        // 1) When the dragged atom collides with hydrogen1 atom
        // 2) When the dragged atom collides with hydrogen2 atom
        // 3) When the dragged atom collides with oxygen atom
        switch (collidedAtom.name) {
            case "Hydrogen1":
                {
                    if (draggedAtom.name == "Hydrogen2")
                    {
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomLinearOffset;
                        isBondingFailed = true;
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            case "Hydrogen2":
                {
                    if (draggedAtom.name == "Hydrogen1")
                    {
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomLinearOffset;
                        isBondingFailed = true;
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            case "Oxygen":
                draggedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                break;
            default:
                // Do nothing
                break;
        }

        // Reset draggedAtom
        draggedAtom = null;
    }
    
}
