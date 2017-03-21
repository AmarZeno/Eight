using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class I2LevelBond : MouseDrag {

    // Constants
    readonly Vector2 iodineAtomLinearOffset = new Vector2(14.5f, 0);

	// Variables
	public GameObject atomProperties;
    private GameObject draggedAtom;
	private I2LevelAtomProperties atomPropertiesScript;

    // Particle effects

    public ParticleSystem iodineOneSuccessParticleEffect;
    public ParticleSystem iodineTwoSuccessParticleEffect;
    public ParticleSystem iodineOneWrongParticleEffect;
    public ParticleSystem iodineTwoWrongParticleEffect;
    public GameObject iodineOneFinisherParticleSystem;
    public GameObject iodineTwoFinisherParticleSystem;
    public GameObject mainCanvas;

    void Start(){
        atomPropertiesScript = atomProperties.GetComponent<I2LevelAtomProperties>();
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
            case "Iodine1":
                {
					// Break if already made a bond
				if (atomPropertiesScript.iodineAtomListStates[0] == I2LevelAtomProperties.AtomBondingState.Successful || atomPropertiesScript.iodineAtomListStates[0] == I2LevelAtomProperties.AtomBondingState.Failed)
						break;

                    if (draggedAtom.name == "Iodine2")
                    {
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = iodineAtomLinearOffset;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                        atomPropertiesScript.iodineAtomListStates[0] = I2LevelAtomProperties.AtomBondingState.Successful;
                        atomPropertiesScript.iodineAtomListStates[1] = I2LevelAtomProperties.AtomBondingState.Successful;

                        // Trigger success particle effects for the involved atoms
                        iodineTwoSuccessParticleEffect.Play();
                        iodineOneSuccessParticleEffect.Play();
                    }
                  
					
                    // Enable bonding joint for colliding with any type of atom
                    collidedAtom.GetComponent<RelativeJoint2D>().enabled = true;
                }
                break;
            case "Iodine2":
                {
					// Break if already made a bond
				if (atomPropertiesScript.iodineAtomListStates[1] == I2LevelAtomProperties.AtomBondingState.Successful || atomPropertiesScript.iodineAtomListStates[1] == I2LevelAtomProperties.AtomBondingState.Failed)
						break;
                    if (draggedAtom.name == "Iodine1")
                    {
                        collidedAtom.GetComponent<RelativeJoint2D>().connectedBody = draggedAtom.GetComponent<Rigidbody2D>();
                        collidedAtom.GetComponent<RelativeJoint2D>().linearOffset = iodineAtomLinearOffset;
                        TriggerShellRotation(collidedAtom, draggedAtom);
                        atomPropertiesScript.iodineAtomListStates[0] = I2LevelAtomProperties.AtomBondingState.Successful;
                        atomPropertiesScript.iodineAtomListStates[1] = I2LevelAtomProperties.AtomBondingState.Successful;

                        // Trigger failure particle effects for the involved atoms
                        iodineOneSuccessParticleEffect.Play();
                        iodineTwoSuccessParticleEffect.Play();
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
            case "Iodine1":
            case "Iodine2":
                collidedAtomShellTransform = collidedAtom.transform.FindChild("Shell").transform;
                collidedAtomShellTransform.localEulerAngles = new Vector3(collidedAtomShellTransform.localRotation.x, collidedAtomShellTransform.localRotation.y, 0);
                draggedAtomShellTransform = draggedAtom.transform.FindChild("Shell").transform;
                draggedAtomShellTransform.localEulerAngles = new Vector3(draggedAtomShellTransform.localRotation.x, draggedAtomShellTransform.localRotation.y, 180);
                break;
            default:
                break;
        }
    }

    public void IsStageBondComplete() {
        if (atomPropertiesScript.iodineAtomListStates[0] == I2LevelAtomProperties.AtomBondingState.Successful && atomPropertiesScript.iodineAtomListStates[1] == I2LevelAtomProperties.AtomBondingState.Successful) {
            if (iodineOneFinisherParticleSystem.activeSelf == false)
            {
                iodineOneFinisherParticleSystem.SetActive(true);
            }
            if (iodineTwoFinisherParticleSystem.activeSelf == false)
            {
                iodineTwoFinisherParticleSystem.SetActive(true);
            }
            mainCanvas.GetComponent<EightSceneManager>().StageComplete();
        }
    }
}
