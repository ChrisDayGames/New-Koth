using Determinism;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Meta, Unique, Event(false)]
public class MenuStateComponent : IComponent {
	public MenuState value;
}


[Meta, Event (false)]
public class ScoreComponent : IComponent {

	public int playerID;
	public int score;

}
