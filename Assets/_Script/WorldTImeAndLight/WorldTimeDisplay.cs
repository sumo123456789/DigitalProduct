using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class WorldTimeDisplay : MonoBehaviour
{
    [SerializeField]
    private WorldTime worldTime;//用来接受时间的容器
    private TMP_Text text;//文本
    private void Awake()
    {
        text = GetComponent<TMP_Text>();//获取当前物体TMP_Text组件
        worldTime.WorldTimeChanged += OnWorldTimeChanged;//订阅时间，当事件WorldTimeChanged发生时，执行OnWorldTimeChanged函数
    }
    private void OnDestroy()
    {
        worldTime.WorldTimeChanged -= OnWorldTimeChanged;
    }
    private void OnWorldTimeChanged(object sender, TimeSpan time)
    {
        text.SetText(time.ToString());//显示时间
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
