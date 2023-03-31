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
    public string statusText;//改变的值
    private void OnStatusTextChanged(string oldStr, string newStr)
    {
        canvasStatusText.text = statusText;//显示改变的值
    }

    //发送信息
    public void ButtonSendPlayerMessage()
    {
        if(playerScript!=null)
        {
            playerScript.CmdSendPlayerMessage();
        }
    }

    public void ButtonChangeScene()//改变场景
    {
        if(isServer)//判断当前是否为服务端
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
            //获取NetworkManager的单例，然后调用服务端场景切换 scene.name == "PlayScene" ? "MenuScene" :
        }
        else
        {
            statusText = "You are not a server";
        }
    }
}
