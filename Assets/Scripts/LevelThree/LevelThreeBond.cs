using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class LevelThreeBond : MouseDrag {
	//TODO Replace AtomProperties
    // Constants
    Vector2 hydrogenAtomOverridenLinearOffset;

    Vector3 hydrogenShellFacingDown;
    Vector3 hydrogenShellFacingUp;

    // Variables
    public GameObject atomProperties;

    private GameObject draggedAtom = null;
    private LevelThreeAtomProperties atomPropertiesScript;

    // Particle effects

    public ParticleSystem[] successParticleEffects;
    public ParticleSystem[] failureParticleEffects;

    public GameObject finisherParticleSystem;

    void Start() {
		atomPropertiesScript = atomProperties.GetComponent<LevelThreeAtomProperties>();

        hydrogenAtomOverridenLinearOffset = new Vector2(0, -15);

        hydrogenShellFacingUp = new Vector3(0, 0, 0);
        hydrogenShellFacingDown = new Vector3(0, 0, 180);
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
        // 1) When the dragged atom collides with Hydrogen1 atom
        // 2) When the dragged atom collides with Hydrogen2 atom
        // 3) When the dragged atom collides with Hydrogen3 atom
        // 4) When the dragged atom collides with Hydrogen4 atom
        // 5) When the dragged atom collides with Carbon atom
        switch (collidedAtom.name) {
            case "Hydrogen1":
                {
                    // Break if already made a succesful bond
				if (atomPropertiesScript.hydrogenAtomStateList[0] == LevelThreeAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[0] == LevelThreeAtomProperties.AtomBondingState.Failed)
                        break;

                    if (draggedAtom.name == "Hydrogen2" || draggedAtom.name == "Hydrogen3" || draggedAtom.name == "Hydrogen4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}

                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomOverridenLinearOffset;
				    	atomPropertiesScript.hydrogenAtomStateList[0] = LevelThreeAtomProperties.AtomBondingState.Failed;
					    atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelThreeAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);

                        // Trigger failure particle effects for the involved atoms
                        failureParticleEffects[0].Play();
                        failureParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    else {
					    atomPropertiesScript.hydrogenAtomStateList[0] = LevelThreeAtomProperties.AtomBondingState.Successful;

                        // Trigger success particle effects for the involved atoms
                        successParticleEffects[0].Play();
                        successParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<LevelThreeBond> ().enabled = false;
                }
                break;
            case "Hydrogen2":
                {
                    // Break if already made a succesful bond
				if (atomPropertiesScript.hydrogenAtomStateList[1] == LevelThreeAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[1] == LevelThreeAtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Hydrogen1" || draggedAtom.name == "Hydrogen3" || draggedAtom.name == "Hydrogen4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomOverridenLinearOffset;
						atomPropertiesScript.hydrogenAtomStateList [1] = LevelThreeAtomProperties.AtomBondingState.Failed;
						atomPropertiesScript.hydrogenAtomStateList [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelThreeAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);

                        // Trigger failure particle effects for the involved atoms
                        failureParticleEffects[1].Play();
                        failureParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    else {
					    atomPropertiesScript.hydrogenAtomStateList [1] = LevelThreeAtomProperties.AtomBondingState.Successful;

                        // Trigger success particle effects for the involved atoms
                        successParticleEffects[1].Play();
                        successParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<LevelThreeBond> ().enabled = false;
                }
                break;
            case "Hydrogen3":
                {
                    // Break if already made a succesful bond
				if (atomPropertiesScript.hydrogenAtomStateList[2] == LevelThreeAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[2] == LevelThreeAtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Hydrogen1" || draggedAtom.name == "Hydrogen2" || draggedAtom.name == "Hydrogen4")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomOverridenLinearOffset;
					    atomPropertiesScript.hydrogenAtomStateList[2] = LevelThreeAtomProperties.AtomBondingState.Failed;
					    atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelThreeAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);

                        // Trigger failure particle effects for the involved atoms
                        failureParticleEffects[2].Play();
                        failureParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();

                    }
                    else {
				    	atomPropertiesScript.hydrogenAtomStateList[2] = LevelThreeAtomProperties.AtomBondingState.Successful;

                        // Trigger success particle effects for the involved atoms
                        successParticleEffects[2].Play();
                        successParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
                    }
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
					collidedAtom.GetComponent<LevelThreeBond> ().enabled = false;
                }
                break;
            case "Hydrogen4":
                {
                    // Break if already made a succesful bond
				if (atomPropertiesScript.hydrogenAtomStateList[3] == LevelThreeAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[3] == LevelThreeAtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Hydrogen1" || draggedAtom.name == "Hydrogen2" || draggedAtom.name == "Hydrogen3")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomOverridenLinearOffset;
					    atomPropertiesScript.hydrogenAtomStateList[3] = LevelThreeAtomProperties.AtomBondingState.Failed;
					    atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelThreeAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);

                        // Trigger failure particle effects for the involved atoms
                        failureParticleEffects[3].Play();
                        failureParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();

                    }
                    else {
					    atomPropertiesScript.hydrogenAtomStateList[3] = LevelThreeAtomProperties.AtomBondingState.Successful;

                        // Trigger success particle effects for the involved atoms
                        successParticleEffects[3].Play();
                        successParticleEffects[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)].Play();
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
                    case "Hydrogen1":
				            atomPropertiesScript.hydrogenAtomStateList[0] = LevelThreeAtomProperties.AtomBondingState.Successful;
                        break;
                    case "Hydrogen2":
			            	atomPropertiesScript.hydrogenAtomStateList[1] = LevelThreeAtomProperties.AtomBondingState.Successful;
                        break;
                    case "Hydrogen3":
			            	atomPropertiesScript.hydrogenAtomStateList[2] = LevelThreeAtomProperties.AtomBondingState.Successful;
                        break;
                    case "Hydrogen4":
			            	atomPropertiesScript.hydrogenAtomStateList[3] = LevelThreeAtomProperties.AtomBondingState.Successful;
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
        collidedAtom.transform.GetChild(2).localEulerAngles = hydrogenShellFacingDown;
        draggedAtom.transform.GetChild(2).localEulerAngles = hydrogenShellFacingUp;
    }

	bool CanDraggedAtomBond() {
		// Check states for the dragged atom
		for(int i = 1; i<atomPropertiesScript.hydrogenAtomStateList.Count; i++){
			if(i == draggedAtom.name [draggedAtom.name.Length - 1]){
				if (atomPropertiesScript.hydrogenAtomStateList [i - 1] == LevelThreeAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[i - 1] == LevelThreeAtomProperties.AtomBondingState.Failed) {
					return false;
				}
			}
		}
		return true;
	}

    public void IsStageBondComplete()
    {
        if (atomPropertiesScript.hydrogenAtomStateList[0] == LevelThreeAtomProperties.AtomBondingState.Successful && atomPropertiesScript.hydrogenAtomStateList[1] == LevelThreeAtomProperties.AtomBondingState.Successful && atomPropertiesScript.hydrogenAtomStateList[2] == LevelThreeAtomProperties.AtomBondingState.Successful && atomPropertiesScript.hydrogenAtomStateList[3] == LevelThreeAtomProperties.AtomBondingState.Successful)
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
       SceneManager.LoadScene(4);
    }
}
