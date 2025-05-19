using UnityEngine;
using UnityEditor;
using System.IO;

public class GameDataEditorWindow : EditorWindow
{
    private int score;
    private string filePath;

    [MenuItem("Tools/Game Data Editor")]
    public static void ShowWindow()
    {
        GetWindow<GameDataEditorWindow>("Game Data Editor");
    }

    private void OnEnable()
    {
        filePath = Path.Combine(Application.persistentDataPath, "gamedata.json");
        LoadData();
    }

    private void OnGUI()
    {
        GUILayout.Label("Game Data (JSON)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Path:", filePath);

        EditorGUILayout.Space();
        score = EditorGUILayout.IntField("Score", score);

        EditorGUILayout.Space();
        if (GUILayout.Button("Save"))
        {
            SaveData();
        }

        if (GUILayout.Button("Reset to Zero"))
        {
            score = 0;
            SaveData();
        }

        if (GUILayout.Button("Open File Location"))
        {
            EditorUtility.RevealInFinder(filePath);
        }
    }

    private void LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            GameDataWrapper wrapper = JsonUtility.FromJson<GameDataWrapper>(json);
            score = wrapper.score;
        }
        else
        {
            score = 0;
        }
    }

    private void SaveData()
    {
        GameDataWrapper wrapper = new GameDataWrapper { score = score };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Game data saved.");
    }

    [System.Serializable]
    private class GameDataWrapper
    {
        public int score;
    }
}
