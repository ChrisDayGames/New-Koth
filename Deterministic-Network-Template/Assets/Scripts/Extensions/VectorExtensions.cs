using UnityEngine;

public static class VectorExtensions {

	public static Vector3 ComponentMultiply (this Vector3 self, Vector3 multiplier) {

		self.x *=  multiplier.x;
		self.y *=  multiplier.y;
		self.z *=  multiplier.z;

		return self;

	}

	public static Vector3 RandomBetween (this Vector3 self, Vector3 min, Vector3 max) {

		self.x *= Random.Range (min.x, max.x);
		self.y *= Random.Range (min.y, max.y);
		self.z *= Random.Range (min.z, max.z);

		return self;

	}

}
