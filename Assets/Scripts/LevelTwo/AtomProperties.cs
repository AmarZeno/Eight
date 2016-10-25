using UnityEngine;
using System.Collections;

public class AtomProperties : MonoBehaviour {
    public enum AtomBondingState { Unknown, Successful, Failed};

    public AtomBondingState flourineOneBondState = AtomBondingState.Unknown;
    public AtomBondingState flourineTwoBondState = AtomBondingState.Unknown;
    public AtomBondingState flourineThreeBondState = AtomBondingState.Unknown;
    public AtomBondingState flourineFourBondState = AtomBondingState.Unknown;
}
