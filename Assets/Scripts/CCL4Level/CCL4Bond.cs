using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class CCL4Bond : MouseDrag {
	//TODO Replace AtomProperties
    // Constants
    Vector2 chlorineAtomOverridenLinearOffset;

    Vector3 chlorineShellFacingDown;
    Vector3 chlorineShellFacingUp;

    // Variables
    public GameObject atomProperties;

    private GameObject draggedAtom = null;
    private CCL4AtomProperties atomPropertiesScript;

    // Particle effects

    public ParticleSystem[] successParticleEffects;
    public ParticleSystem[] failureParticleEffects;

    public GameObject finisherParticleSystem;
    public GameObject mainCanvas;

    void Start() {
		atomPropertiesScript = atomProperties.GetComponent<CCL4AtomProperties>();

        chlorineAtomOverridenLinearOffset = new Vector2(0, -15);

        chlorineShellFacingUp = new Vector3(0, 0, 0);
        chlorineShellFacingDown = new Vector3(0, 0, 180);
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
        if (draggedAtom == null)
            return;

        GameObject collidedAtom = otherAtomCollider.GetComponent<Collider2D>().gameObject;

        // Handle five collision cases
        // 1) When the dragged atom collides with Chlorine1 atom
        // 2) When the dragged atom collides with Chlorine2 atom
        // 3) When the dragged atom collides with Chlorine3 atom
        // 4) When the dragged atom collides with Chlorine4 atom
        // 5) When the dragged atom collides with Carbon atom
        switch (collidedAtom.name) {
            case "Chlorine1":
                {
                    // Break if already made a succesful bond
				if (atomPropertiesScript.chlorineAtomStateList[0] == CCL4AtomProperties.AtomBondingState.Successful || atomPropertiesScript.chlorineAtomStateList[0] == CCL4AtomProperties.AtomBondingState.Failed)
                        break;

                    if (draggedAtom.name == "Chlorine2" || draggedAtom.name == "Chlorine3" || draggedAtom.name == "Chlorine4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}

                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = chlorineAtomOverridenLinearOffset;
				    	atomPropertiesScript.chlorineAtomStateList[0] = CCL4AtomProperties.AtomBondingState.Failed;
					    atomPropertiesScript.chlorineAtomStateList[(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = CCL4AtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);

                        // Trigger failure particle effects for the involved atoms
                        failureParticleEffects[0].Play();
                        failureParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    else {
					    atomPropertiesScript.chlorineAtomStateList[0] = CCL4AtomProperties.AtomBondingState.Successful;

                        // Trigger success particle effects for the involved atoms
                        successParticleEffects[0].Play();
                        successParticleEffects[4].Play();
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<CCL4Bond> ().enabled = false;
                }
                break;
            case "Chlorine2":
                {
                    // Break if already made a succesful bond
				if (atomPropertiesScript.chlorineAtomStateList[1] == CCL4AtomProperties.AtomBondingState.Successful || atomPropertiesScript.chlorineAtomStateList[1] == CCL4AtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Chlorine1" || draggedAtom.name == "Chlorine3" || draggedAtom.name == "Chlorine4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = chlorineAtomOverridenLinearOffset;
						atomPropertiesScript.chlorineAtomStateList[1] = CCL4AtomProperties.AtomBondingState.Failed;
						atomPropertiesScript.chlorineAtomStateList[(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = CCL4AtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);

                        // Trigger failure particle effects for the involved atoms
                        failureParticleEffects[1].Play();
                        failureParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    else {
					    atomPropertiesScript.chlorineAtomStateList[1] = CCL4AtomProperties.AtomBondingState.Successful;

                        // Trigger success particle effects for the involved atoms
                        successParticleEffects[1].Play();
                        successParticleEffects[4].Play();
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<CCL4Bond> ().enabled = false;
                }
                break;
            case "Chlorine3":
                {
                    // Break if already made a succesful bond
				if (atomPropertiesScript.chlorineAtomStateList[2] == CCL4AtomProperties.AtomBondingState.Successful || atomPropertiesScript.chlorineAtomStateList[2] == CCL4AtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Chlorine1" || draggedAtom.name == "Chlorine2" || draggedAtom.name == "Chlorine4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = chlorineAtomOverridenLinearOffset;
					    atomPropertiesScript.chlorineAtomStateList[2] = CCL4AtomProperties.AtomBondingState.Failed;
					    atomPropertiesScript.chlorineAtomStateList[(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = CCL4AtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);

                        // Trigger failure particle effects for the involved atoms
                        failureParticleEffects[2].Play();
                        failureParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();

                    }
                    else {
				    	atomPropertiesScript.chlorineAtomStateList[2] = CCL4AtomProperties.AtomBondingState.Successful;

                        // Trigger success particle effects for the involved atoms
                        successParticleEffects[2].Play();
                        successParticleEffects[4].Play();
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<CCL4Bond> ().enabled = false;
                }
                break;
            case "Chlorine4":
                {
                    // Break if already made a succesful bond
				if (atomPropertiesScript.chlorineAtomStateList[3] == CCL4AtomProperties.AtomBondingState.Successful || atomPropertiesScript.chlorineAtomStateList[3] == CCL4AtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Chlorine1" || draggedAtom.name == "Chlorine2" || draggedAtom.name == "Chlorine3")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = chlorineAtomOverridenLinearOffset;
					    atomPropertiesScript.chlorineAtomStateList[3] = CCL4AtomProperties.AtomBondingState.Failed;
					    atomPropertiesScript.chlorineAtomStateList[(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = CCL4AtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);

                        // Trigger failure particle effects for the involved atoms
                        failureParticleEffects[3].Play();
                        failureParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();

                    }
                    else {
					    atomPropertiesScript.chlorineAtomStateList[3] = CCL4AtomProperties.AtomBondingState.Successful;

                        // Trigger success particle effects for the involved atoms
                        successParticleEffects[3].Play();
                        successParticleEffects[4].Play();
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<CCL4Bond> ().enabled = false;
                }
                break;
            case "Carbon":
                draggedAtom.GetComponent<RelativeJoint2D>().enabled = true;
				draggedAtom.GetComponent<CCL4Bond> ().enabled = false;
                switch (draggedAtom.name) {
                    case "Chlorine1":
				            atomPropertiesScript.chlorineAtomStateList[0] = CCL4AtomProperties.AtomBondingState.Successful;
                        break;
                    case "Chlorine2":
			            	atomPropertiesScript.chlorineAtomStateList[1] = CCL4AtomProperties.AtomBondingState.Successful;
                        break;
                    case "Chlorine3":
			            	atomPropertiesScript.chlorineAtomStateList[2] = CCL4AtomProperties.AtomBondingState.Successful;
                        break;
                    case "Chlorine4":
			            	atomPropertiesScript.chlorineAtomStateList[3] = CCL4AtomProperties.AtomBondingState.Successful;
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
        collidedAtom.transform.GetChild(2).localEulerAngles = chlorineShellFacingDown;
        draggedAtom.transform.GetChild(2).localEulerAngles = chlorineShellFacingUp;
    }

	bool CanDraggedAtomBond() {
		// Check states for the dragged atom
		for(int i = 1; i<atomPropertiesScript.chlorineAtomStateList.Count; i++){
			if(i == draggedAtom.name [draggedAtom.name.Length - 1]){
				if (atomPropertiesScript.chlorineAtomStateList[i - 1] == CCL4AtomProperties.AtomBondingState.Successful || atomPropertiesScript.chlorineAtomStateList[i - 1] == CCL4AtomProperties.AtomBondingState.Failed) {
					return false;
				}
			}
		}
		return true;
	}

    public void IsStageBondComplete()
    {
        if (atomPropertiesScript.chlorineAtomStateList[0] == CCL4AtomProperties.AtomBondingState.Successful && atomPropertiesScript.chlorineAtomStateList[1] == CCL4AtomProperties.AtomBondingState.Successful && atomPropertiesScript.chlorineAtomStateList[2] == CCL4AtomProperties.AtomBondingState.Successful && atomPropertiesScript.chlorineAtomStateList[3] == CCL4AtomProperties.AtomBondingState.Successful)
        {
            if (finisherParticleSystem.activeSelf == false)
            {
                finisherParticleSystem.SetActive(true);
            }
            mainCanvas.GetComponent<EightSceneManager>().StageComplete();
        }
    }
}
