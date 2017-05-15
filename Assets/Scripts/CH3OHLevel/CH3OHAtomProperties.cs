using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CH3OHAtomProperties : MonoBehaviour {    
	public enum AtomBondingState { Unknown, Successful, Failed};
	public List<AtomBondingState> hydrogenAtomStateList;

    public List<Vector2> hydrogenLinearOffsets;
    public List<Vector3> hydrogenShellRotations;
    public List<GameObject> hydrogenAtoms;

    public int carbonSuccessBonds = 0;
    public int oxygenSuccessBonds = 0;
    public int carbonOxygenSuccessBonds = 0;

    public GameObject lastBondedHydrogenWithCarbon;
    public GameObject lastBondedHydrogenWithOxygen;

    public Vector2 hydrogenAtomOverridenLinearOffset;

    public Vector3 hydrogenShellFacingDown;
    public Vector3 hydrogenShellFacingUp;

    // Particle effects

    public ParticleSystem[] successParticleEffects;
    public ParticleSystem[] failureParticleEffects;

    public GameObject finisherParticleSystem1;
    public GameObject finisherParticleSystem2;
    public GameObject mainCanvas;
}
