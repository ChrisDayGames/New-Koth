namespace CommandInput {

	public abstract class ParticleFXCommand : FXCommand {

		public ParticleFXCommand (int _e) 
			: base(_e) {

			priority = (int) Priority.VERY_SLOW;

		}

	}

}