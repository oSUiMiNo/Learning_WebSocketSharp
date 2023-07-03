using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WebSocketSharp;

public class ClientManager : MonoBehaviour
{
    WebSocket ws;

    public TextMeshProUGUI chatText;
    public TMP_InputField messageInput;
    public Button sendButton;

    //サーバへ、メッセージを送信する
    public void SendText()
    {
        ws.Send(messageInput.text);
    }

    //サーバから受け取ったメッセージを、ChatTextに表示する
    public void RecvText(string text)
    {
        chatText.text += (text + "\n");
    }
    //サーバの接続が切れたときのメッセージを、ChatTextに表示する
    public void RecvClose()
    {
        chatText.text = ("Close.");
    }

    void Start()
    {
        ///<summary>
        /// 接続処理。接続先サーバと、ポート番号を指定する
        /// サーバーはコマンドプロンプトの「ipconfig」とかで確認して、
        /// ポート番号は、サーバー用のクラスで指定した任意の数字
        /// </summary>
        //ws = new WebSocket("ws://10.70.13.200:12345/"); //多分家のwifi
        //ws = new WebSocket("ws://10.11.183.249:12345/");  //ガストのwifi 数回繋ぎなおすと変わるっぽい。
        ws = new WebSocket("ws://localhost:12345/");  //ローカル ( 自PC内 ) のサーバーとやり取りする時
        ws.Connect();

        //送信ボタンが押されたときに実行する処理「SendText」を登録する
        sendButton.onClick.AddListener(SendText);
        //サーバからメッセージを受信したときに実行する処理「RecvText」を登録する
        ws.OnMessage += (sender, e) => RecvText(e.Data);
        //サーバとの接続が切れたときに実行する処理「RecvClose」を登録する
        ws.OnClose += (sender, e) => RecvClose();
    }
}

