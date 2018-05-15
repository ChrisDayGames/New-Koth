namespace Determinism {

	public class SqrtLut : LookUpTable {

		#region Override

		public override void ConfigureTable () {

			defaultValue = 0;
			minValue = 0;
			maxValue = FixedMath.ONE + increment;
			increment = FixedMath.Hundredth;

		}

		//f(x) that the table will cache
		public override long F (long x) {
			return FixedMath.Sqrt (x);
		}

		#endregion


	}

}