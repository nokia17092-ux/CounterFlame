using UnityEditor;
using UnityEngine;

public class BuildScript
{
    public static void Build()
    {
        string[] scenes = { "Assets/Scenes/MainScene.unity" }; // Adjust scene path

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenes;
        buildPlayerOptions.locationPathName = "build/StandaloneWindows64/CounterFlame.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;

        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}