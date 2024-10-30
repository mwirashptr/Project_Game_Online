using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBg : MonoBehaviour
{
    public float loopSpeed;
    public Renderer bgRenderer;

    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(loopSpeed * Time.deltaTime, 0f);
    }
}
