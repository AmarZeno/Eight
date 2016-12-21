using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class SplitAtomsLevelThree : MonoBehaviour
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

    private List<GameObject> gameObjectToBeSplitted = new List<GameObject>();

    private bool didBreakAtom = false;

    // Variables
    public GameObject atomProperties;
    private LevelThreeAtomProperties atomPropertiesScript;

    // Use this for initialization
    void Start()
    {
        atomPropertiesScript = atomProperties.GetComponent<LevelThreeAtomProperties>();
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
        if (Input.GetMouseButton(1) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonUp(1))
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0)
            {
                GameObject tappedGameObject = raycastResults[0].gameObject.transform.parent.gameObject;

                LevelThreeAtomProperties.AtomBondingState state = atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)];

                // Prevent splitting for unknown states
                if (state == LevelThreeAtomProperties.AtomBondingState.Unknown) {
                    return;
                }

                if (tappedGameObject.name == "Hydrogen1" || tappedGameObject.name == "Hydrogen2"
                    || tappedGameObject.name == "Hydrogen3" ||
                    tappedGameObject.name == "Hydrogen4")
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
            if (touch.tapCount == 2 && didBreakAtom == false)
            {

                PointerEventData pointer = new PointerEventData(EventSystem.current);
                pointer.position = touch.position;

                List<RaycastResult> raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointer, raycastResults);

                if (raycastResults.Count > 0)
                {
                    GameObject tappedGameObject = raycastResults[0].gameObject.transform.parent.gameObject;
                    // Prevent splitting for unknown states
                    LevelThreeAtomProperties.AtomBondingState state = atomPropertiesScript.hydrogenAtomStateList[(Convert.ToInt32(tappedGameObject.name[tappedGameObject.name.Length - 1].ToString()) - 1)];

                    if (state == LevelThreeAtomProperties.AtomBondingState.Unknown)
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
        tappedGameObject.transform.localPosition = new Vector3(tappedGameObject.transform.localPosition.x, tappedGameObject.transform.localPosition.y + 50, tappedGameObject.transform.localPosition.z);
        connectedGameObject.transform.localPosition = new Vector3(connectedGameObject.transform.localPosition.x, connectedGameObject.transform.localPosition.y - 50, connectedGameObject.transform.localPosition.z);
        tappedGameObject.GetComponent<MouseDrag>().enabled = true;
        tappedGameObject.GetComponent<LevelThreeBond>().enabled = true;
        connectedGameObject.GetComponent<MouseDrag>().enabled = true;
        connectedGameObject.GetComponent<LevelThreeBond>().enabled = true;
        tappedGameObject.GetComponent<RelativeJoint2D>().connectedBody = carbon.GetComponent<Rigidbody2D>();
        AssociateDefaultPositions(tappedGameObject);
        AssociateDefaultPositions(connectedGameObject);
        didBreakAtom = true;
    }

    void AssociateDefaultPositions(GameObject gameObject) {
        if (gameObject.name == "Hydrogen1")
        {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = hydrogenOneDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = hydrogenOneDefaultShellEulerValues;
            atomPropertiesScript.hydrogenAtomStateList[0] = LevelThreeAtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Hydrogen2")
        {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = hydrogenTwoDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = hydrogenTwoDefaultShellEulerValues;
            atomPropertiesScript.hydrogenAtomStateList[1] = LevelThreeAtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Hydrogen3")
        {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = hydrogenThreeDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = hydrogenThreeDefaultShellEulerValues;
            atomPropertiesScript.hydrogenAtomStateList[2] = LevelThreeAtomProperties.AtomBondingState.Unknown;
        }
        else if (gameObject.name == "Hydrogen4") {
            gameObject.GetComponent<RelativeJoint2D>().linearOffset = hydrogenFourDefaultLinearOffset;
            gameObject.transform.GetChild(2).localEulerAngles = hydrogenFourDefaultShellEulerValues;
            atomPropertiesScript.hydrogenAtomStateList[3] = LevelThreeAtomProperties.AtomBondingState.Unknown;
        }
    }
}
