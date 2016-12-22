using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelTwoAtomProperties : MonoBehaviour {    
	public enum AtomBondingState { Unknown, Successful, Failed};
	public List<AtomBondingState> flourineAtomListStates = new List<AtomBondingState> {AtomBondingState.Unknown, AtomBondingState.Unknown, AtomBondingState.Unknown, AtomBondingState.Unknown};
}
