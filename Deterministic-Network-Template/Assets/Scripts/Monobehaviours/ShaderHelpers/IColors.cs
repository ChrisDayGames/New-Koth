using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public interface IColors
{
	event UnityAction OnModified;
	Color32 this[int index] { get; }
	int Length { get; }
}
