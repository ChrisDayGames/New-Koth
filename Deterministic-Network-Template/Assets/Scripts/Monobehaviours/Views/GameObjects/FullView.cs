using Entitas.Unity;
using Entitas;
using UnityEngine;
using Determinism;
using CommandInput;
using TypeReferences;
using System.Collections.Generic;

public class FullView : FXView {

    [SerializeField, HideInInspector]
    private new string name;

	[ClassExtends (typeof (AnimationCommand))]
	public ClassTypeReference animationCommand;

	[ClassExtends (typeof (ParticleFXCommand))]
	public ClassTypeReference particleCommand;

	[ClassExtends (typeof (AudioFXCommand))]
	public ClassTypeReference audioCommand;
	
	private List <Command> commands;

	public override void Link(IEntity entity, IContext context) {
		base.Link (entity, context);

		var e = (LogicEntity) entity;
		commands = new List<Command> ();

		if (animationCommand.Type != null)
			commands.Add ((Command) System.Activator.CreateInstance (animationCommand, e.id.value, anim));

		if (particleCommand.Type != null)
			commands.Add ((Command) System.Activator.CreateInstance (particleCommand, e.id.value));

		if (audioCommand.Type != null)
			commands.Add ((Command) System.Activator.CreateInstance (audioCommand, e.id.value, name, gameObject));

	}

	public override void OnDirty(LogicEntity entity) {
		base.OnDirty (entity);

		foreach (Command command  in commands)
			command.Execute ();

		entity.isDirty = false;

	}

    public override void GetReferences() {
        base.GetReferences();

        if (!Application.isPlaying)
            name = gameObject.name;

    }

}
