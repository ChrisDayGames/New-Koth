using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : CurveLerp {

	[Header ( "Soul Visuals" )]
	public SpriteRenderer sr;

	protected override void DelayedOnReachDestination () {

		gameObject.SetActive (false);

	}

	public void PlayOnHitParticles () {

		//Call global VFX

	}

}
