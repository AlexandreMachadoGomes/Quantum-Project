using System;
using Photon.Deterministic;
using Quantum;
using UnityEngine;

public class LocalInput : MonoBehaviour {
    
    private void OnEnable() {
        QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
    }

    public void PollInput(CallbackPollInput callback) {
        Quantum.Input i = new Quantum.Input();

        i.Dash = UnityEngine.Input.GetButton("Dash");

        i.DirectionX = (short)(UnityEngine.Input.GetAxis("Horizontal") * 10);
        i.DirectionY = (short)(UnityEngine.Input.GetAxis("Vertical") * 10);


        callback.SetInput(i, DeterministicInputFlags.Repeatable);
    }


    
}
