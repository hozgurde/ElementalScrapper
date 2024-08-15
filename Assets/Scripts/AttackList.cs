using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackList : MonoBehaviour
{
    public List<LimbType> limbTypes;
    public List<ElementType> elementTypes;

    public List<GameObject> projectiles;

    private List<List<List<GameObject>>> attackList;
    public List<GameObject> tempAttackList;

    public GameObject tempAttack;

    string attackPath = "BossAttacks/";

    public string bossName = "";

    private GameObject prefab;

    private void Start()
    {
        attackList = new List<List<List<GameObject>>>(limbTypes.Count);
        for (int i = 0; i < limbTypes.Count; i++)
        {
            attackList.Add(new List<List<GameObject>>(elementTypes.Count));
            for (int j = 0; j < elementTypes.Count; j++)
            {
                attackList[i].Add(new List<GameObject>());
            }
        }

        foreach (LimbType limb in limbTypes)
        {
            foreach(ElementType element in elementTypes)
            {
                for(int i = 0; true; i++)
                {
                    prefab = Resources.Load<GameObject>(attackPath + bossName + "_L" + (int)limb + "_E" + (int)element + "_A" + i);
                    if (!prefab)
                    {
                        break;
                    }
                    attackList[(int)limb][(int)element].Add(prefab);
                }
            }
        }


    }

    public GameObject GetAttack(LimbType limbType, ElementType elementType, ref int action)
    {
        /*action = UnityEngine.Random.Range(0, attackList[(int)limbType][(int)elementType].Count);
        return attackList[(int)limbType][(int)elementType][action];*/
        return tempAttackList[(int)limbType];
    }

    public void GenerateProjectile(ElementType elementType, float radius ,float _time, Vector3 _departure, Vector3 _destination)
    {
        var projectile = Instantiate(projectiles[(int)elementType], _departure, Quaternion.identity);
        projectile.transform.localScale = radius * 1.25f * Vector3.one;
        projectile.GetComponent<ThrowBall>().Throw(_time, _departure, _destination);
    }
}
