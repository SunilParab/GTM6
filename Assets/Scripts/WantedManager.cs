using UnityEngine;

namespace NPCs
{

public class WantedManager : MonoBehaviour
{
    [SerializeField]
    float wantedLevel;
    public static WantedManager reference;
    bool spawningCops = false;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnRange;
    [SerializeField]
    private int spawnAttempts;

    void Awake()
    {
        reference = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        wantedLevel -= Time.deltaTime;
        if (wantedLevel < 0) {
            wantedLevel = 0;
        }
    }

    public void GainWanted(float amount) {Debug.Log("gain");
        wantedLevel += amount;
        if (!spawningCops) {
            if (wantedLevel >= 10) {
                CopSpawn();
            }
        }
    }

    void CopSpawn()
    {

        if (wantedLevel >= 10) {
            spawningCops = true;

            Vector3 NPCPos;
            for (int i = 0; i < spawnAttempts; i++) {

                NPCPos = new Vector3(Random.Range(-spawnRange,spawnRange),1.75f,Random.Range(-spawnRange,spawnRange));

                if (!Physics.CheckBox(NPCPos+new Vector3(0,-0.6f,0),new Vector3(0.55f,1.1f,0.55f))) {
                    Instantiate(enemyPrefab,NPCPos, Quaternion.identity);
                    break;
                }
            }

            if (wantedLevel > 200) {
                Invoke("CopSpawn",1);
            } else if (wantedLevel > 100) {
                Invoke("CopSpawn",3);
            } else if (wantedLevel > 50) {
                Invoke("CopSpawn",5);
            } else if (wantedLevel > 25) {
                Invoke("CopSpawn",8);
            } else {
                Invoke("CopSpawn",10);
            }

        } else {
            spawningCops = false;
        }
    }

}

}