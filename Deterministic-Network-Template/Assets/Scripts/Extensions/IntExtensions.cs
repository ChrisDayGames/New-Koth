using Determinism;
using Entitas;

public static class IntExtensions {

    public static int Wrap(this int index, int n) {
        return ((index % n) + n) % n;
    }

}
