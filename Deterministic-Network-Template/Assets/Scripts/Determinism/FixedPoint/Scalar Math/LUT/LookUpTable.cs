using System.Collections.Generic;
using UnityEngine;

namespace Determinism {
	
	public abstract class LookUpTable {

		#region Table

		public long[] testKeys;

		#endregion


		#region Constansts

		protected long defaultValue = 0;
		protected long minValue = 0;
		protected long maxValue = 0;
		protected long increment = 0;

		#endregion


		#region Functions

		public LookUpTable () {

			ConfigureTable ();
			BuildTable ();
			
		}

		public void BuildTable () {

			testKeys =  new long[ (maxValue - minValue).Div(increment) ];

			for (long i = 0, x = minValue, l = maxValue; x <= l; x += increment, i++) {

				long y = F (x);
				testKeys[i] = y;

			}

		}

		#endregion

		#region Mutable

		//configure table vars
		public virtual void ConfigureTable () {}

		//f(x) that the table will cache
		public virtual long F (long x) {return x;}

		#endregion

	}

}
