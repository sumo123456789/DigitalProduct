using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Light))]
public class WorldLight : MonoBehaviour
{
    private Light light;//��
    [SerializeField]
    private WorldTime worldTime;//��������ʱ�������
    [SerializeField]
    private Gradient gradient;

    private void Awake()
    {
        light = GetComponent<Light>();//��ȡ��ǰ����TMP_Text���
        worldTime.WorldTimeChanged += OnWorldTimeChanged;//����ʱ�䣬���¼�WorldTimeChanged����ʱ��ִ��OnWorldTimeChanged����
    }

    private void OnDestroy()
    {
        worldTime.WorldTimeChanged -= OnWorldTimeChanged;
    }

    private void OnWorldTimeChanged(object sender, TimeSpan time)
    {
        light.color = gradient.Evaluate(PercentOfDay(time));//ת����ɫ
    }

    private float PercentOfDay(TimeSpan time)//��ʱ��ת��Ϊһ���еİٷֱ�
    {
        //TimeSpan��������Ϣ�����ȹ�ʱ��������������Ҫȡ��
        return (float)time.TotalMinutes % WorldTimeConstants.MinutesInDay / WorldTimeConstants.MinutesInDay;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
