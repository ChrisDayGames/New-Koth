using Determinism;
using Entitas;

namespace CommandInput{

	public abstract class AxisCommand1D : Command {

		public long axis;

		public AxisCommand1D (int _e, long _axis) : base(_e) {

			axis = _axis;

		}

	}

}
