using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    [Header("Objects for spawn")]
    [Space]
    [SerializeField] private GameObject[] objectGame; // Objects to spawn

    [Header("Settings spawn")]
    [Space]
    [SerializeField] private Transform[] positonSpawn; //Position future objects
    [SerializeField] private GameObject parentObject; //Parent future spawn

    private List<GameObject> listObjectsSpawn = new List<GameObject>();
    private void Start()
    {
        //Creating objects
        PreloaderSpawn(objectGame, parentObject, listObjectsSpawn, positonSpawn.Length);

        //Shuffle created objects
        Shuffle(listObjectsSpawn);

        //Sorting created objects
        Sorting(positonSpawn, listObjectsSpawn);

        // We pass the finished list of objects for counting
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

    //Shuffle created objects
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

    //Sorting created objects
    private void Sorting (Transform[] spawnPositon, List<GameObject> objects)
    {
        for (int x = 0; x < objects.Count; x++)
            objects[x].transform.position = spawnPositon[x].position;        
    }
}
