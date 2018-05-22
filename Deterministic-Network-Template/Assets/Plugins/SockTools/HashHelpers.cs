namespace SockTools {

	public static class HashHelpers {

		/// <summary>
		/// Code for this function found here
		/// https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode/263416#263416
		/// </summary>
		/// <returns>The hash code.</returns>
		/// <param name="fields">Fields.</param>
		public static int GetHashCode<T> (params T[] fields) {

			//do not check for overflow
			unchecked {

				//start with prime number
				int hash = 17;

				//for each field to be calculated in the object
				for (int i = 0; i < fields.Length; i++)

					//multiply the hash with another prime number + the hashcode of the field
					hash = hash * 23 + fields[i].GetHashCode();

				//return the calulated hash code
				return hash;

			}

		}

	}

}