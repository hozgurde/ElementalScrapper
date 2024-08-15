using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemnantGenerator : MonoBehaviour
{

    public List<GameObject> remnants;
    public int generateCount = 3;
    public int allCount = 10;
    public int maxRemnant = 3;
    int curGenerateCount = 0;
    int curAllCount = 0;
    List<int> remnantCounter;

    public Transform UpLeft;
    public Transform BottomRight;

    private void Start()
    {
        remnantCounter = new List<int>();
        remnantCounter.Add(0);
        remnantCounter.Add(0);
        remnantCounter.Add(0);
        remnantCounter.Add(0);
        remnantCounter.Add(0);
    }

    public void GenerateRemnant(Vector3 pos, float radius, int number, ElementType elementType)
    {
        if(curAllCount <= 0)
        {
            curAllCount = allCount;
            curGenerateCount = generateCount;
        }

        int k = UnityEngine.Random.Range(1, curAllCount + 1);
        if(k <= curGenerateCount)
        {
            if(remnantCounter[(int)elementType] >= maxRemnant)
            {
                return;
            }
            
            Vector3 remnantPos;
            for (int i = 0; i < number; i++)
            {
                remnantPos = Random.insideUnitCircle * radius;
                remnantPos += pos;
                if(remnantPos.y < BottomRight.position.y || remnantPos.y > UpLeft.position.y || remnantPos.x < UpLeft.position.x || remnantPos.x > BottomRight.position.x)
                {
                    return;
                }
                var remnant = Instantiate(remnants[(int)elementType]);
                remnant.transform.position = remnantPos;
            }

            remnantCounter[(int)elementType]++;
            curGenerateCount--;


        }
        curAllCount--;

        
    }

    public void RemoveRemnant(ElementType elementType)
    {
        remnantCounter[(int)elementType]--;
    }

}
