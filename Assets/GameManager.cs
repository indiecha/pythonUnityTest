using System.Diagnostics;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // GameManager의 고유한 변수나 함수들을 여기에 작성합니다.
    public int score = 0;
    public byte[] bytes = null;

    public AndroidJavaObject PluginInstance { get; private set; }

    public void AddScore(int amount)
    {
        score += amount;
        UnityEngine.Debug.Log("Score: " + score);
    }

    // Awake()를 오버라이드해야 한다면 base.Awake()를 호출하는 것을 잊지 마세요.
    protected override void Awake()
    {
        base.Awake(); // 부모 클래스의 Awake() 호출이 중요합니다.
        // GameManager 초기화 코드 추가
        UnityEngine.Debug.Log("GameManager initialized!");

        var pluginClass = new AndroidJavaClass("com.example.unitymodule.unityModule");

        PluginInstance = pluginClass.CallStatic<AndroidJavaObject>("instance");

        PluginInstance?.Call("showToast", "!!!TEST!!!");

        //bytes = PluginInstance?.Get<byte[]>("tex_bytes");
    }
}