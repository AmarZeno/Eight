using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelThreeAtomProperties : MonoBehaviour {    
	public enum AtomBondingState { Unknown, Successful, Failed};
	public List<AtomBondingState> flourineAtomStateList = new List<AtomBondingState> {AtomBondingState.Unknown, AtomBondingState.Failed, AtomBondingState.Unknown, AtomBondingState.Failed};
}
