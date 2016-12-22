using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SplitAtomsLevelOne : MonoBehaviour {

    public GameObject atomProperties;
    private LevelOneAtomProperties atomPropertiesScript;

    public GameObject[] hydrogenList;
    public GameObject oxygen;

    public Vector2 hydrogenOneDefaultLinearOffset;
    public Vector3 hydrogenOneDefaultShellEulerValues;

    public Vector2 hydrogenTwoDefaultLinearOffset;
    public Vector3 hydrogenTwoDefaultShellEulerValues;

    // Use this for initialization
    void Start () {
        atomPropertiesScript = atomProperties.GetComponent<LevelOneAtomProperties>();
        hydrogenOneDefaultLinearOffset = new Vector2(-14, -8);
        hydrogenOneDefaultShellEulerValues = new Vector3(0, 0, 120);
        hydrogenTwoDefaultLinearOffset = new Vector2(14, -8);
        hydrogenTwoDefaultShellEulerValues = new Vector3(0, 0, -120);
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

                LevelOneAtomProperties.AtomBondingState state = atomPropertiesScript.hydrogenAtomListStates[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)];

                // Prevent splitting for unknown states
                if (state == LevelOneAtomProperties.AtomBondingState.Unknown || state == LevelOneAtomProperties.AtomBondingState.Successful)
                {
                    return;
                }

                if (tappedGameObject.name == "Hydrogen1" || tappedGameObject.name == "Hydrogen2")
                {
                    if (tappedGameObject.GetComponent<RelativeJoint2D>().enabled == true)
                    {
                        // If the tapped gameobject has an enabled Joint then continue splitting that Atom
                        SplitAtom(tappedGameObject);
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

                    LevelOneAtomProperties.AtomBondingState state = atomPropertiesScript.hydrogenAtomListStates[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)];

                    // Prevent splitting for unknown states
                    if (state == LevelOneAtomProperties.AtomBondingState.Unknown || state == LevelOneAtomProperties.AtomBondingState.Successful)
                    {
                        return;
                    }

                    if (tappedGameObject.name == "Hydrogen1" || tappedGameObject.name == "Hydrogen2")
                    {
                        if (tappedGameObject.GetComponent<RelativeJoint2D>().enabled == true)
                        {
                            // If the tapped gameobject has an enabled Joint then continue splitting that Atom
                            SplitAtom(tappedGameObject);
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
        }
    }

    void SplitAtom(GameObject tappedGameObject)
    {
        GameObject connectedGameObject = tappedGameObject.GetComponent<RelativeJoint2D>().connectedBody.gameObject;
        tappedGameObject.GetComponent<RelativeJoint2D>().enabled = false;
        tappedGameObject.transform.localPosition = new Vector3(tappedGameObject.transform.localPosition.x, tappedGameObject.transform.localPosition.y + 50, tappedGameObject.transform.localPosition.z);
        connectedGameObject.transform.localPosition = new Vector3(connectedGameObject.transform.localPosition.x, connectedGameObject.transform.localPosition.y - 50, connectedGameObject.transform.localPosition.z);
        tappedGameObject.GetComponent<MouseDrag>().enabled = true;
        tappedGameObject.GetComponent<LevelOneBond>().enabled = true;
        connectedGameObject.GetComponent<MouseDrag>().enabled = true;
        connectedGameObject.GetComponent<LevelOneBond>().enabled = true;
        tappedGameObject.GetComponent<RelativeJoint2D>().connectedBody = oxygen.GetComponent<Rigidbody2D>();
        AssociateDefaultPositions(tappedGameObject);
        AssociateDefaultPositions(connectedGameObject);
    }

    void AssociateDefaultPositions(GameObject gameObject)
    {
        if (gameObject.name == "Hydrogen1")
        {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = hydrogenOneDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = hydrogenOneDefaultShellEulerValues;
            atomPropertiesScript.hydrogenAtomListStates[0] = LevelOneAtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Hydrogen2")
        {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = hydrogenTwoDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = hydrogenTwoDefaultShellEulerValues;
            atomPropertiesScript.hydrogenAtomListStates[1] = LevelOneAtomProperties.AtomBondingState.Unknown;
        }
    }
}
