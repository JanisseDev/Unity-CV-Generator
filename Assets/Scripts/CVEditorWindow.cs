using UnityEngine;
using UnityEditor;
using System.IO;

class CVEditorWindow : EditorWindow {
    #region Properties
    [SerializeField] private CVDisplayer cvDisplayerPrefab = null;
    [SerializeField] private CVData cvData = default;
    
    private SerializedObject so;
    private Vector2 scrollPos;
    
    private CVDisplayer cvDisplayer
    {
        get
        {
            if (cvDisplayerPrefab == null) { return null; }
            
            if (_cvDisplayer == null) { _cvDisplayer = FindObjectOfType<CVDisplayer>(); }
            if (_cvDisplayer == null) { _cvDisplayer = Instantiate(cvDisplayerPrefab); }
            return _cvDisplayer;
        }
    }

    private CVDisplayer _cvDisplayer = null;
    #endregion
    
    #region EditorWindow Methods
    [MenuItem ("CV Generator/Editor")]
    public static void  ShowWindow () {
        EditorWindow.GetWindow(typeof(CVEditorWindow));
    }

    void OnGUI () {
        if (so == null) { so = new SerializedObject(this);}
        so.Update();
        
        EditorGUILayout.BeginVertical();
        
        EditorGUILayout.BeginHorizontal();
        SerializedProperty cvDisplayerProperty = so.FindProperty("cvDisplayerPrefab");
        EditorGUILayout.PropertyField(cvDisplayerProperty, true);
        if (GUILayout.Button("Update Displayer")) { UpdateDisplayer(); }
        if (GUILayout.Button("Refresh layout")) { RefreshLayout(); }
        EditorGUILayout.EndHorizontal();

        if (cvDisplayerPrefab == null)
        {
            EditorGUILayout.HelpBox("Please select a Prefab inside the Designs folder", MessageType.Error);
        }
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Load data")) { LoadData(); }
        if(GUILayout.Button("Save data")) { SaveData(); }
        if(GUILayout.Button("Export to PNG")) { ExportToPNG(); }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.HelpBox("Don't forget to edit your Game window resolution to a fixed resolution of 2480x3508 for a crisp A4 export", MessageType.Info);
        
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        SerializedProperty cvDataProperty = so.FindProperty("cvData");
        EditorGUILayout.PropertyField(cvDataProperty, new GUIContent("Data"), true);
        EditorGUILayout.EndScrollView();
        
        EditorGUILayout.EndVertical();
        
        so.ApplyModifiedProperties();
        RefreshDisplayerData();
    }
    
    public static void ExportToPNG()
    {
        string folderPath = EditorPrefs.GetString("CV_Generator_folderPath", "");
        string fullPath = EditorUtility.SaveFilePanel("Save", folderPath, "", "png");
        if (fullPath != null && fullPath != "")
        {
            folderPath = fullPath.Remove(fullPath.LastIndexOf('/'));
            EditorPrefs.SetString("CV_Generator_folderPath", folderPath);

            EditorApplication.ExecuteMenuItem("Window/General/Game");
            ScreenCapture.CaptureScreenshot(fullPath);
        }
    }
    #endregion

    #region Data Methods
    internal void SaveData()
    {
        string path = EditorUtility.SaveFilePanel("Save CV json file", null, "cvData", "json");
        string data = JsonUtility.ToJson(cvData);
        File.WriteAllText(path, data);
    }

    internal void LoadData()
    {
        string defaultFolderPath = PlayerPrefs.GetString("CV_Generator_lastFilePath", "");
        string path = EditorUtility.OpenFilePanel("Load CV json file", defaultFolderPath, "json");
        Debug.Log(path);
        if (!string.IsNullOrEmpty(path))
        {
            PlayerPrefs.SetString("CV_Generator_lastFilePath", path);
            string dataStr = File.ReadAllText(path);
            cvData = JsonUtility.FromJson<CVData>(dataStr);
            RefreshDisplayerData();
        }
    }
    #endregion

    #region Displayer Methods
    internal void RefreshDisplayerData()
    {
        cvDisplayer?.SetData(cvData);
    }
    
    internal void UpdateDisplayer()
    {
        if (_cvDisplayer != null)
        {
            DestroyImmediate(_cvDisplayer.gameObject);
            _cvDisplayer = null;
        }

        RefreshDisplayerData();
    }

    private void RefreshLayout()
    {
        if (cvDisplayer != null)
        {
            cvDisplayer.gameObject.SetActive(false);
            cvDisplayer.gameObject.SetActive(true);
        }
    }
    #endregion
}