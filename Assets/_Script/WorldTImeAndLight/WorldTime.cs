using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTime : MonoBehaviour
{
    public event EventHandler<TimeSpan> WorldTimeChanged;//事件，世界改变
    [SerializeField]
    private float dayLength;//用秒数表达一天的时长
    private TimeSpan curTime;//当前时间
    private float minuteLength => dayLength / WorldTimeConstants.MinutesInDay;//一分钟的时长
    private IEnumerator AddMin()
    {
        curTime += TimeSpan.FromMinutes(1);//当前时间加1分钟
        WorldTimeChanged?.Invoke(this, curTime);//事件，（事件的发送者，事件的信息），然后在WorldTImeDisplay中订阅该事件
        yield return new WaitForSeconds(minuteLength);//等待时间的协程
        StartCoroutine(routine: AddMin());
    }
    void Start()
    {
        StartCoroutine(routine: AddMin()); 
    }
    void Update()
    {
    }
}
