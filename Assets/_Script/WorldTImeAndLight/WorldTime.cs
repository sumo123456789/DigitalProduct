using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTime : MonoBehaviour
{
    public event EventHandler<TimeSpan> WorldTimeChanged;//�¼�������ı�
    [SerializeField]
    private float dayLength;//���������һ���ʱ��
    private TimeSpan curTime;//��ǰʱ��
    private float minuteLength => dayLength / WorldTimeConstants.MinutesInDay;//һ���ӵ�ʱ��
    private IEnumerator AddMin()
    {
        curTime += TimeSpan.FromMinutes(1);//��ǰʱ���1����
        WorldTimeChanged?.Invoke(this, curTime);//�¼������¼��ķ����ߣ��¼�����Ϣ����Ȼ����WorldTImeDisplay�ж��ĸ��¼�
        yield return new WaitForSeconds(minuteLength);//�ȴ�ʱ���Э��
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
