﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class LevelTwoBond : MouseDrag {

    // Constants
    readonly Vector2 fluorineAtomLinearOffset = new Vector2(0, 19);

    // Variables
    public GameObject atomProperties;

    private GameObject draggedAtom = null;
    private LevelTwoAtomProperties atomPropertiesScript;


    // Particle effects

    public ParticleSystem[] successParticleEffects;
    public ParticleSystem[] failureParticleEffects;

    public GameObject finisherParticleSystem;

    void Start() {
        atomPropertiesScript = atomProperties.GetComponent<LevelTwoAtomProperties>();
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
		if (draggedAtom == null)
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
                    // Break if already made a bond
                    if (atomPropertiesScript.flourineAtomListStates[0] == LevelTwoAtomProperties.AtomBondingState.Successful || atomPropertiesScript.flourineAtomListStates[0] == LevelTwoAtomProperties.AtomBondingState.Failed)
                        break;

                    if (draggedAtom.name == "Fluorine2" || draggedAtom.name == "Fluorine3" || draggedAtom.name == "Fluorine4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
						atomPropertiesScript.flourineAtomListStates [0] = LevelTwoAtomProperties.AtomBondingState.Failed;
						atomPropertiesScript.flourineAtomListStates [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelTwoAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);

                        // Trigger failure particle effects for the involved atoms
                        failureParticleEffects[0].Play();
                        failureParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    else {
						atomPropertiesScript.flourineAtomListStates [0] = LevelTwoAtomProperties.AtomBondingState.Successful;

                        // Trigger success particle effects for the involved atoms
                        successParticleEffects[0].Play();
                        successParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<LevelTwoBond> ().enabled = false;
                }
                break;
            case "Fluorine2":
                {
                    // Break if already made a bond
					if (atomPropertiesScript.flourineAtomListStates[1] == LevelTwoAtomProperties.AtomBondingState.Successful || atomPropertiesScript.flourineAtomListStates[1] == LevelTwoAtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Fluorine1" || draggedAtom.name == "Fluorine3" || draggedAtom.name == "Fluorine4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
						atomPropertiesScript.flourineAtomListStates [1] = LevelTwoAtomProperties.AtomBondingState.Failed;
						atomPropertiesScript.flourineAtomListStates [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelTwoAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);

                        // Trigger failure particle effects for the involved atoms
                        failureParticleEffects[1].Play();
                        failureParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    else {
						atomPropertiesScript.flourineAtomListStates [1] = LevelTwoAtomProperties.AtomBondingState.Successful;

                        // Trigger success particle effects for the involved atoms
                        successParticleEffects[1].Play();
                        successParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<LevelTwoBond> ().enabled = false;
                }
                break;
            case "Fluorine3":
                {
                    // Break if already made a bond
					if (atomPropertiesScript.flourineAtomListStates[2] == LevelTwoAtomProperties.AtomBondingState.Successful || atomPropertiesScript.flourineAtomListStates[2] == LevelTwoAtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Fluorine1" || draggedAtom.name == "Fluorine2" || draggedAtom.name == "Fluorine4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
						atomPropertiesScript.flourineAtomListStates [2] = LevelTwoAtomProperties.AtomBondingState.Failed;
						atomPropertiesScript.flourineAtomListStates [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelTwoAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);

                        // Trigger failure particle effects for the involved atoms
                        failureParticleEffects[2].Play();
                        failureParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    else {
						atomPropertiesScript.flourineAtomListStates [2] = LevelTwoAtomProperties.AtomBondingState.Successful;

                        // Trigger success particle effects for the involved atoms
                        successParticleEffects[2].Play();
                        successParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<LevelTwoBond> ().enabled = false;
                }
                break;
            case "Fluorine4":
                {
                    // Break if already made a succesful bond
					if (atomPropertiesScript.flourineAtomListStates[3] == LevelTwoAtomProperties.AtomBondingState.Successful || atomPropertiesScript.flourineAtomListStates[3] == LevelTwoAtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Fluorine1" || draggedAtom.name == "Fluorine2" || draggedAtom.name == "Fluorine3")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = fluorineAtomLinearOffset;
						atomPropertiesScript.flourineAtomListStates [3] = LevelTwoAtomProperties.AtomBondingState.Failed;
						atomPropertiesScript.flourineAtomListStates [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelTwoAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);

                        // Trigger failure particle effects for the involved atoms
                        failureParticleEffects[3].Play();
                        failureParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    else {
						atomPropertiesScript.flourineAtomListStates [3] = LevelTwoAtomProperties.AtomBondingState.Successful;

                        // Trigger success particle effects for the involved atoms
                        successParticleEffects[3].Play();
                        successParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<LevelTwoBond> ().enabled = false;
                }
                break;
		case "Carbon":
				draggedAtom.GetComponent<RelativeJoint2D> ().enabled = true;
				draggedAtom.GetComponent<LevelTwoBond> ().enabled = false;
                switch (draggedAtom.name) {
					case "Fluorine1":
						atomPropertiesScript.flourineAtomListStates [0] = LevelTwoAtomProperties.AtomBondingState.Successful;
                        break;
                    case "Fluorine2":
						atomPropertiesScript.flourineAtomListStates[1] = LevelTwoAtomProperties.AtomBondingState.Successful;
                        break;
                    case "Fluorine3":
						atomPropertiesScript.flourineAtomListStates[2] = LevelTwoAtomProperties.AtomBondingState.Successful;
                        break;
                    case "Fluorine4":
						atomPropertiesScript.flourineAtomListStates[3] = LevelTwoAtomProperties.AtomBondingState.Successful;
                        break;
                    default:
                        break;
                }

                // Trigger success particle effects for the involved atoms
                successParticleEffects[4].Play();
                successParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                break;
            default:
                // Do nothing
                break;
        }
        IsStageBondComplete();
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
				if (atomPropertiesScript.flourineAtomListStates [i - 1] == LevelTwoAtomProperties.AtomBondingState.Successful || atomPropertiesScript.flourineAtomListStates [i - 1] == LevelTwoAtomProperties.AtomBondingState.Failed) {
					return false;
				}
			}
		}
		return true;
	}

    public void IsStageBondComplete()
    {
        if (atomPropertiesScript.flourineAtomListStates[0] == LevelTwoAtomProperties.AtomBondingState.Successful && atomPropertiesScript.flourineAtomListStates[1] == LevelTwoAtomProperties.AtomBondingState.Successful && atomPropertiesScript.flourineAtomListStates[2] == LevelTwoAtomProperties.AtomBondingState.Successful && atomPropertiesScript.flourineAtomListStates[3] == LevelTwoAtomProperties.AtomBondingState.Successful)
        {
            if (finisherParticleSystem.activeSelf == false)
            {
                finisherParticleSystem.SetActive(true);
            }
            StartCoroutine(ProceedToNextLevel());
        }
    }

    IEnumerator ProceedToNextLevel()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(3);
    }

}
