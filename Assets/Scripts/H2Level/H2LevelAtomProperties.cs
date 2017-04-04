﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class H2LevelAtomProperties : MonoBehaviour {    
	public enum AtomBondingState { Unknown, Successful, Failed};
	public List<AtomBondingState> hydrogenAtomListStates = new List<AtomBondingState> {AtomBondingState.Unknown, AtomBondingState.Unknown};
}
