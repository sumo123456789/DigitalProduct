using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class WorldTimeDisplay : MonoBehaviour
{
    [SerializeField]
    private WorldTime worldTime;//��������ʱ�������
    private TMP_Text text;//�ı�
    private void Awake()
    {
        text = GetComponent<TMP_Text>();//��ȡ��ǰ����TMP_Text���
        worldTime.WorldTimeChanged += OnWorldTimeChanged;//����ʱ�䣬���¼�WorldTimeChanged����ʱ��ִ��OnWorldTimeChanged����
    }
    private void OnDestroy()
    {
        worldTime.WorldTimeChanged -= OnWorldTimeChanged;
    }
    private void OnWorldTimeChanged(object sender, TimeSpan time)
    {
        text.SetText(time.ToString());//��ʾʱ��
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
