using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerScript : NetworkBehaviour
{
    public GameObject floatingInfo;//���ͷ����UI
    public TextMesh nameText;//UI��TextMesh
    private Material playerMaterialClone;//��ɫ�Ĳ���

    public GameObject[] toolsArray;//��������

    private int currentTool;//��ǰʹ�õĹ����±�

    private SceneScript sceneScript;//ָ�������ű���һ��������ʾ��ɫUI�Ľű�


    //-------------------------------��ֵ���ı�ʱ����õķ���-------------------------------
    [SyncVar(hook = nameof(OnPlayerNameChanged))]//������ֵͬ�����������
    private string playerName;//����ֵ���޸�ʱ�������OnPlayerNameChanged����

    [SyncVar(hook = nameof(OnPlayerColorChanged))]//������ֵͬ�����������
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
    //-------------------------------�ı�ֵ�����ݵķ���------------------------------------
    [Command]//�ı�ֵ������
    private void CmdSetUpPlayer(string name,Color color)
    {
        playerName = name;
        playerColor = color;
        sceneScript.statusText = $"{playerName}joined";
    }
    [Command]//�ı�ֵ������
    public void CmdSendPlayerMessage()
    {
        sceneScript.statusText = $"{playerName}say hello{Random.Range(1,99)}";
    }
    [Command]
    private void CmdActiveTool(int index)//�����
    {
        //����ֵcurrentToolSynced���޸�ʱ���ᴥ��ͬ����OnToolChanged��������
        currentToolSynced = index;//�ı䵱ǰ�Ĺ���
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

    public override void OnStartLocalPlayer()//����ҽ���ͻ���ʱ
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
