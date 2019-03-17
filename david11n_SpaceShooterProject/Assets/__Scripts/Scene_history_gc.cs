using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_history_gc : MonoBehaviour
{
    // buttons
    Button backButton;

    // sound effects
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // listeners 
        GameObject go;

        go = GameObject.Find("clickAS");
        audioSource = go.GetComponent<AudioSource>();

        go = GameObject.Find("ExitButton");
        backButton = go.GetComponent<Button>();
        backButton.onClick.AddListener(() => MenuClick("back"));

    }

    private void MenuClick(string butNum)
    {
        switch (butNum)
        {
            case "1":
                StartCoroutine(LoadSceneMM(butNum));
                break;
            case "difficulty":
                StartCoroutine(LoadSceneMM(butNum));
                break;
            case "setup":
                StartCoroutine(LoadSceneMM(butNum));
                break;
            case "history":
                StartCoroutine(LoadSceneMM(butNum));
                break;
            case "back":
                StartCoroutine(LoadSceneMM("setup"));
                break;
        }
    }

    IEnumerator LoadSceneMM(string butNum)
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        SceneManager.LoadScene("_Scene_" + butNum);
    }

    IEnumerator QuitGame()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        SceneManager.LoadScene("_Scene_0");
    }
}
