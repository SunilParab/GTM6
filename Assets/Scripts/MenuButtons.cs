using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu {

public class MenuButtons : MonoBehaviour
{
    [SerializeField]
    string destination;
    
    public void GoTo() {
        SceneManager.LoadScene(destination);
    }

    public void Exit() {
        Application.Quit();
    }
}

}