using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class QuizData {
    public int id;
    public string question;
    public string choice1;
    public string choice2;
    public string choice3;
    public string choice4;
    public int answer;
}

public class Connect : MonoBehaviour {

    private string url = "http://localhost/mysql.php";
    private UnityWebRequest _request;
    private bool _is_updated = false;
    /// <summary>データを送受信します</summary>
    public void startConnect( int idx ) {
        StartCoroutine( phpConnect( idx ) );
    }

    private IEnumerator phpConnect( int idx ) {

        //postとして渡すデータの作成
        WWWForm post_data = new WWWForm( );
        //AddFieldでfieldに値を格納                
        post_data.AddField( "id", idx );

        //リクエストの作成
        _request = UnityWebRequest.Post( url, post_data );

        //通信開始
        yield return _request.SendWebRequest( );

        if( _request.error != null ) {
            Debug.LogError( _request.error );
        }
        Debug.LogWarning( _request.downloadHandler.text );
        _is_updated = true;

    }

    private QuizData convartJsonToClient( UnityWebRequest json_request ) {
        QuizData data = new QuizData( );
        string json_data = json_request.downloadHandler.text;
        data = JsonUtility.FromJson<QuizData>( json_data );
        if( data == null ){
            return null;
        }

        return data;
    }

    /// <summary>DBのデータを取得※毎回データを更新します。呼び出しすぎ注意 </summary>
    public QuizData getNewQuizData( ) {
        //jsonで渡されるデータをQuizDataに変換して渡す
        return convartJsonToClient( _request );
    }

    /// <summary>
    /// データを取得出来たか否か
    /// </summary>
    /// <returns></returns>
    public bool isUpdated( ){
        return _is_updated;
    }
}