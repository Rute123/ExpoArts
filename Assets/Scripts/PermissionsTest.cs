using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if PLATFORM_ANDROID
    using UnityEngine.Android;
#endif

public class PermissionsTest : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject dialog = null;
    public Text testText;
    
    void Start ()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
            dialog = new GameObject();
        }

        if (testText != null)
        {
            testText.text = $"{Application.persistentDataPath}\n XUXU {Application.persistentDataPath}";
        }
#endif
    }
}
