using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerScript : NetworkBehaviour
{
    public GameObject floatingInfo;//玩家头顶的UI
    public TextMesh nameText;//UI的TextMesh
    private Material playerMaterialClone;//角色的材质

    public GameObject[] toolsArray;//工具数组

    private int currentTool;//当前使用的工具下标

    private SceneScript sceneScript;//指定场景脚本，一个用于显示角色UI的脚本


    //-------------------------------当值被改变时会调用的方法-------------------------------
    [SyncVar(hook = nameof(OnPlayerNameChanged))]//将变量值同步给其他玩家
    private string playerName;//当该值被修改时，会调用OnPlayerNameChanged方法

    [SyncVar(hook = nameof(OnPlayerColorChanged))]//将变量值同步给其他玩家
    private Color playerColor;

    [SyncVar(hook = nameof(OnToolChanged))]
    private int currentToolSynced;
    private void OnToolChanged(int oldIndex, int newIndex)
    {
        if (0 < oldIndex && oldIndex < toolsArray.Length && toolsArray[oldIndex] != null) 
        {
            toolsArray[oldIndex].SetActive(false);
        }
        if (0 < newIndex && newIndex < toolsArray.Length && toolsArray[newIndex] != null)
        {
            toolsArray[newIndex].SetActive(true);
        }

    }
    private void OnPlayerNameChanged(string oldValue, string newValue)
    {
        nameText.text = newValue;
    }
    private void OnPlayerColorChanged(Color oldValue, Color newValue)
    {
        nameText.color = newValue;
    }
    //-------------------------------改变值的数据的方法------------------------------------
    [Command]//改变值的数据
    private void CmdSetUpPlayer(string name,Color color)
    {
        playerName = name;
        playerColor = color;
        sceneScript.statusText = $"{playerName}joined";
    }
    [Command]//改变值的数据
    public void CmdSendPlayerMessage()
    {
        sceneScript.statusText = $"{playerName}say hello{Random.Range(1,99)}";
    }
    [Command]
    private void CmdActiveTool(int index)//激活工具
    {
        //当数值currentToolSynced被修改时，会触发同步的OnToolChanged监听方法
        currentToolSynced = index;//改变当前的工具
    }

    public void Awake()
    {
        sceneScript = FindObjectOfType<SceneScript>();
        foreach (var item in toolsArray)
        {
            if(item != null)
            {
                item.SetActive(false);
            }
        }
    }

    public override void OnStartLocalPlayer()//当玩家进入客户端时
    {
        sceneScript.playerScript = this;
        base.OnStartLocalPlayer();
        var tempName = $"Player{Random.Range(100, 999)}";
        var tempColor = new Color(1, 1, 1);
        CmdSetUpPlayer(tempName, tempColor);
    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        var moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 10f;
        var moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 10f;
        floatingInfo.transform.rotation = Camera.main.transform.rotation;

        //transform.Rotate(0, 0, 0);
        transform.Translate(moveX, 0, moveZ);

        if(Input.GetKeyDown(KeyCode.Q))
        {
            currentTool++;
            if(currentTool > toolsArray.Length)
            {
                currentTool = 1;
            }
            CmdActiveTool(currentTool);
        }

    }
}
