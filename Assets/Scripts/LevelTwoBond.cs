using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class LevelTwoBond : mouseDrag {

    // Constants
    readonly Vector2 fluorineAtomLinearOffset = new Vector2(0, 11);
    const int maxSuccessfulBond = 4;

    // Variables
    private GameObject draggedAtom;
    private bool isBondingFailed = false;
    private int currentSuccessfullBond = 0;
   

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
        if (draggedAtom == null || isBondingFailed == true || currentSuccessfullBond == maxSuccessfulBond)
            return;

        GameObject collidedAtom = otherAtomCollider.GetComponent<Collider2D>().gameObject;

        // Handle three collision cases
        // 1) When the dragged atom collides with Fluorine1 atom
        // 2) When the dragged atom collides with Fluorine2 atom
        // 3) When the dragged atom collides with Fluorine3 atom
        // 4) When the dragged atom collides with Fluorine4 atom
        // 5) When the dragged atom collides with Carbon atom
        switch (collidedAtom.name) {
            case "Fluorine1":
                {
                    if (draggedAtom.name == "Fluorine2" || draggedAtom.name == "Fluorine3" || draggedAtom.name == "Fluorine4")
                    {
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
                        isBondingFailed = true;
                    }
                    else {
                        currentSuccessfullBond = currentSuccessfullBond + 1;
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            case "Fluorine2":
                {
                    if (draggedAtom.name == "Fluorine1" || draggedAtom.name == "Fluorine3" || draggedAtom.name == "Fluorine4")
                    {
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
                        isBondingFailed = true;
                    }
                    else {
                        currentSuccessfullBond = currentSuccessfullBond + 1;
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            case "Fluorine3":
                {
                    if (draggedAtom.name == "Fluorine1" || draggedAtom.name == "Fluorine2" || draggedAtom.name == "Fluorine4")
                    {
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
                        isBondingFailed = true;
                    }
                    else {
                        currentSuccessfullBond = currentSuccessfullBond + 1;
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            case "Fluorine4":
                {
                    if (draggedAtom.name == "Fluorine1" || draggedAtom.name == "Fluorine2" || draggedAtom.name == "Fluorine3")
                    {
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
                        isBondingFailed = true;
                    }
                    else {
                        currentSuccessfullBond = currentSuccessfullBond + 1;
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            case "Carbon":
                draggedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                currentSuccessfullBond = currentSuccessfullBond + 1;
                break;
            default:
                // Do nothing
                break;
        }

        // Reset draggedAtom
        draggedAtom = null;
    }
    
}
