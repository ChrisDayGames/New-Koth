//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class CollisionInfoEventSystem : Entitas.ReactiveSystem<LogicEntity> {

    readonly Entitas.IGroup<LogicEntity> _listeners;

    public CollisionInfoEventSystem(Contexts contexts) : base(contexts.logic) {
        _listeners = contexts.logic.GetGroup(LogicMatcher.CollisionInfoListener);
    }

    protected override Entitas.ICollector<LogicEntity> GetTrigger(Entitas.IContext<LogicEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(LogicMatcher.CollisionInfo)
        );
    }

    protected override bool Filter(LogicEntity entity) {
        return entity.hasCollisionInfo;
    }

    protected override void Execute(System.Collections.Generic.List<LogicEntity> entities) {
        foreach (var e in entities) {
            var component = e.collisionInfo;
            foreach (var listenerEntity in _listeners) {
                foreach (var listener in listenerEntity.collisionInfoListener.value) {
                    listener.OnCollisionInfo(e, component.value);
                }
            }
        }
    }
}
