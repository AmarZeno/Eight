using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelOneAtomProperties : MonoBehaviour {    
	public enum AtomBondingState { Unknown, Successful, Failed};
	public List<AtomBondingState> hydrogenAtomListStates = new List<AtomBondingState> {AtomBondingState.Unknown, AtomBondingState.Unknown};
}
