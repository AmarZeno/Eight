using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class SplitAtomsLevelTwo : MonoBehaviour
{

    public GameObject fluorineOne;
    public GameObject fluorineTwo;
    public GameObject fluorineThree;
    public GameObject fluorineFour;

    public GameObject[] fluorineList;

    public GameObject carbon;
    public Vector2 fluorineOneDefaultLinearOffset;
    public Vector2 fluorineTwoDefaultLinearOffset;
    public Vector2 fluorineThreeDefaultLinearOffset;
    public Vector2 fluorineFourDefaultLinearOffset;

    public Vector3 fluorineOneDefaultShellEulerValues;
    public Vector3 fluorineTwoDefaultShellEulerValues;
    public Vector3 fluorineThreeDefaultShellEulerValues;
    public Vector3 fluorineFourDefaultShellEulerValues;

    // Variables
    public GameObject atomProperties;
    private LevelTwoAtomProperties atomPropertiesScript;

    // Use this for initialization
    void Start()
    {
        atomPropertiesScript = atomProperties.GetComponent<LevelTwoAtomProperties>();
        fluorineTwo.GetComponent<MouseDrag>().enabled = false;
        fluorineFour.GetComponent<MouseDrag>().enabled = false;

        fluorineTwoDefaultLinearOffset = fluorineTwo.GetComponent<RelativeJoint2D>().linearOffset;
        fluorineOneDefaultLinearOffset = fluorineOne.GetComponent<RelativeJoint2D>().linearOffset;
        fluorineThreeDefaultLinearOffset = fluorineThree.GetComponent<RelativeJoint2D>().linearOffset;
        fluorineFourDefaultLinearOffset = fluorineFour.GetComponent<RelativeJoint2D>().linearOffset;

        fluorineOneDefaultShellEulerValues = fluorineOne.transform.GetChild(2).localEulerAngles;
        fluorineTwoDefaultShellEulerValues = fluorineTwo.transform.GetChild(2).localEulerAngles;
        fluorineThreeDefaultShellEulerValues = fluorineThree.transform.GetChild(2).localEulerAngles;
        fluorineFourDefaultShellEulerValues = fluorineFour.transform.GetChild(2).localEulerAngles;
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

                LevelTwoAtomProperties.AtomBondingState state = atomPropertiesScript.flourineAtomListStates[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)];

                // Prevent splitting for unknown states
                if (state == LevelTwoAtomProperties.AtomBondingState.Unknown || state == LevelTwoAtomProperties.AtomBondingState.Successful) {
                    return;
                }

                if (tappedGameObject.name == "Fluorine1" || tappedGameObject.name == "Fluorine2"
                    || tappedGameObject.name == "Fluorine3" ||
                    tappedGameObject.name == "Fluorine4")
                {
                    if (tappedGameObject.GetComponent<RelativeJoint2D>().enabled == true)
                    {
                        // If the tapped gameobject has an enabled Joint then continue splitting that Atom
                        SplitAtom(tappedGameObject);
                    }
                    else
                    {
                        // Check for gameObject that holds joint with the tapped gameobject
                        
                        foreach (GameObject fluorine in fluorineList)
                        {
                            if (fluorine.GetComponent<RelativeJoint2D>().connectedBody == tappedGameObject.GetComponent<Rigidbody2D>())
                            {
                                SplitAtom(fluorine);
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
                    LevelTwoAtomProperties.AtomBondingState state = atomPropertiesScript.flourineAtomListStates[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)];

                    if (state == LevelTwoAtomProperties.AtomBondingState.Unknown || state == LevelTwoAtomProperties.AtomBondingState.Successful)
                    {
                        return;
                    }
                    if (tappedGameObject.name == "Fluorine1" || tappedGameObject.name == "Fluorine2"
                        || tappedGameObject.name == "Fluorine3" ||
                        tappedGameObject.name == "Fluorine4")
                    {

                        if (tappedGameObject.GetComponent<RelativeJoint2D>().enabled == true)
                        {
                            // If the tapped gameobject has an enabled Joint then continue splitting that Atom
                            SplitAtom(tappedGameObject);
                        }
                        else {
                            // Check for gameObject that holds joint with the tapped gameobject

                            foreach (GameObject fluorine in fluorineList) {
                                if (fluorine.GetComponent<RelativeJoint2D>().connectedBody == tappedGameObject.GetComponent<Rigidbody2D>()) {
                                    SplitAtom(fluorine);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void SplitAtom(GameObject tappedGameObject) {
        GameObject connectedGameObject = tappedGameObject.GetComponent<RelativeJoint2D>().connectedBody.gameObject;
        tappedGameObject.GetComponent<RelativeJoint2D>().enabled = false;

        if (tappedGameObject.transform.localPosition.y > connectedGameObject.transform.localPosition.y)
        { // If tappedObject is on top
            tappedGameObject.transform.localPosition = new Vector3(tappedGameObject.transform.localPosition.x, tappedGameObject.transform.localPosition.y + 50, tappedGameObject.transform.localPosition.z);
            connectedGameObject.transform.localPosition = new Vector3(connectedGameObject.transform.localPosition.x, connectedGameObject.transform.localPosition.y - 50, connectedGameObject.transform.localPosition.z);
        }
        else {
            tappedGameObject.transform.localPosition = new Vector3(tappedGameObject.transform.localPosition.x, tappedGameObject.transform.localPosition.y - 50, tappedGameObject.transform.localPosition.z);
            connectedGameObject.transform.localPosition = new Vector3(connectedGameObject.transform.localPosition.x, connectedGameObject.transform.localPosition.y + 50, connectedGameObject.transform.localPosition.z);
        }
        
        
        AssociateDefaultPositions(tappedGameObject);
        AssociateDefaultPositions(connectedGameObject);

        tappedGameObject.GetComponent<MouseDrag>().enabled = true;
        tappedGameObject.GetComponent<LevelTwoBond>().enabled = true;
        connectedGameObject.GetComponent<MouseDrag>().enabled = true;
        connectedGameObject.GetComponent<LevelTwoBond>().enabled = true;
        tappedGameObject.GetComponent<RelativeJoint2D>().connectedBody = carbon.GetComponent<Rigidbody2D>();
        connectedGameObject.GetComponent<RelativeJoint2D>().connectedBody = carbon.GetComponent<Rigidbody2D>();
    }

    void AssociateDefaultPositions(GameObject gameObject) {
        if (gameObject.name == "Fluorine1")
        {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = fluorineOneDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = fluorineOneDefaultShellEulerValues;
            atomPropertiesScript.flourineAtomListStates[0] = LevelTwoAtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Fluorine2")
        {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = fluorineTwoDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = fluorineTwoDefaultShellEulerValues;
            atomPropertiesScript.flourineAtomListStates[1] = LevelTwoAtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Fluorine3")
        {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = fluorineThreeDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = fluorineThreeDefaultShellEulerValues;
            atomPropertiesScript.flourineAtomListStates[2] = LevelTwoAtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Fluorine4") {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = fluorineFourDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = fluorineFourDefaultShellEulerValues;
            atomPropertiesScript.flourineAtomListStates[3] = LevelTwoAtomProperties.AtomBondingState.Unknown;
        }
    }
}
