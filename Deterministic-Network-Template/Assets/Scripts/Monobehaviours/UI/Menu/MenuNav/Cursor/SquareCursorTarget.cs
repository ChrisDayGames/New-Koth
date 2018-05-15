using Determinism;

public class SquareCursorTarget : CursorTarget {

    public override bool IsCollidingWith(CursorBehaviour cursor) {
        return BoundingBox.CheckOverlap(cursor.col.box, box);
    }

}
