using System.Collections.Generic;
using UnityEngine;

public class ClothController : MonoBehaviour
{
    public List<GameObject> targetObjects;
    private List<Cloth> clothComponents = new List<Cloth>();
    private Transform player;

    public List<GameObject> targetObjects_02;
    private List<Cloth> clothComponents_02 = new List<Cloth>();

    private float elapsedTime = 0f;

    void Start()
    {
        player = transform;

        FindClothComponentsInChildren(targetObjects, clothComponents);
        FindClothComponentsInChildren(targetObjects_02, clothComponents_02);

        EnableClothComponents(clothComponents);
        EnableClothComponents(clothComponents_02);
    }

    void FindClothComponentsInChildren(List<GameObject> objects, List<Cloth> clothList)
    {
        foreach (GameObject obj in objects)
        {
            FindClothComponentsInChildrenRecursive(obj.transform, clothList);
        }
    }

    void FindClothComponentsInChildrenRecursive(Transform parent, List<Cloth> clothList)
    {
        Cloth cloth = parent.GetComponent<Cloth>();
        if (cloth != null)
        {
            clothList.Add(cloth);
        }

        foreach (Transform child in parent)
        {
            FindClothComponentsInChildrenRecursive(child, clothList);
        }
    }

    void EnableClothComponents(List<Cloth> cloths)
    {
        foreach (Cloth cloth in cloths)
        {
            cloth.enabled = true;
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > 1f)
        {
            DisableClothComponents(clothComponents_02);

            foreach (GameObject obj in targetObjects)
            {
                CheckAndDisableCloth(obj.transform);
            }
        }
    }

    void DisableClothComponents(List<Cloth> cloths)
    {
        foreach (Cloth cloth in cloths)
        {
            cloth.enabled = false;
        }
    }

    void CheckAndDisableCloth(Transform parentTransform)
    {
        foreach (Transform child in parentTransform)
        {
            Cloth cloth = child.GetComponent<Cloth>();
            if (cloth != null)
            {
                float distance = Vector3.Distance(player.position, child.position);
                bool isFartherThan9f = distance > 9f;
                cloth.enabled = !isFartherThan9f;
            }

            CheckAndDisableCloth(child);
        }
    }
}