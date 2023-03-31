using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneScript : NetworkBehaviour
{
    public Text canvasStatusText;//UI Text
    public PlayerScript playerScript;

    [SyncVar(hook = nameof(OnStatusTextChanged))]
    public string statusText;//�ı��ֵ
    private void OnStatusTextChanged(string oldStr, string newStr)
    {
        canvasStatusText.text = statusText;//��ʾ�ı��ֵ
    }

    //������Ϣ
    public void ButtonSendPlayerMessage()
    {
        if(playerScript!=null)
        {
            playerScript.CmdSendPlayerMessage();
        }
    }

    public void ButtonChangeScene()//�ı䳡��
    {
        if(isServer)//�жϵ�ǰ�Ƿ�Ϊ�����
        {
            var scene = SceneManager.GetActiveScene();
            if(scene.name == "PlayScene")
            {
                NetworkManager.singleton.ServerChangeScene("OtherScene");
            }
            else if(scene.name == "OtherScene")
            {
                NetworkManager.singleton.ServerChangeScene("PlayScene");
            }
            //��ȡNetworkManager�ĵ�����Ȼ����÷���˳����л� scene.name == "PlayScene" ? "MenuScene" :
        }
        else
        {
            statusText = "You are not a server";
        }
    }
}
