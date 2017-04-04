using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class CCL4SplitAtoms : MonoBehaviour
{

    public GameObject hydrogenOne;
    public GameObject hydrogenTwo;
    public GameObject hydrogenThree;
    public GameObject hydrogenFour;

    public GameObject[] hydrogenList;

    public GameObject carbon;
    public Vector2 hydrogenOneDefaultLinearOffset;
    public Vector2 hydrogenTwoDefaultLinearOffset;
    public Vector2 hydrogenThreeDefaultLinearOffset;
    public Vector2 hydrogenFourDefaultLinearOffset;

    public Vector3 hydrogenOneDefaultShellEulerValues;
    public Vector3 hydrogenTwoDefaultShellEulerValues;
    public Vector3 hydrogenThreeDefaultShellEulerValues;
    public Vector3 hydrogenFourDefaultShellEulerValues;

    // Variables
    public GameObject atomProperties;
    private CCL4AtomProperties atomPropertiesScript;

    // Use this for initialization
    void Start()
    {
        atomPropertiesScript = atomProperties.GetComponent<CCL4AtomProperties>();
        hydrogenTwo.GetComponent<MouseDrag>().enabled = false;
        hydrogenFour.GetComponent<MouseDrag>().enabled = false;

        hydrogenTwoDefaultLinearOffset = new Vector2(-16, -4);
        hydrogenOneDefaultLinearOffset = hydrogenOne.GetComponent<RelativeJoint2D>().linearOffset;
        hydrogenThreeDefaultLinearOffset = hydrogenThree.GetComponent<RelativeJoint2D>().linearOffset;
        hydrogenFourDefaultLinearOffset = new Vector2(0, -17);

        hydrogenOneDefaultShellEulerValues = new Vector3(0, 0, 240);
        hydrogenTwoDefaultShellEulerValues = new Vector3(0, 0, 120);
        hydrogenThreeDefaultShellEulerValues = new Vector3(0, 0, 0);
        hydrogenFourDefaultShellEulerValues = new Vector3(0, 0, 180);
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        /* For testing touch logics in unity*/
        if (Input.GetMouseButton(1) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonUp(1))
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0)
            {
                GameObject tappedGameObject = raycastResults[0].gameObject.transform.parent.gameObject;

                CCL4AtomProperties.AtomBondingState state = atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)];

                // Prevent splitting for unknown states
                if (state == CCL4AtomProperties.AtomBondingState.Unknown) {
                    return;
                }

                if (tappedGameObject.name == "Hydrogen1" || tappedGameObject.name == "Hydrogen2"
                    || tappedGameObject.name == "Hydrogen3" ||
                    tappedGameObject.name == "Hydrogen4")
                {
                    if (tappedGameObject.GetComponent<RelativeJoint2D>().enabled == true)
                    {
                        // If the tapped gameobject has an enabled Joint then continue splitting that Atom
                        if (state == CCL4AtomProperties.AtomBondingState.Successful) // Split success bonds
                        {
                            SplitSuccessBond(tappedGameObject);
                        }
                        else // Split wrong bonds
                        {
                            SplitAtom(tappedGameObject);
                        }
                    }
                    else
                    {
                        // Check for gameObject that holds joint with the tapped gameobject
                        
                        foreach (GameObject hydrogen in hydrogenList)
                        {
                            if (hydrogen.GetComponent<RelativeJoint2D>().connectedBody == tappedGameObject.GetComponent<Rigidbody2D>())
                            {
                                SplitAtom(hydrogen);
                            }
                        }
                    }
                }
            }
        }
#endif

        foreach (Touch touch in Input.touches)
        {
            if (touch.tapCount == 2)
            {
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                pointer.position = touch.position;

                List<RaycastResult> raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointer, raycastResults);

                if (raycastResults.Count > 0)
                {
                    GameObject tappedGameObject = raycastResults[0].gameObject.transform.parent.gameObject;
                    // Prevent splitting for unknown states
                    CCL4AtomProperties.AtomBondingState state = atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)];

                    if (state == CCL4AtomProperties.AtomBondingState.Unknown)
                    {
                        return;
                    }
                    if (tappedGameObject.name == "Hydrogen1" || tappedGameObject.name == "Hydrogen2"
                        || tappedGameObject.name == "Hydrogen3" ||
                        tappedGameObject.name == "Hydrogen4")
                    {

                        if (tappedGameObject.GetComponent<RelativeJoint2D>().enabled == true)
                        {
                            // If the tapped gameobject has an enabled Joint then continue splitting that Atom
                            if (state == CCL4AtomProperties.AtomBondingState.Successful) // Split success bonds
                            {
                                SplitSuccessBond(tappedGameObject);
                            }
                            else // Split wrong bonds
                            {
                                SplitAtom(tappedGameObject);
                            }
                        }
                        else {
                            // Check for gameObject that holds joint with the tapped gameobject

                            foreach (GameObject hydrogen in hydrogenList) {
                                if (hydrogen.GetComponent<RelativeJoint2D>().connectedBody == tappedGameObject.GetComponent<Rigidbody2D>()) {
                                    SplitAtom(hydrogen);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    void SplitSuccessBond(GameObject tappedGameObject)
    {
        GameObject connectedGameObject = tappedGameObject.GetComponent<RelativeJoint2D>().connectedBody.gameObject;
        tappedGameObject.GetComponent<RelativeJoint2D>().enabled = false;

        if (connectedGameObject.transform.localPosition.x - tappedGameObject.transform.localPosition.x < 10 && connectedGameObject.transform.localPosition.x - tappedGameObject.transform.localPosition.x > -10) // Lies top or bottom
        {
            if (tappedGameObject.transform.localPosition.y > connectedGameObject.transform.localPosition.y)
            { // TOP
                tappedGameObject.transform.localPosition = new Vector3(tappedGameObject.transform.localPosition.x, tappedGameObject.transform.localPosition.y + 70, tappedGameObject.transform.localPosition.z);
            }
            else
            { // BOTTOM
                tappedGameObject.transform.localPosition = new Vector3(tappedGameObject.transform.localPosition.x, tappedGameObject.transform.localPosition.y - 70, tappedGameObject.transform.localPosition.z);
            }
        }
        else
        { // Lies left or right
            if (tappedGameObject.transform.localPosition.x < connectedGameObject.transform.localPosition.x) // LEFT
            {
                tappedGameObject.transform.localPosition = new Vector3(tappedGameObject.transform.localPosition.x - 70, tappedGameObject.transform.localPosition.y, tappedGameObject.transform.localPosition.z);
            }
            else
            { // RIGHT
                tappedGameObject.transform.localPosition = new Vector3(tappedGameObject.transform.localPosition.x + 70, tappedGameObject.transform.localPosition.y, tappedGameObject.transform.localPosition.z);
            }
        }
       
        tappedGameObject.GetComponent<MouseDrag>().enabled = true;
        tappedGameObject.GetComponent<CCL4Bond>().enabled = true;
        AssociateDefaultPositions(tappedGameObject);
        atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)] = CCL4AtomProperties.AtomBondingState.Unknown;
    }


    void SplitAtom(GameObject tappedGameObject) {
        GameObject connectedGameObject = tappedGameObject.GetComponent<RelativeJoint2D>().connectedBody.gameObject;
        tappedGameObject.GetComponent<RelativeJoint2D>().enabled = false;
        if (tappedGameObject.transform.localPosition.y > connectedGameObject.transform.localPosition.y)
        { // If tappedObject is on top
            tappedGameObject.transform.localPosition = new Vector3(tappedGameObject.transform.localPosition.x, tappedGameObject.transform.localPosition.y + 50, tappedGameObject.transform.localPosition.z);
            connectedGameObject.transform.localPosition = new Vector3(connectedGameObject.transform.localPosition.x, connectedGameObject.transform.localPosition.y - 50, connectedGameObject.transform.localPosition.z);
        }
        else
        {
            tappedGameObject.transform.localPosition = new Vector3(tappedGameObject.transform.localPosition.x, tappedGameObject.transform.localPosition.y - 50, tappedGameObject.transform.localPosition.z);
            connectedGameObject.transform.localPosition = new Vector3(connectedGameObject.transform.localPosition.x, connectedGameObject.transform.localPosition.y + 50, connectedGameObject.transform.localPosition.z);
        }
        tappedGameObject.GetComponent<MouseDrag>().enabled = true;
        tappedGameObject.GetComponent<CCL4Bond>().enabled = true;
        connectedGameObject.GetComponent<MouseDrag>().enabled = true;
        connectedGameObject.GetComponent<CCL4Bond>().enabled = true;
        tappedGameObject.GetComponent<RelativeJoint2D>().connectedBody = carbon.GetComponent<Rigidbody2D>();
        AssociateDefaultPositions(tappedGameObject);
        AssociateDefaultPositions(connectedGameObject);
        atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)] = CCL4AtomProperties.AtomBondingState.Unknown;
    }

    void AssociateDefaultPositions(GameObject gameObject) {
        if (gameObject.name == "Hydrogen1")
        {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = hydrogenOneDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = hydrogenOneDefaultShellEulerValues;
            atomPropertiesScript.hydrogenAtomStateList[0] = CCL4AtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Hydrogen2")
        {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = hydrogenTwoDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = hydrogenTwoDefaultShellEulerValues;
            atomPropertiesScript.hydrogenAtomStateList[1] = CCL4AtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Hydrogen3")
        {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = hydrogenThreeDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = hydrogenThreeDefaultShellEulerValues;
            atomPropertiesScript.hydrogenAtomStateList[2] = CCL4AtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Hydrogen4") {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = hydrogenFourDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = hydrogenFourDefaultShellEulerValues;
            atomPropertiesScript.hydrogenAtomStateList[3] = CCL4AtomProperties.AtomBondingState.Unknown;
        }
    }
}
