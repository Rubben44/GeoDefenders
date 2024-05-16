using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using EasyTransition;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private float loadDelay;
    public void GoToMainMenu()
    {
        TransitionManager.Instance().Transition(0, transitionSettings, loadDelay);
    }

    public void PlayLevel1()
    {
        TransitionManager.Instance().Transition(1, transitionSettings, loadDelay);
    }

    public void PlayLevel2()
    {
        TransitionManager.Instance().Transition(2, transitionSettings, loadDelay);
    }
    public void PlayLevel3()
    {
        TransitionManager.Instance().Transition(3, transitionSettings, loadDelay);
    }

    public void RetryLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        TransitionManager.Instance().Transition(currentScene.buildIndex, transitionSettings, loadDelay);
    }

}
