using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[System.Serializable]
public struct HatAndBodySkins
{
	[Required]
	[AssetsOnly]
	[SerializeField]
	[HorizontalGroup]
	[LabelText( "Hat" )]
	private Skin m_HatSkin;

	[Required]
	[AssetsOnly]
	[SerializeField]
	[HorizontalGroup]
	[LabelText( "Body" )]
	private Skin m_BodySkin;

	public Skin HatSkin { get { return m_HatSkin; } }

	public Skin BodySkin { get { return m_BodySkin; } }
}
