using UnityEngine;
using System.Collections;

public class SplitAtoms : MonoBehaviour {

	public GameObject hydrogenTwo;
	public GameObject hydrogenFour;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 2) {
			RaycastHit2D firstHitInformation = Physics2D.Raycast(hydrogenTwo.transform.position, Camera.main.transform.forward);
			RaycastHit2D secondHitInformation = Physics2D.Raycast(hydrogenFour.transform.position, Camera.main.transform.forward);
			if (firstHitInformation.collider != null && secondHitInformation.collider != null) {
				hydrogenFour.GetComponent<RelativeJoint2D> ().enabled = false;

				hydrogenTwo.GetComponent<RelativeJoint2D> ().enabled = false;

				hydrogenTwo.transform.position = Input.GetTouch (0).position;
				hydrogenFour.transform.position = Input.GetTouch (1).position;
			}
		}
	}
}
