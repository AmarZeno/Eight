using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class LevelFourBond : MouseDrag {
	//TODO Replace AtomProperties
    // Constants
    Vector2 hydrogenAtomOverridenLinearOffset;

    Vector3 hydrogenShellFacingDown;
    Vector3 hydrogenShellFacingUp;

    public GameObject firstCarbonAtom;
    public GameObject secondCarbonAtom;

    // Variables
    public GameObject atomProperties;

    private GameObject draggedAtom = null;
    private LevelFourAtomProperties atomPropertiesScript;

    public GameObject finisherParticleSystem;

    void Start() {
		atomPropertiesScript = atomProperties.GetComponent<LevelFourAtomProperties>();

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
				if (atomPropertiesScript.hydrogenAtomStateList[0] == LevelFourAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[0] == LevelFourAtomProperties.AtomBondingState.Failed)
                        break;

                    if (draggedAtom.name == "Hydrogen2" || draggedAtom.name == "Hydrogen3" || draggedAtom.name == "Hydrogen4" || draggedAtom.name == "Hydrogen5" || draggedAtom.name == "Hydrogen6")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}

                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomOverridenLinearOffset;
				    	atomPropertiesScript.hydrogenAtomStateList [0] = LevelFourAtomProperties.AtomBondingState.Failed;
					    atomPropertiesScript.hydrogenAtomStateList [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelFourAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                        collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                    }
                    else if (draggedAtom.name == "Carbon1") {
                        // Break if first carbon has completed its bonds
                        if (atomPropertiesScript.firstCarbonSuccessBonds == 3)
                        {
                            break;
                        }
                        MakeSuccessfulBond(draggedAtom, collidedAtom);
                    }
                    else if (draggedAtom.name == "Carbon2")
                    {
                        // Break if first carbon has completed its bonds
                        if (atomPropertiesScript.secondCarbonSuccessBonds == 6)
                        {
                            break;
                        }
                        MakeSuccessfulBond(draggedAtom, collidedAtom);
                    }
					collidedAtom.GetComponent<LevelFourBond> ().enabled = false;
                }
                break;
            case "Hydrogen2":
                {
                    // Break if already made a succesful bond
				if (atomPropertiesScript.hydrogenAtomStateList[1] == LevelFourAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[1] == LevelFourAtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Hydrogen1" || draggedAtom.name == "Hydrogen3" || draggedAtom.name == "Hydrogen4" || draggedAtom.name == "Hydrogen5" || draggedAtom.name == "Hydrogen6")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomOverridenLinearOffset;
						atomPropertiesScript.hydrogenAtomStateList [1] = LevelFourAtomProperties.AtomBondingState.Failed;
						atomPropertiesScript.hydrogenAtomStateList [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelFourAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                        collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                    }
                    else if (draggedAtom.name == "Carbon1")
                    {
                        // Break if first carbon has completed its bonds
                        if (atomPropertiesScript.firstCarbonSuccessBonds == 3)
                        {
                            break;
                        }
                        MakeSuccessfulBond(draggedAtom, collidedAtom);
                    }
                    else if (draggedAtom.name == "Carbon2")
                    {
                        // Break if first carbon has completed its bonds
                        if (atomPropertiesScript.secondCarbonSuccessBonds == 6)
                        {
                            break;
                        }
                        MakeSuccessfulBond(draggedAtom, collidedAtom);
                    }
					collidedAtom.GetComponent<LevelFourBond> ().enabled = false;
                }
                break;
            case "Hydrogen3":
                {
                    // Break if already made a succesful bond
				if (atomPropertiesScript.hydrogenAtomStateList[2] == LevelFourAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[2] == LevelFourAtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Hydrogen1" || draggedAtom.name == "Hydrogen2" || draggedAtom.name == "Hydrogen4" || draggedAtom.name == "Hydrogen5" || draggedAtom.name == "Hydrogen6")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomOverridenLinearOffset;
					    atomPropertiesScript.hydrogenAtomStateList [2] = LevelFourAtomProperties.AtomBondingState.Failed;
					    atomPropertiesScript.hydrogenAtomStateList [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelFourAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                        collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                    }
                    else if (draggedAtom.name == "Carbon1")
                    {
                        // Break if first carbon has completed its bonds
                        if (atomPropertiesScript.firstCarbonSuccessBonds == 3)
                        {
                            break;
                        }
                        MakeSuccessfulBond(draggedAtom, collidedAtom);
                    }
                    else if (draggedAtom.name == "Carbon2")
                    {
                        // Break if first carbon has completed its bonds
                        if (atomPropertiesScript.secondCarbonSuccessBonds == 6)
                        {
                            break;
                        }
                        MakeSuccessfulBond(draggedAtom, collidedAtom);
                    }
					collidedAtom.GetComponent<LevelFourBond> ().enabled = false;
                }
                break;
            case "Hydrogen4":
                {
                    // Break if already made a succesful bond
				if (atomPropertiesScript.hydrogenAtomStateList[3] == LevelFourAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[3] == LevelFourAtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Hydrogen1" || draggedAtom.name == "Hydrogen2" || draggedAtom.name == "Hydrogen3" || draggedAtom.name == "Hydrogen5" || draggedAtom.name == "Hydrogen6")
                    {
						if (CanDraggedAtomBond () == false) {
							break;
						}
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomOverridenLinearOffset;
					    atomPropertiesScript.hydrogenAtomStateList [3] = LevelFourAtomProperties.AtomBondingState.Failed;
					    atomPropertiesScript.hydrogenAtomStateList [(Convert.ToInt32(draggedAtom.name [draggedAtom.name.Length - 1].ToString()) - 1)] = LevelFourAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                        collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                    }
                    else if (draggedAtom.name == "Carbon1")
                    {
                        // Break if first carbon has completed its bonds
                        if (atomPropertiesScript.firstCarbonSuccessBonds == 3)
                        {
                            break;
                        }
                        MakeSuccessfulBond(draggedAtom, collidedAtom);
                    }
                    else if (draggedAtom.name == "Carbon2")
                    {
                        // Break if first carbon has completed its bonds
                        if (atomPropertiesScript.secondCarbonSuccessBonds == 6)
                        {
                            break;
                        }
                        MakeSuccessfulBond(draggedAtom, collidedAtom);
                    }
					collidedAtom.GetComponent<LevelFourBond> ().enabled = false;
                }
                break;
            case "Hydrogen5":
                {
                    // Break if already made a succesful bond
                    if (atomPropertiesScript.hydrogenAtomStateList[3] == LevelFourAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[3] == LevelFourAtomProperties.AtomBondingState.Failed)
                        break;

                    if (draggedAtom.name == "Hydrogen1" || draggedAtom.name == "Hydrogen2" || draggedAtom.name == "Hydrogen3" || draggedAtom.name == "Hydrogen4" || draggedAtom.name == "Hydrogen6")
                    {
                        if (CanDraggedAtomBond() == false)
                        {
                            break;
                        }
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomOverridenLinearOffset;
                        atomPropertiesScript.hydrogenAtomStateList[3] = LevelFourAtomProperties.AtomBondingState.Failed;
                        atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)] = LevelFourAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                        collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                    }
                    else if (draggedAtom.name == "Carbon1")
                    {
                        // Break if first carbon has completed its bonds
                        if (atomPropertiesScript.firstCarbonSuccessBonds == 3)
                        {
                            break;
                        }
                        MakeSuccessfulBond(draggedAtom, collidedAtom);
                    }
                    else if (draggedAtom.name == "Carbon2")
                    {
                        // Break if first carbon has completed its bonds
                        if (atomPropertiesScript.secondCarbonSuccessBonds == 6)
                        {
                            break;
                        }
                        MakeSuccessfulBond(draggedAtom, collidedAtom);
                    }
                    collidedAtom.GetComponent<LevelFourBond>().enabled = false;
                }
                break;
            case "Hydrogen6":
                {
                    // Break if already made a succesful bond
                    if (atomPropertiesScript.hydrogenAtomStateList[3] == LevelFourAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[3] == LevelFourAtomProperties.AtomBondingState.Failed)
                        break;

                    if (draggedAtom.name == "Hydrogen1" || draggedAtom.name == "Hydrogen2" || draggedAtom.name == "Hydrogen3" || draggedAtom.name == "Hydrogen4" || draggedAtom.name == "Hydrogen5")
                    {
                        if (CanDraggedAtomBond() == false)
                        {
                            break;
                        }
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomOverridenLinearOffset;
                        atomPropertiesScript.hydrogenAtomStateList[3] = LevelFourAtomProperties.AtomBondingState.Failed;
                        atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)] = LevelFourAtomProperties.AtomBondingState.Failed;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                        collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                    }
                    else if (draggedAtom.name == "Carbon1")
                    {
                        // Break if first carbon has completed its bonds
                        if (atomPropertiesScript.firstCarbonSuccessBonds == 3)
                        {
                            break;
                        }
                        MakeSuccessfulBond(draggedAtom, collidedAtom);
                    }
                    else if (draggedAtom.name == "Carbon2")
                    {
                        // Break if first carbon has completed its bonds
                        if (atomPropertiesScript.secondCarbonSuccessBonds == 6)
                        {
                            break;
                        }
                        MakeSuccessfulBond(draggedAtom, collidedAtom);
                    }
                    collidedAtom.GetComponent<LevelFourBond>().enabled = false;
                }
                break;
            case "Carbon1":
                if (draggedAtom.name == "Carbon2")
                {
                    // Enable carbon1 joint
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                else
                {
                    // Break if first carbon has completed its bonds
                    if (atomPropertiesScript.firstCarbonSuccessBonds == 3) {
                        break;
                    }
                    draggedAtom.GetComponent<LevelFourBond>().enabled = false;
                    // Inverse the params
                    MakeSuccessfulBond(collidedAtom, draggedAtom);
                }
                break;
            case "Carbon2":
                if (draggedAtom.name == "Carbon1")
                {
                    // Enable carbon1 joint
                    draggedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                else
                {
                    // Break if first carbon has completed its bonds
                    if (atomPropertiesScript.secondCarbonSuccessBonds == 6)
                    {
                        break;
                    }
                    draggedAtom.GetComponent<LevelFourBond>().enabled = false;
                    // Inverse the params
                    MakeSuccessfulBond(collidedAtom, draggedAtom);
                }
                break;
            default:
                // Do nothing
                break;
        }
        IsStageBondComplete();
        // Reset draggedAtom
        draggedAtom = null;
    }

    public void MakeSuccessfulBond(GameObject draggedAtom, GameObject collidedAtom) {
        if (draggedAtom.name == "Carbon1")
        {
            collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = firstCarbonAtom.GetComponent<Rigidbody2D>();
            collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
            atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(collidedAtom.name[collidedAtom.name.Length - 1].ToString()) - 1)] = LevelFourAtomProperties.AtomBondingState.Successful;
            collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = atomPropertiesScript.hydrogenLinearOffsets[atomPropertiesScript.firstCarbonSuccessBonds];
            collidedAtom.transform.GetChild(2).localEulerAngles = atomPropertiesScript.hydrogenShellRotations[atomPropertiesScript.firstCarbonSuccessBonds];
            atomPropertiesScript.firstCarbonSuccessBonds = atomPropertiesScript.firstCarbonSuccessBonds + 1;
        }
        else if (draggedAtom.name == "Carbon2") {
            collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = secondCarbonAtom.GetComponent<Rigidbody2D>();
            collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
            atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(collidedAtom.name[collidedAtom.name.Length - 1].ToString()) - 1)] = LevelFourAtomProperties.AtomBondingState.Successful;
            collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = atomPropertiesScript.hydrogenLinearOffsets[atomPropertiesScript.secondCarbonSuccessBonds];
            collidedAtom.transform.GetChild(2).localEulerAngles = atomPropertiesScript.hydrogenShellRotations[atomPropertiesScript.secondCarbonSuccessBonds];
            atomPropertiesScript.secondCarbonSuccessBonds = atomPropertiesScript.secondCarbonSuccessBonds + 1;
        }
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
				if (atomPropertiesScript.hydrogenAtomStateList [i - 1] == LevelFourAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList [i - 1] == LevelFourAtomProperties.AtomBondingState.Failed) {
					return false;
				}
			}
		}
		return true;
	}

    public void IsStageBondComplete()
    {
        if (atomPropertiesScript.hydrogenAtomStateList[0] == LevelFourAtomProperties.AtomBondingState.Successful && atomPropertiesScript.hydrogenAtomStateList[1] == LevelFourAtomProperties.AtomBondingState.Successful && atomPropertiesScript.hydrogenAtomStateList[2] == LevelFourAtomProperties.AtomBondingState.Successful && atomPropertiesScript.hydrogenAtomStateList[3] == LevelFourAtomProperties.AtomBondingState.Successful
            && atomPropertiesScript.hydrogenAtomStateList[4] == LevelFourAtomProperties.AtomBondingState.Successful && atomPropertiesScript.hydrogenAtomStateList[5] == LevelFourAtomProperties.AtomBondingState.Successful && atomPropertiesScript.hydrogenAtomStateList[6] == LevelFourAtomProperties.AtomBondingState.Successful)
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
        yield return new WaitForSeconds(2f);
       // SceneManager.LoadScene(3);
    }
}
