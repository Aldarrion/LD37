using UnityEngine;
using System.Collections;

public class StopSmoke : MonoBehaviour {

    private ParticleSystem smoke;

	// Use this for initialization
	void Awake () {
        smoke = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	public void StopSmoking() {
        smoke.enableEmission = false;
	}
}
