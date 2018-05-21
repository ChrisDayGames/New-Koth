using UnityEngine;

[System.Serializable]
public struct ColorSwap
{
	public int rValue;
	public Color32 swapColor;

	public ColorSwap SetSwapColor ( Color32 color )
	{
		ColorSwap cs = this;

		cs.swapColor = color;

		return cs;
	}
}