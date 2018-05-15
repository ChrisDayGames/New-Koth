using Determinism;
using Entitas;
using UnityEngine;

public class ColliderDebugView : ManagedBehaviour {

	private LogicContext _context;
	private IGroup <LogicEntity> _allColliders;

	public override void Initialize () {
		base.Initialize ();

		_context = Contexts.sharedInstance.logic;
		_allColliders = _context.GetGroup (LogicMatcher.Collider);

	}

	public override void Execute (float alpha) {
		base.Execute (alpha);

		foreach (LogicEntity e in _allColliders) {

			Color c = new Color (1, 1, 1, 1);

			if (e.isDangerous)
				c = Color.red;

			else if (e.hasPlayerID || e.hasFollowPoint)
				c = Color.green;
			


			foreach (FixedLine2 edge in e.collider.value.edges) {
				GameDebug.Gizmos.DrawLine (edge.start.ToVector3 (), edge.end.ToVector3 (), c);
			}

		}


	}

	public override void FixedExecute () {
		base.FixedExecute ();
	}

}
