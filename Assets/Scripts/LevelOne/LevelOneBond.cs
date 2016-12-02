﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class LevelOneBond : MouseDrag {

    // Constants
    readonly Vector2 hydrogenAtomLinearOffset = new Vector2(0, 15);

	// Variables
	public GameObject atomProperties;
    private GameObject draggedAtom;
    private bool isBondingFailed = false;
	private LevelOneAtomProperties atomPropertiesScript;

	void Start(){
		atomPropertiesScript = atomProperties.GetComponent<LevelOneAtomProperties>();
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
					// Break if already made a bond
				if (atomPropertiesScript.hydrogenAtomListStates[0] == LevelOneAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomListStates[0] == LevelOneAtomProperties.AtomBondingState.Failed)
						break;
				
                    if (draggedAtom.name == "Hydrogen2")
                    {
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomLinearOffset;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                        isBondingFailed = true;
                    }
					atomPropertiesScript.hydrogenAtomListStates [0] = LevelOneAtomProperties.AtomBondingState.Failed;
					atomPropertiesScript.hydrogenAtomListStates [1] = LevelOneAtomProperties.AtomBondingState.Failed;
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            case "Hydrogen2":
                {
					// Break if already made a bond
				if (atomPropertiesScript.hydrogenAtomListStates[1] == LevelOneAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomListStates[1] == LevelOneAtomProperties.AtomBondingState.Failed)
						break;
                    if (draggedAtom.name == "Hydrogen1")
                    {
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomLinearOffset;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                        isBondingFailed = true;
                    }
					atomPropertiesScript.hydrogenAtomListStates [0] = LevelOneAtomProperties.AtomBondingState.Failed;
					atomPropertiesScript.hydrogenAtomListStates [1] = LevelOneAtomProperties.AtomBondingState.Failed;
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            case "Oxygen":
                draggedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                atomPropertiesScript.hydrogenAtomListStates[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)] = LevelOneAtomProperties.AtomBondingState.Successful;
                break;
            default:
                // Do nothing
                break;
        }

        // Reset draggedAtom
        draggedAtom = null;
    }

    public void TriggerShellRotation(GameObject collidedAtom, GameObject draggedAtom) {
        Transform collidedAtomShellTransform;
        Transform draggedAtomShellTransform;
        switch (collidedAtom.name)
        {
            case "Hydrogen1":
            case "Hydrogen2":
                collidedAtomShellTransform = collidedAtom.transform.FindChild("Shell").transform;
                collidedAtomShellTransform.localEulerAngles = new Vector3(collidedAtomShellTransform.localRotation.x, collidedAtomShellTransform.localRotation.y, 0);
                draggedAtomShellTransform = draggedAtom.transform.FindChild("Shell").transform;
                draggedAtomShellTransform.localEulerAngles = new Vector3(draggedAtomShellTransform.localRotation.x, draggedAtomShellTransform.localRotation.y, 180);
                break;
            case "Oxygen":
                break;
            default:
                break;
        }
    }
}
