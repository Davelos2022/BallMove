using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    [Header("Objects for spawn")]
    [Space]
    [SerializeField] private GameObject[] objectGame;

    [Header("Settings spawn")]
    [Space]
    [SerializeField] private Transform[] positonSpawn;
    [SerializeField] private GameObject parentObject;

    private List<GameObject> listObjectsSpawn = new List<GameObject>();
    private void Start()
    {
        PreloaderSpawn(objectGame, parentObject, listObjectsSpawn, positonSpawn.Length);
        Shuffle(listObjectsSpawn);
        Sorting(positonSpawn, listObjectsSpawn);

        GameManager.Instance.CheckItem(listObjectsSpawn);
    }

    private void PreloaderSpawn(GameObject[] objThatSpawn, GameObject parent, List<GameObject> objList, int maxCountSpawn)
    {
        for (int x = 0; x < maxCountSpawn; x++)
        {
            GameObject item = Instantiate(objThatSpawn[Random.Range(0, objectGame.Length)], parent.transform);
            objList.Add(item);
        }
    }

    private void Shuffle<T>(List<T> obj)
    {
        for (int i = obj.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T tmp = obj[i];
            obj[i] = obj[j];
            obj[j] = tmp;
        }
    }

    private void Sorting (Transform[] spawnPositon, List<GameObject> objects)
    {
        for (int x = 0; x < objects.Count; x++)
        {
            objects[x].transform.position = spawnPositon[x].position;
        }
    }
}
