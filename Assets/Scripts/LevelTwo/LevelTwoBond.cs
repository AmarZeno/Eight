using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class LevelTwoBond : MouseDrag {

    // Constants
    readonly Vector2 fluorineAtomLinearOffset = new Vector2(0, 19);

    // Variables
    public GameObject atomProperties;

    private GameObject draggedAtom = null;
    private bool isBondingFailed = false;
    private AtomProperties atomPropertiesScript;
	private List<AtomProperties.AtomBondingState> fluorineAtomList = new List<AtomProperties.AtomBondingState>();

    void Start() {
        atomPropertiesScript = atomProperties.GetComponent<AtomProperties>();
		AddFlourineAtoms ();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        draggedAtom = eventData.pointerDrag.gameObject;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Bond(other);
    }

	void AddFlourineAtoms(){
		fluorineAtomList.Add (atomPropertiesScript.flourineOneBondState);
		fluorineAtomList.Add (atomPropertiesScript.flourineTwoBondState);
		fluorineAtomList.Add (atomPropertiesScript.flourineThreeBondState);
		fluorineAtomList.Add (atomPropertiesScript.flourineFourBondState);
	}

    void Bond(Collider2D otherAtomCollider) {

        // Prevent bonding when the user is not dragging an atom and when he makes a wrong bonding
        if (draggedAtom == null || isBondingFailed == true)
            return;

        GameObject collidedAtom = otherAtomCollider.GetComponent<Collider2D>().gameObject;

        // Handle five collision cases
        // 1) When the dragged atom collides with Fluorine1 atom
        // 2) When the dragged atom collides with Fluorine2 atom
        // 3) When the dragged atom collides with Fluorine3 atom
        // 4) When the dragged atom collides with Fluorine4 atom
        // 5) When the dragged atom collides with Carbon atom
        switch (collidedAtom.name) {
            case "Fluorine1":
                {
                    // Break if already made a succesful bond
                    if (atomPropertiesScript.flourineAtomListStates[0] == AtomProperties.AtomBondingState.Successful || atomPropertiesScript.flourineAtomListStates[0] == AtomProperties.AtomBondingState.Failed)
                        break;

                    if (draggedAtom.name == "Fluorine2" || draggedAtom.name == "Fluorine3" || draggedAtom.name == "Fluorine4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}

                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
                        isBondingFailed = true;
						atomPropertiesScript.flourineAtomListStates [0] = AtomProperties.AtomBondingState.Failed;
						atomPropertiesScript.flourineAtomListStates [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = AtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                    }
                    else {
						atomPropertiesScript.flourineAtomListStates [0] = AtomProperties.AtomBondingState.Failed;
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            case "Fluorine2":
                {
                    // Break if already made a succesful bond
					if (atomPropertiesScript.flourineAtomListStates[1] == AtomProperties.AtomBondingState.Successful || atomPropertiesScript.flourineAtomListStates[1] == AtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Fluorine1" || draggedAtom.name == "Fluorine3" || draggedAtom.name == "Fluorine4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
                        isBondingFailed = true;
						atomPropertiesScript.flourineAtomListStates [1] = AtomProperties.AtomBondingState.Failed;
						atomPropertiesScript.flourineAtomListStates [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = AtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                    }
                    else {
						atomPropertiesScript.flourineAtomListStates [1] = AtomProperties.AtomBondingState.Failed;
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            case "Fluorine3":
                {
                    // Break if already made a succesful bond
					if (atomPropertiesScript.flourineAtomListStates[2] == AtomProperties.AtomBondingState.Successful || atomPropertiesScript.flourineAtomListStates[2] == AtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Fluorine1" || draggedAtom.name == "Fluorine2" || draggedAtom.name == "Fluorine4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
                        isBondingFailed = true;
						atomPropertiesScript.flourineAtomListStates [2] = AtomProperties.AtomBondingState.Failed;
						atomPropertiesScript.flourineAtomListStates [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = AtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                    }
                    else {
						atomPropertiesScript.flourineAtomListStates [2] = AtomProperties.AtomBondingState.Failed;
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            case "Fluorine4":
                {
                    // Break if already made a succesful bond
					if (atomPropertiesScript.flourineAtomListStates[3] == AtomProperties.AtomBondingState.Successful || atomPropertiesScript.flourineAtomListStates[3] == AtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Fluorine1" || draggedAtom.name == "Fluorine2" || draggedAtom.name == "Fluorine3")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
                        isBondingFailed = true;
						atomPropertiesScript.flourineAtomListStates [3] = AtomProperties.AtomBondingState.Failed;
						atomPropertiesScript.flourineAtomListStates [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = AtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                    }
                    else {
						atomPropertiesScript.flourineAtomListStates [3] = AtomProperties.AtomBondingState.Failed;
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            case "Carbon":
                draggedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                switch (draggedAtom.name) {
                    case "Fluorine1":
                        atomPropertiesScript.flourineOneBondState = AtomProperties.AtomBondingState.Successful;
                        break;
                    case "Fluorine2":
                        atomPropertiesScript.flourineTwoBondState = AtomProperties.AtomBondingState.Successful;
                        break;
                    case "Fluorine3":
                        atomPropertiesScript.flourineThreeBondState = AtomProperties.AtomBondingState.Successful;
                        break;
                    case "Fluorine4":
                        atomPropertiesScript.flourineFourBondState = AtomProperties.AtomBondingState.Successful;
                        break;
                    default:
                        break;
                }
                break;
            default:
                // Do nothing
                break;
        }

        // Reset draggedAtom
        draggedAtom = null;
    }

    public void TriggerShellRotation(GameObject collidedAtom, GameObject draggedAtom)
    {
        Transform collidedAtomShellTransform;
        Transform draggedAtomShellTransform;
        switch (collidedAtom.name)
        {
            case "Fluorine1":
            case "Fluorine2":
            case "Fluorine3":
            case "Fluorine4":
                collidedAtomShellTransform = collidedAtom.transform.FindChild("Shell").transform;
                collidedAtomShellTransform.localEulerAngles = new Vector3(collidedAtomShellTransform.localRotation.x, collidedAtomShellTransform.localRotation.y, 0);
                draggedAtomShellTransform = draggedAtom.transform.FindChild("Shell").transform;
                draggedAtomShellTransform.localEulerAngles = new Vector3(draggedAtomShellTransform.localRotation.x, draggedAtomShellTransform.localRotation.y, 0);
                break;
            case "Carbon":
                break;
            default:
                break;
        }
    }

	bool CanDraggedAtomBond() {
		// Check states for the dragged atom
		for(int i = 1; i<atomPropertiesScript.flourineAtomListStates.Count; i++){
			if(i == draggedAtom.name [draggedAtom.name.Length - 1]){
				if (atomPropertiesScript.flourineAtomListStates [i - 1] == AtomProperties.AtomBondingState.Successful || atomPropertiesScript.flourineAtomListStates [i - 1] == AtomProperties.AtomBondingState.Failed) {
					return false;
				}
			}
		}
		return true;
	}

}
