using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Light))]
public class WorldLight : MonoBehaviour
{
    private Light light;//灯
    [SerializeField]
    private WorldTime worldTime;//用来接受时间的容器
    [SerializeField]
    private Gradient gradient;

    private void Awake()
    {
        light = GetComponent<Light>();//获取当前物体TMP_Text组件
        worldTime.WorldTimeChanged += OnWorldTimeChanged;//订阅时间，当事件WorldTimeChanged发生时，执行OnWorldTimeChanged函数
    }

    private void OnDestroy()
    {
        worldTime.WorldTimeChanged -= OnWorldTimeChanged;
    }

    private void OnWorldTimeChanged(object sender, TimeSpan time)
    {
        light.color = gradient.Evaluate(PercentOfDay(time));//转换颜色
    }

    private float PercentOfDay(TimeSpan time)//将时间转换为一天中的百分比
    {
        //TimeSpan包含的信息包括度过时间总数，所以需要取余
        return (float)time.TotalMinutes % WorldTimeConstants.MinutesInDay / WorldTimeConstants.MinutesInDay;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
