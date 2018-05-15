namespace Determinism {

	public class SineLut : LookUpTable {

		#region Override

		//f(x) that the table will cache
		public override long F (long x) {

			return FixedMath.Sin (x);

		}

		#endregion


	}

}
