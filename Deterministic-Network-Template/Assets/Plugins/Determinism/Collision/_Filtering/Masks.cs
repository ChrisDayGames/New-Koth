namespace Determinism {

	[System.Flags]
	public enum Mask : int {

		NONE = 0,
		DEFAULT = 1,
		P1 = 2,
		P2 = 4,
		P3 = 8,
		P4 = 16,
		P5 = 32,
		P6 = 64,
		P7 = 128,
		P8 = 256,
		SOLID = 512,
		DEAD = 1024,

	}

	public static class MaskUtil {
		
		public static Mask AddFlag (this Mask a, Mask b) {
			return a | b;
		}

		public static Mask AddFlags (this Mask a, params Mask[] list) {

			foreach (Mask b in list)
				a = a | b;

			return a;

		}

		public static Mask RemoveFlag (this Mask a, Mask b) {
			return a & (~b);
		}

		public static Mask RemoveFlags (this Mask a, params Mask[] list) {

			foreach (Mask b in list)
				a = a & (~b);

			return a;

		}

		// Works with "None" as well
		public static bool HasFlag (this Mask a, Mask b) {
			return (a & b) == b;
		}

		public static Mask ToogleFlag (this Mask a, Mask b) {
			return a ^ b;
		}

		public static Mask GetMaskForPlayerID (int ID) {

			switch (ID) {

			case 0:
				return Mask.P1;

			case 1:
				return Mask.P2;

			case 2:
				return Mask.P3;

			case 3:
				return Mask.P4;

			case 4:
				return Mask.P5;

			case 5:
				return Mask.P6;

			case 6:
				return Mask.P7;

			case 7:
				return Mask.P8;

			}

			return Mask.DEFAULT;

		}

	}
		
}