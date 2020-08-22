using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraRig))]
public class CameraRigEditor : Editor
{
    CameraRig cameraRig;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        cameraRig = (CameraRig)target;

        EditorGUILayout.LabelField("Camera Helper");

        if (GUILayout.Button("Save camera's position now."))
        {
            Camera camera = Camera.main;

            if (camera)
            {
                Transform camT = camera.transform;
                Vector3 camPos = camT.localPosition;
                Vector3 camRight = camPos;
                Vector3 camLeft = camPos;
                camLeft.x = -camPos.x;
                cameraRig.cameraSettings.camPositionOffsetRight = camRight;
                cameraRig.cameraSettings.camPositionOffsetLeft = camLeft;
            }
        }

    }
}
