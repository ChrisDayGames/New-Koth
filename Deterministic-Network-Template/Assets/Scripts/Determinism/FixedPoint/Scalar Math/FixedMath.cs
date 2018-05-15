//Sources:
//http://graphics.stanford.edu/~seander/bithacks.html
//https://hbfs.wordpress.com/2008/08/05/branchless-equivalents-of-simple-functions/
//http://jetfistgames.com/blog/post/fixed-point-math-c-part-2
//https://github.com/SnpM/LockstepFramework
//http://mathworld.wolfram.com/PiApproximations.html
//https://www.rookieslab.com/posts/fast-power-algorithm-exponentiation-by-squaring-cpp-python-implementation

//Future Research
//Fast Known Division: https://www.codeproject.com/Articles/17480/Optimizing-integer-divisions-with-Multiply-Shift-i
//Inverse Modular Moultiplier: https://www.rookieslab.com/posts/how-to-find-multiplicative-inverse-of-a-number-modulo-m-in-python-cpp

namespace Determinism {

	public static class FixedMath {

		#region Lookup Tables
		public static LookUpTable sqrtLut = new SqrtLut ();
		#endregion

		#region Constants

		//Bit Constants
		public const int TOTAL_BITS = 32;
		public const int DECIMAL_BITS = 16;
		public const int SIGN_EXTEND = TOTAL_BITS - 1;

		//Standard Constants
		public const long ONE = 1 << DECIMAL_BITS;
		public const float ONE_F = (float)ONE;
		public const double ONE_D = (double)ONE;
		public const long HALF = ONE / 2;
		public const long Tenth = ONE / 10;
		public const long Hundredth = ONE / 100;
		public const long Thousandth = ONE / 1000;
		public const long Epsilon = 1 << (DECIMAL_BITS - 10);

		//Min Max Constants
		public const long NEGATIVE_INFINITY = long.MinValue;
		public const long MIN_VALUE = long.MinValue + 1;
		public const long MAX_VALUE = long.MaxValue - 2;
		public const long POSITIVE_INFINITY = long.MaxValue - 1;
		public const long NAN = long.MaxValue;

		//Trig Constants
		public const long PI = (355 * ONE) / 113;
		public const long PI_OVER_TWO = PI / 2;
		public const long PI_OVER_THREE = PI / 3;
		public const long PI_OVER_FOUR = PI / 4;
		public const long TWO_PI = PI * 2;
		public const long Deg2Rad = PI / (180 * ONE);
		public const long Rad2Deg = (180 * ONE) / PI;

		#endregion

		#region Contructors

		public static long Create(long integer) {

			return integer << DECIMAL_BITS;

		}

		public static long Create(float singleFloat) {

			return (long)((double)singleFloat * ONE);

		}

		public static long Create(double doubleFloat) {

			return (long)(doubleFloat * ONE);

		}

		public static long Create(long Numerator, long Denominator) {

			return (Numerator << DECIMAL_BITS) / Denominator;

		}

		#endregion

		#region Casts

		//Sign
		public static int Sign (this long lhs) {


			if (lhs >= 0) return 1;
			else return -1;

			//return (int)(1 | (lhs >> SIGN_EXTEND));

		}

		//Convert to integer
		public static int ToInt (this long lhs) {

			return (int) lhs >> DECIMAL_BITS;

		}

		public static int FloorToInt (this long lhs) {

			return (int) lhs >> DECIMAL_BITS;

		}

		public static int CeilToInt (this long lhs) {

			return (int)(lhs + ONE - 1) >> DECIMAL_BITS;

		}

		public static int RoundToInt (this long lhs) {

			return (int)(lhs + HALF - 1) >> DECIMAL_BITS;

		}

		//Convert with decimal places
		public static float ToFloat (this long lhs) {

			return (float) (lhs / ONE_F);

		}

		public static float ToPreciseFloat(this long lhs) {

			return (float) (lhs / ONE_D);

		}

		public static double ToDouble (this long lhs) {

			return (double) (lhs / ONE_D);

		}

		#endregion

		#region basic operations
		//addition
		public static long Add (this long lhs, long rhs) {

			return lhs + rhs;

		}

		//subtraction
		public static long Sub (this long lhs, long rhs) {

			return lhs + rhs;

		}

		//multiplication
		public static long Mul (this long lhs, long rhs) {

			return (lhs * rhs) >> DECIMAL_BITS;

		}

		//"scalar" multiplication
		public static long IMul (this long lhs, int rhs) {

			return lhs * rhs;

		}


		//division
		public static long Div (this long lhs, long rhs) {

			return (lhs << DECIMAL_BITS) / rhs;

		}

		//modulo
		public static long Mod (this long lhs, long rhs) {

			return lhs % rhs;

		}

		//check if the number is even (faster than modulo)
		public static bool IsEven (this long lhs) {

			return (lhs & ONE) == 0;

		}

		//check if the number is even (faster than modulo)
		public static bool IsOdd (this long lhs) {

			return (lhs & ONE) == 1;

		}

		#endregion

		#region General Math
		public static long Max (this long lhs, long rhs) {

			return lhs >= rhs ? lhs : rhs;

		}

		public static long Min (this long lhs, long rhs) {

			return lhs <= rhs ? lhs : rhs;
		}

		public static long Clamp (this long lhs, long min, long max) {

			if (lhs < min) return min;
			else if (lhs > max) return max;

			return lhs;

		}

        public static long Wrap(this long index, long n) {
            return ((index % n) + n) % n;
        }

        //Abs
        public static long Abs (this long lhs) {

			if (lhs < 0) {
				return -lhs;

			}

			return lhs;

		}

		//Check if the magnitude of one is greater than the other
		public static bool AbsGreaterThan (this long lhs, long rhs) {

			if (lhs < 0) {
				return -lhs > rhs;

			} else {
				return lhs > rhs;
			}

		}

		//Check if the magnitude of one is greater than the other
		public static bool AbsLessThan (this long lhs, long rhs) {

			if (lhs < 0) {
				return -lhs < rhs;

			} else {
				return -lhs < rhs;

			}

		}

		public static long Floor (this long lhs) {

			return (lhs >> DECIMAL_BITS) << DECIMAL_BITS;

		}

		public static long Ceil (this long lhs) {

			return ((lhs + ONE - 1) >> DECIMAL_BITS) << DECIMAL_BITS;

		}

		public static long Round (this long lhs) {

			return ((lhs + HALF - 1) >> DECIMAL_BITS) << DECIMAL_BITS;

		}

		public static long Squared (this long lhs) {

			return (lhs * lhs) >> DECIMAL_BITS;

		}


		public static long Cubed (this long lhs) {

			return (((lhs * lhs) >> DECIMAL_BITS) * lhs) >> DECIMAL_BITS;

		}

		//O log (n) Power function
		public static long Pow(this long lhs, int power) {

			long n = ONE;

			while(power > 0) {

				if((power & 1) == 1) {
					n = (n * lhs) >> DECIMAL_BITS;
				}

				lhs = (lhs * lhs) >> DECIMAL_BITS;
				power >>= 1;

			}

			return n;

		}

		public static long Sqrt (this long f1) {

			if (f1 <= 0) return 0;

			long x, x1;

			x = (f1 >> 1) + 1;
			x1 = (x + (f1 / x)) >> 1;

			while (x1 < x) {

				x = x1;
				x1 = (x + (f1 / x)) >> 1;

			}
			return x << (DECIMAL_BITS >> 1);

		}

		public static long FastSqrt (this long f1) {

			//if (f1 <= 0) return 0;

			long multiplier = 1;
			//long four = ONE * 4;
			//long error = 0;

			while (f1 > ONE) {

				f1 >>= 2;
				multiplier <<= 1;

				//error += ((f1 & 0x0000ffff) * 2304) >> DECIMAL_BITS;

			}

			int key = (int) ((f1 * 100) >> DECIMAL_BITS);
			long floorRoot = sqrtLut.testKeys [key];
			long ceilRoot = sqrtLut.testKeys [key + 1];

			long decimalPart = f1 & 0x0000ffff;  
			f1 = floorRoot + (((ceilRoot - floorRoot) * decimalPart) >> DECIMAL_BITS);

			return f1 * multiplier;

		}

		public static long Lerp (this long start, long end, long t) {

			return (((end - start) * t) >> DECIMAL_BITS) + start;

		}

		public static long Lerp01 (this long start, long end, long t) {

			if (t < 0) t = 0;
			else if (t > ONE) t = ONE;

			return ((end - start) * t) >> DECIMAL_BITS;

		}

		public static void Swap (ref long lhs, ref long rhs) {

			long temp = lhs;
			lhs = rhs;
			rhs = temp;

		}

		public static long InertialDamp(this long currentValue, long targetValue, long smoothTime) {

			return (targetValue - currentValue).Mul (ONE.Div(smoothTime));

		}

		//rip off of unity smooth damp
		public static long SmoothDamp(this long current, long target, ref long speed, long smoothTime) {

			smoothTime = Max(Thousandth, smoothTime);

			long num = (ONE * 2).Div(smoothTime);
			long num2 = num / 50;

			long num3 = ONE.Div(ONE + num2 + Create (48, 100).Mul(num2.Squared ()) + Create (235, 1000).Mul(num2.Cubed ()));
			long delta = current - target;
			long num5 = target;

			target = current - delta;

			long num7 = (speed + num.Mul(delta)) / 50;
			speed = (speed - num.Mul(num7)).Mul(num3);

			long num8 = target + (delta + num7).Mul(num3);
			if (num5 - current > 0 == num8 > num5) {

				num8 = num5;
				speed = (num8 - num5) * 50;

			}
			return (num8 - current) * 50;

		}

		public static long SpringDamp (this long currentValue, long targetValue, long speed, long smoothTime) {

			long springConstant = (ONE.Div(smoothTime)) ;
			long delta = targetValue - currentValue;
			long springForce = delta.Mul (springConstant);
			long dampingForce = (speed * 2).Mul (springConstant.Sqrt ());

			return springForce - dampingForce;

		}

		public static long PingPong (long time, long length) {

			long doubleLength = length * 2;
			long normalizedTime = time.Mod (doubleLength);

			if (0 <= normalizedTime && normalizedTime <= length)
				return normalizedTime;
			else
				return doubleLength - normalizedTime;

		}

		#endregion

		#region Trig

		public static long FastSin (long lhs) {

			lhs.Div (PI_OVER_TWO);

			if (lhs < -PI)
				lhs += TWO_PI;
			else if (lhs > PI)
				lhs -= TWO_PI;

			if (lhs < 0)
				return Create (127323954,100000000).Mul (lhs) + Create (405284735, 1000000000).Mul (lhs.Squared ());
			else 
				return Create (127323954,100000000).Mul (lhs) - Create (405284735, 1000000000).Mul (lhs.Squared ());

		}

		public static long Sin (long lhs) {

			long sin = 0;
			lhs.Div (PI_OVER_TWO);

			if (lhs < -PI)
				lhs += TWO_PI;
			else if (lhs > PI)
				lhs -= TWO_PI;

			if (lhs < 0) {

				sin = Create (127323954,100000000).Mul (lhs) + Create (405284735, 1000000000).Mul (lhs.Squared ());

				if (sin < 0)
					sin = Create (225, 1000).Mul(-sin.Squared () - sin) + sin;
				else
					sin = Create (225, 1000).Mul(sin.Squared () - sin) + sin;

			} else {

				sin = Create (127323954,100000000).Mul (lhs) - Create (405284735, 1000000000).Mul (lhs.Squared ());

				if (sin < 0)
					sin = Create (225, 1000).Mul(-sin.Squared () - sin) + sin;
				else
					sin = Create (225, 1000).Mul(sin.Squared () - sin) + sin;

			}

			return sin;

		}

		public static long FastCos (long lhs) {

			lhs += PI_OVER_TWO;
			return FastSin (lhs);

		}

		public static long Cos (long lhs) {

			lhs += PI_OVER_TWO;
			return Sin (lhs);

		}

		public static long Tan (long lhs) {
			return lhs;
		}

		#endregion

	}

}


