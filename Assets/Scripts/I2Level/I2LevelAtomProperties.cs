using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class I2LevelAtomProperties : MonoBehaviour {    
	public enum AtomBondingState { Unknown, Successful, Failed};
	public List<AtomBondingState> iodineAtomListStates = new List<AtomBondingState> {AtomBondingState.Unknown, AtomBondingState.Unknown};
}
