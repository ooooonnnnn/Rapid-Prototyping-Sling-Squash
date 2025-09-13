using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;


#if UNITY_EDITOR
[CustomEditor(typeof(ScoreManager))]
public class ScoreManager_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ScoreManager scoreManager = (ScoreManager)target;
        if (GUILayout.Button("Set Dev Score"))
        {
            scoreManager.SetDevScore();
        }
    }
}
#endif
