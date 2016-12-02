using UnityEngine;
using System.Collections;

public class SplitAtoms : MonoBehaviour {

	public GameObject hydrogenTwo;
	public GameObject hydrogenFour;

	public LayerMask touchInputMask;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 2) {

			foreach (Touch touch in Input.touches) {
				Ray ray1 = Camera.main.ScreenPointToRay(touch);
				RaycastHit hit;
				if (Physics.Raycast (ray1, out hit, touchInputMask)) {
					GameObject recipient =  hit.transform.gameObject;
					if(recipient.transform.name == "Hydrogen2" || recipient.transform.name == "Hydrogen1"){
						
						hydrogenFour.GetComponent<RelativeJoint2D> ().enabled = false;

						hydrogenTwo.GetComponent<RelativeJoint2D> ().enabled = false;

					}
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
