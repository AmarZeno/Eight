using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelFourAtomProperties : MonoBehaviour {    
	public enum AtomBondingState { Unknown, Successful, Failed};
    public List<AtomBondingState> hydrogenAtomStateList;

    public List<Vector2> hydrogenLinearOffsets;
    public List<Vector3> hydrogenShellRotations;

    public int firstCarbonSuccessBonds = 0;
    public int secondCarbonSuccessBonds = 3;
    public int carbonCarbonSuccessBonds = 0;
}
