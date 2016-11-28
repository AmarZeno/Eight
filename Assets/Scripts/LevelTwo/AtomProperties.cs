using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AtomProperties : MonoBehaviour {    
	public enum AtomBondingState { Unknown, Successful, Failed};
	public List<AtomBondingState> flourineAtomListStates = new List<AtomBondingState> {AtomBondingState.Unknown, AtomBondingState.Unknown, AtomBondingState.Unknown, AtomBondingState.Unknown};
}
