//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class ControllerInputEventSystem : Entitas.ReactiveSystem<InputEntity> {

    readonly Entitas.IGroup<InputEntity> _listeners;

    public ControllerInputEventSystem(Contexts contexts) : base(contexts.input) {
        _listeners = contexts.input.GetGroup(InputMatcher.ControllerInputListener);
    }

    protected override Entitas.ICollector<InputEntity> GetTrigger(Entitas.IContext<InputEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(InputMatcher.ControllerInput)
        );
    }

    protected override bool Filter(InputEntity entity) {
        return entity.hasControllerInput;
    }

    protected override void Execute(System.Collections.Generic.List<InputEntity> entities) {
        foreach (var e in entities) {
            var component = e.controllerInput;
            foreach (var listenerEntity in _listeners) {
                foreach (var listener in listenerEntity.controllerInputListener.value) {
                    listener.OnControllerInput(e, component.snapshot);
                }
            }
        }
    }
}
