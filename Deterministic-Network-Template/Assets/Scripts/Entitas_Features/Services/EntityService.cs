using Entitas;

public class EntityService {

	public static EntityService singleton = new EntityService();

	Contexts _contexts;

	public void Initialize(Contexts contexts) {
		_contexts = contexts;
	}

	public LogicEntity CreatePlayer () {

		var entity = _contexts.logic.CreateEntity();
		return entity;

	}

	public LogicEntity CreateHat () {

		var entity = _contexts.logic.CreateEntity();
		return entity;

	}

	public LogicEntity CreateLevel () {

		var entity = _contexts.logic.CreateEntity();
		return entity;

	}

}