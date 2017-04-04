using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CCL4AtomProperties : MonoBehaviour {    
	public enum AtomBondingState { Unknown, Successful, Failed};
	public List<AtomBondingState> hydrogenAtomStateList = new List<AtomBondingState> {AtomBondingState.Unknown, AtomBondingState.Unknown, AtomBondingState.Unknown, AtomBondingState.Unknown};
}
