using Entitas;
using Determinism;

public class RayCastSystem : IInitializeSystem, IExecuteSystem {

	readonly LogicContext _context;
	static IGroup <LogicEntity> _allColliders;

	public RayCastSystem (Contexts contexts) {

		_context = contexts.logic;
		_allColliders = _context.GetGroup (LogicMatcher.Collider);

	}	

	public void Initialize () {

		//get + sort all collider;s


	}

	public void Execute () {


		//Check for new colliders or removed collider + resort

	}



	private static FixedVector2 hit;
	public static bool Check (FixedVector2 origin, FixedVector2 end, out FixedVector2 intersection, int id, Mask check, out int otherID) {

		_allColliders = Contexts.sharedInstance.logic.GetGroup (LogicMatcher.Collider);
		intersection = FixedVector2.NAN;
		otherID = -1;
	
		foreach (LogicEntity e in _allColliders) {

			if (!check.HasFlag(e.collider.value.mask)) continue;
			if (id == e.id.value) continue;

			if (RayCast.CheckIntersection (origin, end, out intersection, e.collider.value)) {

				otherID = e.id.value;
				return true;

			}

		}

		return false;
			
	}

}
