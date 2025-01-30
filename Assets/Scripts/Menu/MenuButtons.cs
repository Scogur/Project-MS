using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void PlayButton(){
        Debug.Log("PlayButton clicked");
        SceneManager.LoadScene("VillageScene");
    }

    public void OptionsButton(){
        Debug.Log("OptionsButton clicked");
    }

    public void ExitButton(){
        Debug.Log("ExitButton clicked");
        Application.Quit();
    }
}
