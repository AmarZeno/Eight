using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class CH3OHBond : MouseDrag
{

    // Variables
    public GameObject carbonAtom;
    public GameObject oxygenAtom;
    public GameObject atomProperties;
    private CH3OHAtomProperties atomPropertiesScript;
    public GameObject draggedAtom = null;

    // Use this for initialization
    void Start()
    {
        atomPropertiesScript = atomProperties.GetComponent<CH3OHAtomProperties>();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        draggedAtom = eventData.pointerDrag.gameObject;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        draggedAtom = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Bond(other);
    }

    void Bond(Collider2D otherAtomCollider)
    {
        // Prevent bonding when the user is not dragging an atom and when he makes a wrong bonding
        if (draggedAtom == null)
            return;

        GameObject collidedAtom = otherAtomCollider.GetComponent<Collider2D>().gameObject;

        switch (collidedAtom.name)
        {
            case "Hydrogen1":
                {
                    // Break if already made a succesful or a failed bond
                    if (atomPropertiesScript.hydrogenAtomStateList[0] == CH3OHAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[0] == CH3OHAtomProperties.AtomBondingState.Failed)
                        break;
                    if (draggedAtom.name == "Carbon")
                    {
                        if (atomPropertiesScript.carbonSuccessBonds < 4)
                        {
                            MakeSuccessfulBond(draggedAtom, collidedAtom);
                        }
                        else {
                            break;
                        }
                    }
                    else if (draggedAtom.name == "Oxygen")
                    {
                        if (atomPropertiesScript.oxygenSuccessBonds < 6)
                        {
                            MakeSuccessfulBond(draggedAtom, collidedAtom);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else {
                        // For all hydrogen cases
                        MakeHydrogenBonds(draggedAtom, collidedAtom);
                    }
                }
                break;
            case "Hydrogen2":
                {
                    // Break if already made a succesful or a failed bond
                    if (atomPropertiesScript.hydrogenAtomStateList[1] == CH3OHAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[1] == CH3OHAtomProperties.AtomBondingState.Failed)
                        break;
                    if (draggedAtom.name == "Carbon")
                    {
                        if (atomPropertiesScript.carbonSuccessBonds < 4)
                        {
                            MakeSuccessfulBond(draggedAtom, collidedAtom);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (draggedAtom.name == "Oxygen")
                    {
                        if (atomPropertiesScript.oxygenSuccessBonds < 6)
                        {
                            MakeSuccessfulBond(draggedAtom, collidedAtom);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        // For all hydrogen cases
                        MakeHydrogenBonds(draggedAtom, collidedAtom);
                    }
                }
                break;
            case "Hydrogen3":
                {
                    // Break if already made a succesful or a failed bond
                    if (atomPropertiesScript.hydrogenAtomStateList[2] == CH3OHAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[2] == CH3OHAtomProperties.AtomBondingState.Failed)
                        break;
                    if (draggedAtom.name == "Carbon")
                    {
                        if (atomPropertiesScript.carbonSuccessBonds < 4)
                        {
                            MakeSuccessfulBond(draggedAtom, collidedAtom);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (draggedAtom.name == "Oxygen")
                    {
                        if (atomPropertiesScript.oxygenSuccessBonds < 6)
                        {
                            MakeSuccessfulBond(draggedAtom, collidedAtom);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        // For all hydrogen cases
                        MakeHydrogenBonds(draggedAtom, collidedAtom);
                    }
                }
                break;
            case "Hydrogen4":
                {
                    // Break if already made a succesful or a failed bond
                    if (atomPropertiesScript.hydrogenAtomStateList[3] == CH3OHAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomStateList[3] == CH3OHAtomProperties.AtomBondingState.Failed)
                        break;
                    if (draggedAtom.name == "Carbon")
                    {
                        if (atomPropertiesScript.carbonSuccessBonds < 4)
                        {
                            MakeSuccessfulBond(draggedAtom, collidedAtom);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (draggedAtom.name == "Oxygen")
                    {
                        if (atomPropertiesScript.oxygenSuccessBonds < 6)
                        {
                            MakeSuccessfulBond(draggedAtom, collidedAtom);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        // For all hydrogen cases
                        MakeHydrogenBonds(draggedAtom, collidedAtom);
                    }
                }
                break;
            case "Oxygen":
                if (draggedAtom.name == "Carbon")
                {
                    if (atomPropertiesScript.oxygenSuccessBonds == 6)
                    {
                        // Detach and attach the last hydrogen bond
                        atomPropertiesScript.lastBondedHydrogenWithOxygen.GetComponent<RelativeJoint2D>().enabled = false;
                        Vector3 localPosition = atomPropertiesScript.lastBondedHydrogenWithOxygen.gameObject.transform.localPosition;
                        localPosition.x -= 100;
                        atomPropertiesScript.lastBondedHydrogenWithOxygen.gameObject.transform.localPosition = localPosition;
                        atomPropertiesScript.oxygenSuccessBonds -= 1;
                        //Reattach to carbon
                        MakeSuccessfulBond(draggedAtom, atomPropertiesScript.lastBondedHydrogenWithOxygen.gameObject);
                    }
                    // Enable carbon joint
                    draggedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                    atomPropertiesScript.carbonOxygenSuccessBonds += 1;
                   
                }
                else
                {
                    // Break if first carbon has completed its bonds
                    if (atomPropertiesScript.oxygenSuccessBonds < 6)
                    {
                        MakeSuccessfulBond(collidedAtom, draggedAtom);
                    }
                    else
                    {
                        break;
                    }
                    // draggedAtom.GetComponent<LevelFourBond>().enabled = false;
                    // Inverse the params
                }
                break;
            case "Carbon":
                if (draggedAtom.name == "Oxygen")
                {
                    if (atomPropertiesScript.carbonSuccessBonds == 4)
                    {
                        // Detach and attach the last hydrogen bond
                        atomPropertiesScript.lastBondedHydrogenWithCarbon.GetComponent<RelativeJoint2D>().enabled = false;
                        Vector3 localPosition = atomPropertiesScript.lastBondedHydrogenWithCarbon.gameObject.transform.localPosition;
                        localPosition.x += 100;
                        atomPropertiesScript.lastBondedHydrogenWithCarbon.gameObject.transform.localPosition = localPosition;
                        atomPropertiesScript.carbonSuccessBonds -= 1;
                        //Reattach to oxygen
                        MakeSuccessfulBond(draggedAtom, atomPropertiesScript.lastBondedHydrogenWithCarbon.gameObject);
                    }
                    // Enable carbon joint
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                    atomPropertiesScript.carbonOxygenSuccessBonds += 1;
                }
                else
                {
                    
                    if (atomPropertiesScript.carbonSuccessBonds < 4)
                    {
                        GameObject draggedAtom2 = null;
                        if (atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)] == CH3OHAtomProperties.AtomBondingState.Failed) {
                            // Find draggedAtom2 if attached is another hydrogen atom
                            draggedAtom2 = GetAttachedHydrogenAtom(); 
                            draggedAtom.GetComponent<RelativeJoint2D>().enabled = false;
                            Vector3 draggedAtomLocalPosition = draggedAtom.gameObject.transform.localPosition;
                            draggedAtomLocalPosition.y -= 100;
                            
                            if (draggedAtom2 != null)
                            {
                                draggedAtom2.GetComponent<RelativeJoint2D>().enabled = false;
                                Vector3 draggedAtom2LocalPosition = draggedAtom2.gameObject.transform.localPosition;
                                draggedAtom2LocalPosition.y += 100;
                                MakeSuccessfulBond(collidedAtom, draggedAtom2);
                            }
                        }
                        MakeSuccessfulBond(collidedAtom, draggedAtom);
                    }
                    else {
                        break;// Break if first carbon has completed its bonds
                    }
                   // draggedAtom.GetComponent<LevelFourBond>().enabled = false;
                    // Inverse the params
                }
                break;
            default:
                break;
        }
        draggedAtom = null;
        IsStageBondComplete();
    }

    public void MakeHydrogenBonds(GameObject draggedAtom, GameObject collidedAtom) {

        draggedAtom.GetComponent<RelativeJoint2D>().connectedBody = collidedAtom.GetComponent<Rigidbody2D>();
        draggedAtom.GetComponent<RelativeJoint2D>().linearOffset = atomPropertiesScript.hydrogenAtomOverridenLinearOffset;
        collidedAtom.transform.GetChild(2).localEulerAngles = atomPropertiesScript.hydrogenShellFacingDown;
        draggedAtom.transform.GetChild(2).localEulerAngles = atomPropertiesScript.hydrogenShellFacingUp;
        draggedAtom.GetComponent<RelativeJoint2D>().enabled = true;
        atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(draggedAtom.name[draggedAtom.name.Length - 1].ToString()) - 1)] = CH3OHAtomProperties.AtomBondingState.Failed;
        atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(collidedAtom.name[collidedAtom.name.Length - 1].ToString()) - 1)] = CH3OHAtomProperties.AtomBondingState.Failed;

        switch (draggedAtom.name) {
            case "Hydrogen1":
                {
                    if (collidedAtom.name == "Hydrogen2" || collidedAtom.name == "Hydrogen3" || collidedAtom.name == "Hydrogen4") {
                        
                    }
                }
                break;
            case "Hydrogen2":
                {
                    if (collidedAtom.name == "Hydrogen1" || collidedAtom.name == "Hydrogen3" || collidedAtom.name == "Hydrogen4")
                    {

                    }
                }
                break;
            case "Hydrogen3":
                {
                    if (collidedAtom.name == "Hydrogen1" || collidedAtom.name == "Hydrogen2" || collidedAtom.name == "Hydrogen4")
                    {

                    }
                }
                break;
            case "Hydrogen4":
                {
                    if (collidedAtom.name == "Hydrogen1" || collidedAtom.name == "Hydrogen2" || collidedAtom.name == "Hydrogen3")
                    {

                    }
                }
                break;
            default:
                break;
        }
    }

    public void MakeSuccessfulBond(GameObject draggedAtom, GameObject collidedAtom)
    {
        if (draggedAtom.name == "Carbon")
        {
            collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = carbonAtom.GetComponent<Rigidbody2D>();
            collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
            atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(collidedAtom.name[collidedAtom.name.Length - 1].ToString()) - 1)] = CH3OHAtomProperties.AtomBondingState.Successful;
            collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = atomPropertiesScript.hydrogenLinearOffsets[atomPropertiesScript.carbonSuccessBonds];
            collidedAtom.transform.GetChild(2).localEulerAngles = atomPropertiesScript.hydrogenShellRotations[atomPropertiesScript.carbonSuccessBonds];
            atomPropertiesScript.carbonSuccessBonds += 1;
            SetLastBondedAtoms(draggedAtom, collidedAtom);
        }
        else if (draggedAtom.name == "Oxygen")
        {
            collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = oxygenAtom.GetComponent<Rigidbody2D>();
            collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
            atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(collidedAtom.name[collidedAtom.name.Length - 1].ToString()) - 1)] = CH3OHAtomProperties.AtomBondingState.Successful;
            collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = atomPropertiesScript.hydrogenLinearOffsets[atomPropertiesScript.oxygenSuccessBonds];
            collidedAtom.transform.GetChild(2).localEulerAngles = atomPropertiesScript.hydrogenShellRotations[atomPropertiesScript.oxygenSuccessBonds];
            atomPropertiesScript.oxygenSuccessBonds = atomPropertiesScript.oxygenSuccessBonds + 1;
            SetLastBondedAtoms(draggedAtom, collidedAtom);
        }
    }

    void SetLastBondedAtoms(GameObject draggedAtom, GameObject collidedAtom) {
        if (draggedAtom.name == "Carbon")
        {
            if (atomPropertiesScript.carbonSuccessBonds == 4)
            {
                atomPropertiesScript.lastBondedHydrogenWithCarbon = collidedAtom;
            }
            else {
                ResetLastBondedAtoms(draggedAtom, collidedAtom);
            }
        }
        else if (draggedAtom.name == "Oxygen") {
            if (atomPropertiesScript.oxygenSuccessBonds == 6)
            {
                atomPropertiesScript.lastBondedHydrogenWithOxygen = collidedAtom;
            }
            else
            {
                ResetLastBondedAtoms(draggedAtom, collidedAtom);
            }
        }
    }

    void ResetLastBondedAtoms(GameObject draggedAtom, GameObject collidedAtom) {
        if (draggedAtom.name == "Carbon")
        {
            if (atomPropertiesScript.carbonSuccessBonds < 3) {
                atomPropertiesScript.lastBondedHydrogenWithCarbon = null;
            }
        }
        else if (draggedAtom.name == "Oxygen")
        {
            if (atomPropertiesScript.oxygenSuccessBonds < 6) {
                atomPropertiesScript.lastBondedHydrogenWithOxygen = null;
            }
        }
    }

    public GameObject GetAttachedHydrogenAtom() {
        // Returns the hydrogen attached to another hydrogen
        if (draggedAtom.GetComponent<RelativeJoint2D>().isActiveAndEnabled)
        {
            return draggedAtom.GetComponent<RelativeJoint2D>().connectedBody.gameObject;
        }
        else
        {
            foreach (GameObject hydrogenAtom in atomPropertiesScript.hydrogenAtoms)
            {
                GameObject test = hydrogenAtom.GetComponent<RelativeJoint2D>().connectedBody.gameObject;
                if (hydrogenAtom.GetComponent<RelativeJoint2D>().connectedBody.gameObject.name == draggedAtom.name)
                {
                    return hydrogenAtom;
                }
            }
        }
        return null;
    }

    public void IsStageBondComplete()
    {
    }
}
