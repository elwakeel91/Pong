using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    static bool isCreated = false;

    // Use this for initialization
    void Awake()
    {
        if (isCreated)
            Destroy(gameObject);

        if (!isCreated)
        {
            isCreated = true;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
