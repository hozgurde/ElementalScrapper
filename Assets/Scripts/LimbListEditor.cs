using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    public List<List<GameObject>> myList;
}

[CustomEditor(typeof(MyScript))]
public class MyScriptEditor : Editor
{
    private MyScript myScript;
    private int numRows = 0;
    private int numCols = 0;

    private void OnEnable()
    {
        myScript = (MyScript)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        numRows = EditorGUILayout.IntField("Number of Rows", numRows);
        numCols = EditorGUILayout.IntField("Number of Columns", numCols);

        // Add rows to the list
        while (myScript.myList.Count < numRows)
        {
            myScript.myList.Add(new List<GameObject>());
        }

        // Remove rows from the list
        while (myScript.myList.Count > numRows)
        {
            myScript.myList.RemoveAt(myScript.myList.Count - 1);
        }

        // Add columns to each row
        foreach (List<GameObject> row in myScript.myList)
        {
            while (row.Count < numCols)
            {
                row.Add(null);
            }
        }

        // Remove columns from each row
        foreach (List<GameObject> row in myScript.myList)
        {
            while (row.Count > numCols)
            {
                row.RemoveAt(row.Count - 1);
            }
        }

        // Display the list in the inspector
        for (int i = 0; i < myScript.myList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int j = 0; j < myScript.myList[i].Count; j++)
            {
                myScript.myList[i][j] = (GameObject)EditorGUILayout.ObjectField(myScript.myList[i][j], typeof(GameObject), true);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}