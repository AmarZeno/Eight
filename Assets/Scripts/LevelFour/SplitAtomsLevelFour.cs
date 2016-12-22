using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class SplitAtomsLevelFour : MonoBehaviour
{

    public GameObject hydrogenOne;
    public GameObject hydrogenTwo;
    public GameObject hydrogenThree;
    public GameObject hydrogenFour;

    public GameObject[] hydrogenList;

    public GameObject carbon;


    // Variables
    public GameObject atomProperties;
    private LevelFourAtomProperties atomPropertiesScript;

    // Use this for initialization
    void Start()
    {
        atomPropertiesScript = atomProperties.GetComponent<LevelFourAtomProperties>();

        hydrogenTwo.GetComponent<MouseDrag>().enabled = false;
        hydrogenFour.GetComponent<MouseDrag>().enabled = false;

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

                LevelFourAtomProperties.AtomBondingState state = atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)];

                // Prevent splitting for unknown states
                if (state == LevelFourAtomProperties.AtomBondingState.Unknown || state == LevelFourAtomProperties.AtomBondingState.Successful) {
                    return;
                }

                if (tappedGameObject.name == "Hydrogen1" || tappedGameObject.name == "Hydrogen2"
                    || tappedGameObject.name == "Hydrogen3" ||
                    tappedGameObject.name == "Hydrogen4" ||
                    tappedGameObject.name == "Hydrogen5" ||
                    tappedGameObject.name == "Hydrogen6")
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
                    // Prevent splitting for unknown states
                    LevelFourAtomProperties.AtomBondingState state = atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)];

                    if (state == LevelFourAtomProperties.AtomBondingState.Unknown || state == LevelFourAtomProperties.AtomBondingState.Successful)
                    {
                        return;
                    }
                    if (tappedGameObject.name == "Hydrogen1" || tappedGameObject.name == "Hydrogen2"
                        || tappedGameObject.name == "Hydrogen3" ||
                        tappedGameObject.name == "Hydrogen4" ||
                    tappedGameObject.name == "Hydrogen5" ||
                    tappedGameObject.name == "Hydrogen6")
                    {

                        if (tappedGameObject.GetComponent<RelativeJoint2D>().enabled == true)
                        {
                            // If the tapped gameobject has an enabled Joint then continue splitting that Atom
                            SplitAtom(tappedGameObject);
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

        ApplyDefaultStates(tappedGameObject);
        ApplyDefaultStates(connectedGameObject);

        tappedGameObject.GetComponent<MouseDrag>().enabled = true;
        tappedGameObject.GetComponent<LevelFourBond>().enabled = true;
        connectedGameObject.GetComponent<MouseDrag>().enabled = true;
        connectedGameObject.GetComponent<LevelFourBond>().enabled = true;
        tappedGameObject.GetComponent<RelativeJoint2D>().connectedBody = carbon.GetComponent<Rigidbody2D>();
        connectedGameObject.GetComponent<RelativeJoint2D>().connectedBody = carbon.GetComponent<Rigidbody2D>();
    }

    void ApplyDefaultStates(GameObject gameObject) {
        if (gameObject.name == "Hydrogen1")
        {

            atomPropertiesScript.hydrogenAtomStateList[0] = LevelFourAtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Hydrogen2")
        {

            atomPropertiesScript.hydrogenAtomStateList[1] = LevelFourAtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Hydrogen3")
        {

            atomPropertiesScript.hydrogenAtomStateList[2] = LevelFourAtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Hydrogen4") {
            atomPropertiesScript.hydrogenAtomStateList[3] = LevelFourAtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Hydrogen5")
        {
            atomPropertiesScript.hydrogenAtomStateList[4] = LevelFourAtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Hydrogen6")
        {
            atomPropertiesScript.hydrogenAtomStateList[5] = LevelFourAtomProperties.AtomBondingState.Unknown;
        }
    }
}
