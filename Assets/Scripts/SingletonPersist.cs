using UnityEngine;
using System.Collections;

public class SingletonPersist : MonoBehaviour {

    private static SingletonPersist instance;

    public static SingletonPersist GetInstance() {
        return instance;
    }

	// Use this for initialization
	void Awake () {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

}
