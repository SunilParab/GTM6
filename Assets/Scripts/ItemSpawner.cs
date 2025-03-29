using UnityEngine;

namespace Items {

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private GameObject item2Prefab;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float spawnRange;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ItemSpawn",timer,timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ItemSpawn()
    {
        int type = Random.Range(0,2);
        switch (type) {
            case 0:
                Instantiate(itemPrefab,new Vector3(Random.Range(-spawnRange,spawnRange),2,Random.Range(-spawnRange,spawnRange)), Quaternion.identity);
                break;
            case 1:
                Instantiate(item2Prefab,new Vector3(Random.Range(-spawnRange,spawnRange),2,Random.Range(-spawnRange,spawnRange)), Quaternion.identity);
                break;
        }
    }
}

}