using System;
using UnityEngine;

public class Enum<T> where T : struct, IConvertible {
	
	public static int Count {

		get {
			if (!typeof(T).IsEnum)
				throw new ArgumentException("T must be an enumerated type");

			return Enum.GetNames(typeof (T)).Length;
		}

	}

	public static T[] Values {

		get {
			if (!typeof(T).IsEnum)
				throw new ArgumentException("T must be an enumerated type");

			return (T[])Enum.GetValues(typeof (T));
		}

	}
		
	public static T RandomValue () {

		return Values[UnityEngine.Random.Range (0, Count)];

	}

}
