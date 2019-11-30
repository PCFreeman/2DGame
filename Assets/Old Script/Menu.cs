using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject ExitButton;
    RectTransform StartButtonPosition;
    RectTransform ExitButtonPosition;
    float CurrentTime;

    private void Start()
    {
        StartButtonPosition = StartButton.GetComponent<RectTransform>();
        ExitButtonPosition = ExitButton.GetComponent<RectTransform>();
    }
    IEnumerator Move(RectTransform Button, Vector3 sPosition, Vector3 ePosition, float speed)
    {
        CurrentTime += Time.time;
        while (CurrentTime < speed)
        {
            Button.anchoredPosition = Vector3.Lerp(sPosition, ePosition, CurrentTime / speed);
            yield return null;
        }
        Button.anchoredPosition = ePosition;
    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }

    private void Update()
    {
        StartCoroutine(Move(StartButton.GetComponent<RectTransform>(), StartButtonPosition.anchoredPosition, new Vector3(0,-35,0),200));
        StartCoroutine(Move(ExitButton.GetComponent<RectTransform>(), ExitButtonPosition.anchoredPosition, new Vector3(0, -185, 0), 200));

    }
}
