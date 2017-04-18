using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CH3OHAtomProperties : MonoBehaviour {    
	public enum AtomBondingState { Unknown, Successful, Failed};
	public List<AtomBondingState> hydrogenAtomStateList;

    public List<Vector2> hydrogenLinearOffsets;
    public List<Vector3> hydrogenShellRotations;

    public int carbonSuccessBonds = 0;
    public int oxygenSuccessBonds = 0;
    public int carbonOxygenSuccessBonds = 0;
}
