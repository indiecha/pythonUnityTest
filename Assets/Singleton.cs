using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
             DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 하려면 주석 해제
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 현재 게임 오브젝트 파괴
        }
    }
}