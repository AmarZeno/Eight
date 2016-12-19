using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SplitAtoms : MonoBehaviour
{

    public GameObject hydrogenTwo;
    public GameObject hydrogenFour;

    public GameObject carbon;
    public Vector2 hydrogenTwoDefaultLinearOffset = new Vector2(-16, -4);
    public Vector2 hydrogenFourDefaultLinearOffset = new Vector2(0, -17);

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
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0 && didBreakAtom == false)
            {
                didBreakAtom = true;
                // raycastResults[0].gameObject.transform.parent.gameObject.transform .position = Input.mousePosition;
                hydrogenTwo.GetComponent<RelativeJoint2D>().enabled = false;
                hydrogenFour.GetComponent<RelativeJoint2D>().enabled = false;
                hydrogenFour.GetComponent<RelativeJoint2D>().connectedBody = carbon.GetComponent<Rigidbody2D>();
                hydrogenTwo.transform.localPosition = new Vector3(hydrogenTwo.transform.localPosition.x, hydrogenTwo.transform.localPosition.y - 50, hydrogenTwo.transform.localPosition.z);
                hydrogenFour.transform.localPosition = new Vector3(hydrogenFour.transform.localPosition.x, hydrogenFour.transform.localPosition.y + 50, hydrogenFour.transform.localPosition.z);
                hydrogenTwo.GetComponent<MouseDrag>().enabled = true;
                hydrogenFour.GetComponent<MouseDrag>().enabled = true;
                hydrogenTwo.GetComponent<LevelThreeBond>().enabled = true;
                hydrogenFour.GetComponent<LevelThreeBond>().enabled = true;
                hydrogenTwo.GetComponent<RelativeJoint2D>().linearOffset = hydrogenTwoDefaultLinearOffset;
                hydrogenFour.GetComponent<RelativeJoint2D>().linearOffset = hydrogenFourDefaultLinearOffset;
                hydrogenTwo.transform.GetChild(2).localEulerAngles = new Vector3(0, 0, 120);
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
                    if (raycastResults[0].gameObject.transform.parent.gameObject.name == "Hydrogen2" || raycastResults[0].gameObject.transform.parent.gameObject.name == "Hydrogen4")
                    {
                        hydrogenTwo.GetComponent<RelativeJoint2D>().enabled = false;
                        hydrogenFour.GetComponent<RelativeJoint2D>().enabled = false;
                        hydrogenFour.GetComponent<RelativeJoint2D>().connectedBody = carbon.GetComponent<Rigidbody2D>();
                        hydrogenTwo.transform.localPosition = new Vector3(hydrogenTwo.transform.localPosition.x, hydrogenTwo.transform.localPosition.y - 50, hydrogenTwo.transform.localPosition.z);
                        hydrogenFour.transform.localPosition = new Vector3(hydrogenFour.transform.localPosition.x, hydrogenFour.transform.localPosition.y + 50, hydrogenFour.transform.localPosition.z);
                        hydrogenTwo.GetComponent<MouseDrag>().enabled = true;
                        hydrogenFour.GetComponent<MouseDrag>().enabled = true;
                        hydrogenTwo.GetComponent<LevelThreeBond>().enabled = true;
                        hydrogenFour.GetComponent<LevelThreeBond>().enabled = true;
                        hydrogenTwo.GetComponent<RelativeJoint2D>().linearOffset = hydrogenTwoDefaultLinearOffset;
                        hydrogenFour.GetComponent<RelativeJoint2D>().linearOffset = hydrogenFourDefaultLinearOffset;
                        hydrogenTwo.transform.GetChild(2).localEulerAngles = new Vector3(0, 0, 120);
                        didBreakAtom = true;
                    }
                }
            }
        }
    }
}
