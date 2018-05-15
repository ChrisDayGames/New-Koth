using Determinism;
using Entitas;

namespace CommandInput{

	public abstract class AxisCommand2D : Command {

		public FixedVector2 axes;

		public AxisCommand2D (int _e, FixedVector2 _axes) : base(_e) {

			axes = _axes;

		}

	}

}
