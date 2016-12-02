using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class LevelThreeBond : MouseDrag {
	//TODO Replace AtomProperties
    // Constants
    readonly Vector2 fluorineAtomLinearOffset = new Vector2(0, 19);

    // Variables
    public GameObject atomProperties;

    private GameObject draggedAtom = null;
    private bool isBondingFailed = false;
    private LevelThreeAtomProperties atomPropertiesScript;

    void Start() {
		atomPropertiesScript = atomProperties.GetComponent<LevelThreeAtomProperties>();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        draggedAtom = eventData.pointerDrag.gameObject;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Bond(other);
    }

    void Bond(Collider2D otherAtomCollider) {

		// Prevent multi touch
		if (Input.touchCount == 2) {
			return;
		}

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
				if (atomPropertiesScript.hydrogenAtomStateList[0] == LevelThreeAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[0] == LevelThreeAtomProperties.AtomBondingState.Failed)
                        break;

                    if (draggedAtom.name == "Fluorine2" || draggedAtom.name == "Fluorine3" || draggedAtom.name == "Fluorine4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}

                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
                        isBondingFailed = true;
					atomPropertiesScript.hydrogenAtomStateList [0] = LevelThreeAtomProperties.AtomBondingState.Failed;
					atomPropertiesScript.hydrogenAtomStateList [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelThreeAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                    }
                    else {
					atomPropertiesScript.hydrogenAtomStateList [0] = LevelThreeAtomProperties.AtomBondingState.Failed;
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<LevelThreeBond> ().enabled = false;
                }
                break;
            case "Fluorine2":
                {
                    // Break if already made a succesful bond
				if (atomPropertiesScript.hydrogenAtomStateList[1] == LevelThreeAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[1] == LevelThreeAtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Fluorine1" || draggedAtom.name == "Fluorine3" || draggedAtom.name == "Fluorine4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
                        isBondingFailed = true;
						atomPropertiesScript.hydrogenAtomStateList [1] = LevelThreeAtomProperties.AtomBondingState.Failed;
						atomPropertiesScript.hydrogenAtomStateList [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelThreeAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                    }
                    else {
					atomPropertiesScript.hydrogenAtomStateList [1] = LevelThreeAtomProperties.AtomBondingState.Failed;
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<LevelThreeBond> ().enabled = false;
                }
                break;
            case "Fluorine3":
                {
                    // Break if already made a succesful bond
				if (atomPropertiesScript.hydrogenAtomStateList[2] == LevelThreeAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[2] == LevelThreeAtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Fluorine1" || draggedAtom.name == "Fluorine2" || draggedAtom.name == "Fluorine4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
                        isBondingFailed = true;
					atomPropertiesScript.hydrogenAtomStateList [2] = LevelThreeAtomProperties.AtomBondingState.Failed;
					atomPropertiesScript.hydrogenAtomStateList [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelThreeAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                    }
                    else {
					atomPropertiesScript.hydrogenAtomStateList [2] = LevelThreeAtomProperties.AtomBondingState.Failed;
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<LevelThreeBond> ().enabled = false;
                }
                break;
            case "Fluorine4":
                {
                    // Break if already made a succesful bond
				if (atomPropertiesScript.hydrogenAtomStateList[3] == LevelThreeAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[3] == LevelThreeAtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Fluorine1" || draggedAtom.name == "Fluorine2" || draggedAtom.name == "Fluorine3")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
                        isBondingFailed = true;
					atomPropertiesScript.hydrogenAtomStateList [3] = LevelThreeAtomProperties.AtomBondingState.Failed;
					atomPropertiesScript.hydrogenAtomStateList [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelThreeAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                    }
                    else {
					atomPropertiesScript.hydrogenAtomStateList [3] = LevelThreeAtomProperties.AtomBondingState.Failed;
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<LevelThreeBond> ().enabled = false;
                }
                break;
            case "Carbon":
                draggedAtom.GetComponent<RelativeJoint2D>().enabled = true;
				draggedAtom.GetComponent<LevelThreeBond> ().enabled = false;
                switch (draggedAtom.name) {
                    case "Fluorine1":
				atomPropertiesScript.hydrogenAtomStateList[0] = LevelThreeAtomProperties.AtomBondingState.Successful;
                        break;
                    case "Fluorine2":
				atomPropertiesScript.hydrogenAtomStateList[1] = LevelThreeAtomProperties.AtomBondingState.Successful;
                        break;
                    case "Fluorine3":
				atomPropertiesScript.hydrogenAtomStateList[2] = LevelThreeAtomProperties.AtomBondingState.Successful;
                        break;
                    case "Fluorine4":
				atomPropertiesScript.hydrogenAtomStateList[3] = LevelThreeAtomProperties.AtomBondingState.Successful;
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
		for(int i = 1; i<atomPropertiesScript.hydrogenAtomStateList.Count; i++){
			if(i == draggedAtom.name [draggedAtom.name.Length - 1]){
				if (atomPropertiesScript.hydrogenAtomStateList [i - 1] == LevelThreeAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList [i - 1] == LevelThreeAtomProperties.AtomBondingState.Failed) {
					return false;
				}
			}
		}
		return true;
	}

}
