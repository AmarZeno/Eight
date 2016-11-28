using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AtomProperties : MonoBehaviour {    
	public enum AtomBondingState { Unknown, Successful, Failed};
    public AtomBondingState flourineOneBondState = AtomBondingState.Unknown;
    public AtomBondingState flourineTwoBondState = AtomBondingState.Unknown;
    public AtomBondingState flourineThreeBondState = AtomBondingState.Unknown;
    public AtomBondingState flourineFourBondState = AtomBondingState.Unknown;
	public List<AtomBondingState> flourineAtomListStates = new List<AtomBondingState> {AtomBondingState.Unknown, AtomBondingState.Unknown, AtomBondingState.Unknown, AtomBondingState.Unknown};
}
