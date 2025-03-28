using UnityEngine;

namespace NPCs {

public class NpcSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float spawnRange;
    [SerializeField]
    private int spawnAttempts;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("NPCSpawn",timer,timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NPCSpawn()
    {
        Vector3 NPCPos = new Vector3(Random.Range(-spawnRange,spawnRange),1.75f,Random.Range(-spawnRange,spawnRange));
        for (int i = 0; i < spawnAttempts; i++) {
            if (Physics.CheckBox(NPCPos+new Vector3(0,-0.6f,0),new Vector3(0.55f,1.1f,0.55f))) {
                NPCPos = new Vector3(Random.Range(-spawnRange,spawnRange),1.75f,Random.Range(-spawnRange,spawnRange));
            } else {
                Instantiate(enemyPrefab,NPCPos, Quaternion.identity);
                return;
            }
        }

        if (Physics.CheckBox(NPCPos+new Vector3(0,-0.6f,0),new Vector3(0.55f,1.1f,0.55f))) {
            //Destroy(NPC);
        } else {
            Instantiate(enemyPrefab,NPCPos, Quaternion.identity);
        }
    }
}

}