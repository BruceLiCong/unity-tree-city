using Mapbox.Unity.Map;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupController : MonoBehaviour
{
    private const string MAP_SCENE_NAME = "Scene1";

    [SerializeField] 
    private Slider _progressBar;

    [SerializeField]
    private AbstractMap _map;

    [SerializeField]
    private GameObject _player;

    private void Awake()
    {
        Messenger.AddListener(MapEvent.MAP_INITIALIZED, OnMapInitialized);

        Physics.queriesHitTriggers = true;

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(_map.gameObject);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(MapEvent.MAP_INITIALIZED, OnMapInitialized);
    }

    private void OnMapInitialized()
    {
        GameObject player = Instantiate(_player);
        player.transform.position = new Vector3(-80, 1, -80); // TODO: Serialize? LatLng->World?
        DontDestroyOnLoad(player);

        StartCoroutine(LoadScene(MAP_SCENE_NAME));
    }

    private void OnMapProgress(int tilesReady, int tilesLength)
    {
        float progress = (float) tilesReady / tilesLength;
        _progressBar.value = progress;
    }

    /// http://www.alanzucconi.com/2016/03/30/loading-bar-in-unity/
    private IEnumerator LoadScene(string scene)
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Loading completed
            if (ao.progress == 0.9f)
            {
                ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
