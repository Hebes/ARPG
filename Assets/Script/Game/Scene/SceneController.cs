using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景控制器
/// </summary>
public class SceneController : SMono<SceneController>
{
    private static AsyncOperation _asyncOperation;

    public static AsyncOperation LoadScene(string levelName)
    {
        if (_asyncOperation?.isDone == false) return _asyncOperation;
        _asyncOperation = SceneManager.LoadSceneAsync(levelName);
        if (_asyncOperation == null)
            $"{levelName}场景不存在，是不是没放在build里？还是名字写错了？".Log();
        return _asyncOperation;
    }
}