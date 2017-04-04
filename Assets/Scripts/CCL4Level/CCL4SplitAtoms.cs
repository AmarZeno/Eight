using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class CCL4SplitAtoms : MonoBehaviour
{

    public GameObject chlorineOne;
    public GameObject chlorineTwo;
    public GameObject chlorineThree;
    public GameObject chlorineFour;

    public GameObject[] chlorineList;

    public GameObject carbon;
    public Vector2 chlorineOneDefaultLinearOffset;
    public Vector2 chlorineTwoDefaultLinearOffset;
    public Vector2 chlorineThreeDefaultLinearOffset;
    public Vector2 chlorineFourDefaultLinearOffset;
               
    public Vector3 chlorineOneDefaultShellEulerValues;
    public Vector3 chlorineTwoDefaultShellEulerValues;
    public Vector3 chlorineThreeDefaultShellEulerValues;
    public Vector3 chlorineFourDefaultShellEulerValues;

    // Variables
    public GameObject atomProperties;
    private CCL4AtomProperties atomPropertiesScript;

    // Use this for initialization
    void Start()
    {
        atomPropertiesScript = atomProperties.GetComponent<CCL4AtomProperties>();
        chlorineTwo.GetComponent<MouseDrag>().enabled = false;
        chlorineFour.GetComponent<MouseDrag>().enabled = false;

        chlorineTwoDefaultLinearOffset = chlorineTwo.GetComponent<RelativeJoint2D>().linearOffset;
        chlorineOneDefaultLinearOffset = chlorineOne.GetComponent<RelativeJoint2D>().linearOffset;
        chlorineThreeDefaultLinearOffset = chlorineThree.GetComponent<RelativeJoint2D>().linearOffset;
        chlorineFourDefaultLinearOffset = chlorineFour.GetComponent<RelativeJoint2D>().linearOffset;
       
        chlorineOneDefaultShellEulerValues = new Vector3(0, 0, -16);
        chlorineTwoDefaultShellEulerValues = new Vector3(0, 0, 182);
        chlorineThreeDefaultShellEulerValues = new Vector3(0, 0, 86);
        chlorineFourDefaultShellEulerValues = new Vector3(0, 0, 262);
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

                CCL4AtomProperties.AtomBondingState state = atomPropertiesScript.chlorineAtomStateList[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)];

                // Prevent splitting for unknown states
                if (state == CCL4AtomProperties.AtomBondingState.Unknown) {
                    return;
                }

                if (tappedGameObject.name == "Chlorine1" || tappedGameObject.name == "Chlorine2"
                    || tappedGameObject.name == "Chlorine3" ||
                    tappedGameObject.name == "Chlorine4")
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
                        
                        foreach (GameObject chlorine in chlorineList)
                        {
                            if (chlorine.GetComponent<RelativeJoint2D>().connectedBody == tappedGameObject.GetComponent<Rigidbody2D>())
                            {
                                SplitAtom(chlorine);
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
                    CCL4AtomProperties.AtomBondingState state = atomPropertiesScript.chlorineAtomStateList[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)];

                    if (state == CCL4AtomProperties.AtomBondingState.Unknown)
                    {
                        return;
                    }
                    if (tappedGameObject.name == "Chlorine1" || tappedGameObject.name == "Chlorine2"
                        || tappedGameObject.name == "Chlorine3" ||
                        tappedGameObject.name == "Chlorine4")
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

                            foreach (GameObject chlorine in chlorineList) {
                                if (chlorine.GetComponent<RelativeJoint2D>().connectedBody == tappedGameObject.GetComponent<Rigidbody2D>()) {
                                    SplitAtom(chlorine);
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
        atomPropertiesScript.chlorineAtomStateList[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)] = CCL4AtomProperties.AtomBondingState.Unknown;
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
        atomPropertiesScript.chlorineAtomStateList[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)] = CCL4AtomProperties.AtomBondingState.Unknown;
    }

    void AssociateDefaultPositions(GameObject gameObject) {
        if (gameObject.name == "Chlorine1")
        {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = chlorineOneDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = chlorineOneDefaultShellEulerValues;
            atomPropertiesScript.chlorineAtomStateList[0] = CCL4AtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Chlorine2")
        {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = chlorineTwoDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = chlorineTwoDefaultShellEulerValues;
            atomPropertiesScript.chlorineAtomStateList[1] = CCL4AtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Chlorine3")
        {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = chlorineThreeDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = chlorineThreeDefaultShellEulerValues;
            atomPropertiesScript.chlorineAtomStateList[2] = CCL4AtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Chlorine4") {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = chlorineFourDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = chlorineFourDefaultShellEulerValues;
            atomPropertiesScript.chlorineAtomStateList[3] = CCL4AtomProperties.AtomBondingState.Unknown;
        }
    }
}
