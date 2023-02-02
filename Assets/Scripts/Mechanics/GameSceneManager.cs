using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private ScreenTint screenTint;
    [SerializeField] private CameraConfiner cameraConfiner;

    private AsyncOperation unload;
    private AsyncOperation load;

    public static GameSceneManager Instance;

    private string currentScene;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    private void SwitchScene(string sceneName, Vector3 targetPosition)
    {
        load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(currentScene);
        currentScene = sceneName;

        var player = GameManager.Instance.Player.transform;

        CinemachineBrain currentCamera = Camera.main.GetComponent<CinemachineBrain>();
        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(
            player, 
            targetPosition - player.position);


        GameManager.Instance.Player.transform.position = 
            new Vector3(
                targetPosition.x, 
                targetPosition.y, 
                GameManager.Instance.Player.transform.position.z);
    }

    public void InitSceneTransition(string sceneName, Vector3 targetPosition)
    {
        StartCoroutine(TransitionScenes(sceneName, targetPosition));
    }

    private IEnumerator TransitionScenes(string sceneName, Vector3 targetPosition)
    {
        screenTint.Tint();

        yield return new WaitForSeconds(1 / screenTint.speed + .1f);

        SwitchScene(sceneName, targetPosition);

        while (load != null & unload != null)
        {
            if (load.isDone)
            {
                load = null;
            }

            if (unload.isDone)
            {
                unload = null;
            }

            yield return new WaitForSeconds(.1f);
        }

        cameraConfiner.UpdateBounds();

        screenTint.Untint();
    }
}
