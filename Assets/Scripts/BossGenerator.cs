using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{

    public List<Element> elementOrder;

    public List<Element> newElementOrder;

    public static int LimbCount, LimbTypeCount;
    [System.Serializable]
    public class Column
    {
        public GameObject[] limb = new GameObject[LimbTypeCount];
    }

    public Column[] allLimbs = new Column[LimbCount];

    public GameObject referenceBoss;

    private GameObject boss;

    private int curLimbIndex = 0;

    public bool addLimb = false;

    private void Start()
    {
        boss = Instantiate(referenceBoss);

        newElementOrder = new List<Element>(elementOrder);

        System.Random rng = new System.Random();
        int n = newElementOrder.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Element value = newElementOrder[k];
            newElementOrder[k] = newElementOrder[n];
            newElementOrder[n] = value;
        }
    }

    private void Update()
    {
        if (addLimb)
        {
            AddLimb();
            addLimb = false;
        }

    }

    public void AddLimb()
    {
        if (curLimbIndex >= elementOrder.Count)
        {
            Debug.LogWarning($"Index out of range {curLimbIndex}");
            return;
        }

        Element element = newElementOrder[curLimbIndex];
        print(LimbTypeCount);
        
        int index = elementOrder.IndexOf(element);
        if (index >= 0 && index < elementOrder.Count)
        {
            GameObject limb = allLimbs[curLimbIndex].limb[index];
            Instantiate(limb, boss.transform);
            curLimbIndex++;
        }
        else
        {
            Debug.LogWarning($"Could not find limb type for element {element}");
        }
    }
}

