using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplitAtoms : MonoBehaviour {

	public GameObject hydrogenTwo;
	public GameObject hydrogenFour;

	public LayerMask touchInputMask;

    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;

    private RaycastHit hit;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {

            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();


            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray2, out hit, touchInputMask))
            {
                GameObject recipient2 = hit.transform.gameObject;
                touchList.Add(recipient2);


                if (Input.GetMouseButtonDown(0))
                {
                    // recipient.GetComponent<RelativeJoint2D>().enabled = false;
                    recipient2.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    recipient2.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                }

                if (Input.GetMouseButton(0))
                {
                    recipient2.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                }

                //if(recipient.transform.name == "Hydrogen2" || recipient.transform.name == "Hydrogen1"){

                //	hydrogenFour.GetComponent<RelativeJoint2D> ().enabled = false;

                //	hydrogenTwo.GetComponent<RelativeJoint2D> ().enabled = false;

                //}

                foreach (GameObject g in touchesOld)
                {
                    if (!touchList.Contains(g))
                    {
                        g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
#endif

            if (Input.touchCount == 2) {

            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();

			foreach (Touch touch in Input.touches) {
				Ray ray1 = Camera.main.ScreenPointToRay(touch.position);
				
				if (Physics.Raycast (ray1, out hit, touchInputMask)) {
					GameObject recipient =  hit.transform.gameObject;
                    touchList.Add(recipient);


                    if (touch.phase == TouchPhase.Began) {
                         recipient.GetComponent<RelativeJoint2D>().enabled = false;
                        recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (touch.phase == TouchPhase.Ended) {
                        recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
                        Vector3 objPosition = Camera.main.ScreenToWorldPoint(touch.position);
                        recipient.gameObject.transform.position = objPosition;
                        recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (touch.phase == TouchPhase.Canceled) {
                        recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

					//if(recipient.transform.name == "Hydrogen2" || recipient.transform.name == "Hydrogen1"){
						
					//	hydrogenFour.GetComponent<RelativeJoint2D> ().enabled = false;

					//	hydrogenTwo.GetComponent<RelativeJoint2D> ().enabled = false;

					//}
				}
			}

            foreach (GameObject g in touchesOld) {
                if (!touchList.Contains(g)) {
                    g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }

			//TODO Fix split atoms
			/*RaycastHit2D firstHitInformation = Physics2D.Raycast(hydrogenTwo.transform.position, Camera.main.transform.forward);
			RaycastHit2D secondHitInformation = Physics2D.Raycast(hydrogenFour.transform.position, Camera.main.transform.forward);
			if (firstHitInformation.collider.name == "Hydrogen1" && secondHitInformation.collider.name == "Hydrogen4") {
				
				//TODO update atom properties

				//Vector3 realWorldPos0 = Camera.main.ScreenToWorldPoint(Input.GetTouch (0).position);
				//Vector3 realWorldPos1  = Camera.main.ScreenToWorldPoint(Input.GetTouch (1).position);


				Touch[] myTouches = Input.touches;

					//Do something with the touches
					hydrogenTwo.transform.localPosition = myTouches [0].position;
					hydrogenFour.transform.localPosition = myTouches [1].position;
				//}


			}*/
		}
	}
}
