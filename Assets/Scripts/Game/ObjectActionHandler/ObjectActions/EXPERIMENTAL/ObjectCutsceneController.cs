//using UnityEngine;
//using UnityEngine.EventSystems;
//using Cinemachine;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//public class ObjectCutsceneController : ObjectActionHandler
//{
//    [SerializeField]
//    private CutsceneController cutsceneController;

//    [SerializeField]
//    private CinemaCutsceneController cinemaCutsceneController;

//    [SerializeField]
//    private Camera cutsceneCamera;

//    [SerializeField]
//    private CinemachineVirtualCamera cinemaCutsceneCamera;

//    [SerializeField]
//    private float cutsceneDuration = 3f;

//    public override void OnPointerClick(PointerEventData eventData)
//    {
//        Debug.Log("Is cutsceneController null: " + cutsceneController);
//        Debug.Log("Is cinemaCutsceneController null: " + cinemaCutsceneController);
//        Debug.Log("Is cutsceneCamera null: " + cutsceneCamera);
//        Debug.Log("Is cinemaCutsceneCamera null: " + cinemaCutsceneCamera);

//        if (cutsceneController != null && cutsceneCamera != null)
//        {
//            Debug.Log("Normal Cutscene goes");
//            cutsceneController.StartCutscene();
//            return;
//        }

//        if (cinemaCutsceneController != null && cinemaCutsceneCamera != null)
//        {
//            Debug.Log("Cinema Cutscene goes");
//            cinemaCutsceneController.StartCutscene(cutsceneDuration, cinemaCutsceneCamera);
//            return;
//        }
//    }
//#if UNITY_EDITOR
//    [CustomEditor(typeof(ObjectCutsceneController))]
//    public class ObjectCutsceneControllerEditor : Editor
//    {
//        private SerializedProperty cutsceneControllerProp;
//        private SerializedProperty cinemaCutsceneControllerProp;
//        private SerializedProperty cutsceneCameraProp;
//        private SerializedProperty cinemaCutsceneCameraProp;

//        private bool hasCutsceneController;
//        private bool hasCinemaCutsceneController;

//        private void OnEnable()
//        {
//            cutsceneControllerProp = serializedObject.FindProperty("cutsceneController");
//            cinemaCutsceneControllerProp = serializedObject.FindProperty("cinemaCutsceneController");
//            cutsceneCameraProp = serializedObject.FindProperty("cutsceneCamera");
//            cinemaCutsceneCameraProp = serializedObject.FindProperty("cinemaCutsceneCamera");


//            hasCutsceneController = cutsceneControllerProp.objectReferenceValue != null;
//            hasCinemaCutsceneController = cinemaCutsceneControllerProp.objectReferenceValue != null;
//        }

//        public override void OnInspectorGUI()
//        {
//            serializedObject.Update();

//            EditorGUI.BeginChangeCheck();
//            EditorGUILayout.PropertyField(cutsceneControllerProp);
//            if (EditorGUI.EndChangeCheck())
//            {
//                hasCutsceneController = cutsceneControllerProp.objectReferenceValue != null;
//                if (hasCutsceneController)
//                {
//                    hasCinemaCutsceneController = false;
//                    cinemaCutsceneControllerProp.objectReferenceValue = null;
//                }
//            }

//            EditorGUI.BeginChangeCheck();
//            EditorGUILayout.PropertyField(cinemaCutsceneControllerProp);
//            if (EditorGUI.EndChangeCheck())
//            {
//                hasCinemaCutsceneController = cinemaCutsceneControllerProp.objectReferenceValue != null;
//                if (hasCinemaCutsceneController)
//                {
//                    hasCutsceneController = false;
//                    cutsceneControllerProp.objectReferenceValue = null;
//                }
//            }

//            if (hasCutsceneController)
//            {
//                EditorGUILayout.PropertyField(cutsceneCameraProp);
//            } else if (hasCinemaCutsceneController)
//            {
//                EditorGUILayout.PropertyField(cinemaCutsceneCameraProp);
//            }

//            serializedObject.ApplyModifiedProperties();
//        }
//    }
//#endif
//}