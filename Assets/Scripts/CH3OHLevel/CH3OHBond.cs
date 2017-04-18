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
    private GameObject draggedAtom = null;

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
                }
                break;
            case "Hydrogen2":
                break;
            case "Hydrogen3":
                break;
            case "Hydrogen4":
                break;
            case "Oxygen":
                break;
            case "Carbon":
                if (draggedAtom.name == "Oxygen")
                {
                    // Enable carbon joint
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                    atomPropertiesScript.carbonOxygenSuccessBonds += 1;
                }
                else
                {
                    // Break if first carbon has completed its bonds
                    if (atomPropertiesScript.carbonSuccessBonds == 3)
                    {
                        break;
                    }
                    draggedAtom.GetComponent<LevelFourBond>().enabled = false;
                    // Inverse the params
                    MakeSuccessfulBond(collidedAtom, draggedAtom);
                }
                break;
            default:
                break;
        }
        draggedAtom = null;
        IsStageBondComplete();
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
        }
        else if (draggedAtom.name == "Oxygen")
        {
            collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = oxygenAtom.GetComponent<Rigidbody2D>();
            collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
            atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(collidedAtom.name[collidedAtom.name.Length - 1].ToString()) - 1)] = CH3OHAtomProperties.AtomBondingState.Successful;
            collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = atomPropertiesScript.hydrogenLinearOffsets[atomPropertiesScript.oxygenSuccessBonds];
            collidedAtom.transform.GetChild(2).localEulerAngles = atomPropertiesScript.hydrogenShellRotations[atomPropertiesScript.oxygenSuccessBonds];
            atomPropertiesScript.oxygenSuccessBonds = atomPropertiesScript.oxygenSuccessBonds + 1;
        }
    }

    public void IsStageBondComplete()
    {
    }
}
