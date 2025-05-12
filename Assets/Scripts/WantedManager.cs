using UnityEngine;

namespace NPCs
{

public class WantedManager : MonoBehaviour
{
    public float wantedLevel;
    public static WantedManager reference;
    bool spawningCops = false;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnRange;
    [SerializeField]
    private int spawnAttempts;

    public int wantedStars = 0;
    public static readonly float[] starValues = {10,25,50,100,200};

    [SerializeField]
    WantedDisplay display;

    void Awake()
    {
        reference = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("RandomCopSpawn",5,5);
    }

    // Update is called once per frame
    void Update()
    {
        wantedLevel -= Time.deltaTime;
        if (wantedLevel < 0) {
            wantedLevel = 0;
        }
        UpdateWantedStars();
    }

    public void GainWanted(float amount) {
        wantedLevel += amount;
        if (!spawningCops) {
            if (wantedLevel > starValues[0]) {
                CopSpawn();
            }
        }
        UpdateWantedStars();
    }

    void CopSpawn()
    {

        if (wantedLevel > starValues[0]) {
            spawningCops = true;

            Vector3 NPCPos;
            for (int i = 0; i < spawnAttempts; i++) {

                NPCPos = new Vector3(Random.Range(-spawnRange,spawnRange),1.75f,Random.Range(-spawnRange,spawnRange));

                if (!Physics.CheckBox(NPCPos+new Vector3(0,-0.6f,0),new Vector3(0.55f,1.1f,0.55f))) {
                    Instantiate(enemyPrefab,NPCPos, Quaternion.identity);
                    break;
                }
            }

            if (wantedLevel > starValues[4]) {
                Invoke("CopSpawn",1);
            } else if (wantedLevel > starValues[3]) {
                Invoke("CopSpawn",3);
            } else if (wantedLevel > starValues[2]) {
                Invoke("CopSpawn",5);
            } else if (wantedLevel > starValues[1]) {
                Invoke("CopSpawn",8);
            } else {
                Invoke("CopSpawn",10);
            }

        } else { //This is the only way cops stop spawning
            spawningCops = false;
        }
    }

    void UpdateWantedStars() {
        if (wantedLevel > starValues[4]) {
            wantedStars = 5;
        } else if (wantedLevel > starValues[3]) {
            wantedStars = 4;
        } else if (wantedLevel > starValues[2]) {
            wantedStars = 3;
        } else if (wantedLevel > starValues[1]) {
            wantedStars = 2;
        } else if (wantedLevel > starValues[0]) {
            wantedStars = 1;
        } else {
            wantedStars = 0;
        }

        display.ShowStars();
    }

    //Spawning seperate from the normal loop
    void RandomCopSpawn()
    {

        Vector3 NPCPos;
        for (int i = 0; i < spawnAttempts; i++) {

            NPCPos = new Vector3(Random.Range(-spawnRange,spawnRange),1.75f,Random.Range(-spawnRange,spawnRange));

            if (!Physics.CheckBox(NPCPos+new Vector3(0,-0.6f,0),new Vector3(0.55f,1.1f,0.55f))) {
                GameObject enem = Instantiate(enemyPrefab,NPCPos, Quaternion.identity);
                enem.GetComponent<CopBehavior>().SetLongLife(10);
                break;
            }
        }

    }

}

}