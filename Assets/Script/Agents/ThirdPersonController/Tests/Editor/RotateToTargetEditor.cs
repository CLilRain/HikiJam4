using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RotateToTarget))]
public class RotateToTargetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = (RotateToTarget) target;

        if (GUILayout.Button("LookTowardsTargetAtFixedDistance"))
        {
            script.LookTowardsTargetAtFixedDistance();
        }


        if (GUILayout.Button("LookTowardsTargetWithTilt"))
        {
            script.LookTowardsTargetWithTilt();
        }

        if (GUILayout.Button("Move To Behind Target"))
        {
            script.MoveToBehindTargetWithTilt();
        }
        

    }
}