using UnityEngine;

public static class UITools
{
    public static int ScreenHeight => 1080; //SingletonMono<UIController>.Instance.RootWidget.height;

    public static int ScreenWidth => 1920; // SingletonMono<UIController>.Instance.RootWidget.width;

    //public static string SceneName => LevelManager.SceneName;

    private static string _sceneName;
}