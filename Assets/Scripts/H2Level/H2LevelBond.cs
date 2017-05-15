using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class H2LevelBond : MouseDrag {

    // Constants
    readonly Vector2 hydrogenAtomLinearOffset = new Vector2(13.8f, 0);

	// Variables
	public GameObject atomProperties;
    private GameObject draggedAtom;
	private H2LevelAtomProperties atomPropertiesScript;

    // Particle effects

    public ParticleSystem hydrogenOneSuccessParticleEffect;
    public ParticleSystem hydrogenTwoSuccessParticleEffect;
    public ParticleSystem hydrogenOneWrongParticleEffect;
    public ParticleSystem hydrogenTwoWrongParticleEffect;
    public GameObject hydrogenOneFinisherParticleSystem;
    public GameObject hydrogenTwoFinisherParticleSystem;
    public GameObject mainCanvas;

    void Start(){
        atomPropertiesScript = atomProperties.GetComponent<H2LevelAtomProperties>();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        draggedAtom = eventData.pointerDrag.gameObject;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Bond(other);
    }

    void Bond(Collider2D otherAtomCollider) {

        // Prevent bonding when the user is not dragging an atom and when he makes a wrong bonding
        if (draggedAtom == null)
            return;

        GameObject collidedAtom = otherAtomCollider.GetComponent<Collider2D>().gameObject;

        // Handle three collision cases
        // 1) When the dragged atom collides with hydrogen1 atom
        // 2) When the dragged atom collides with hydrogen2 atom
        // 3) When the dragged atom collides with oxygen atom
        switch (collidedAtom.name) {
            case "Hydrogen1":
                {
					// Break if already made a bond
				if (atomPropertiesScript.hydrogenAtomListStates[0] == H2LevelAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomListStates[0] == H2LevelAtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Hydrogen2")
                    {
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomLinearOffset;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                        atomPropertiesScript.hydrogenAtomListStates[0] = H2LevelAtomProperties.AtomBondingState.Successful;
                        atomPropertiesScript.hydrogenAtomListStates[1] = H2LevelAtomProperties.AtomBondingState.Successful;

                        // Trigger success particle effects for the involved atoms
                        hydrogenTwoSuccessParticleEffect.Play();
                        hydrogenOneSuccessParticleEffect.Play();
                    }
                  
					
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            case "Hydrogen2":
                {
					// Break if already made a bond
				if (atomPropertiesScript.hydrogenAtomListStates[1] == H2LevelAtomProperties.AtomBondingState.Successful || atomPropertiesScript.hydrogenAtomListStates[1] == H2LevelAtomProperties.AtomBondingState.Failed)
						break;
                    if (draggedAtom.name == "Hydrogen1")
                    {
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = hydrogenAtomLinearOffset;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                        atomPropertiesScript.hydrogenAtomListStates[0] = H2LevelAtomProperties.AtomBondingState.Successful;
                        atomPropertiesScript.hydrogenAtomListStates[1] = H2LevelAtomProperties.AtomBondingState.Successful;

                        // Trigger failure particle effects for the involved atoms
                        hydrogenOneSuccessParticleEffect.Play();
                        hydrogenTwoSuccessParticleEffect.Play();
                    }
                   
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            default:
                // Do nothing
                break;
        }
        IsStageBondComplete();
        // Reset draggedAtom
        draggedAtom = null;
    }

    public void TriggerShellRotation(GameObject collidedAtom, GameObject draggedAtom) {
        Transform collidedAtomShellTransform;
        Transform draggedAtomShellTransform;
        switch (collidedAtom.name)
        {
            case "Hydrogen1":
            case "Hydrogen2":
                collidedAtomShellTransform = collidedAtom.transform.FindChild("Shell").transform;
                collidedAtomShellTransform.localEulerAngles = new Vector3(collidedAtomShellTransform.localRotation.x, collidedAtomShellTransform.localRotation.y, 264);
                draggedAtomShellTransform = draggedAtom.transform.FindChild("Shell").transform;
                draggedAtomShellTransform.localEulerAngles = new Vector3(draggedAtomShellTransform.localRotation.x, draggedAtomShellTransform.localRotation.y, 83);
                break;
            default:
                break;
        }
    }

    public void IsStageBondComplete() {
        if (atomPropertiesScript.hydrogenAtomListStates[0] == H2LevelAtomProperties.AtomBondingState.Successful && atomPropertiesScript.hydrogenAtomListStates[1] == H2LevelAtomProperties.AtomBondingState.Successful) {
            if (hydrogenOneFinisherParticleSystem.activeSelf == false)
            {   
                hydrogenOneFinisherParticleSystem.SetActive(true);
            }   
            if (hydrogenTwoFinisherParticleSystem.activeSelf == false)
            {   
                hydrogenTwoFinisherParticleSystem.SetActive(true);
            }
            mainCanvas.GetComponent<EightSceneManager>().StageComplete();
        }
    }
}
