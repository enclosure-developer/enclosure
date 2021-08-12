using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Camera_multi : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!isLocalPlayer)
        {
            enabled = false;
            var camera = transform.Find("Main Camera");
            camera.GetComponent<Camera>().enabled = false;
            // camera.GetComponent<AudioListener>().enabled = false;
        }
    }
}
