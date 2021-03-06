//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class RotationEventSystem : Entitas.ReactiveSystem<LogicEntity> {

    public RotationEventSystem(Contexts contexts) : base(contexts.logic) {
    }

    protected override Entitas.ICollector<LogicEntity> GetTrigger(Entitas.IContext<LogicEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(LogicMatcher.Rotation)
        );
    }

    protected override bool Filter(LogicEntity entity) {
        return entity.hasRotation && entity.hasRotationListener;
    }

    protected override void Execute(System.Collections.Generic.List<LogicEntity> entities) {
        foreach (var e in entities) {
            var component = e.rotation;
            foreach (var listener in e.rotationListener.value) {
                listener.OnRotation(e, component.value);
            }
        }
    }
}
