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
    /// <summary>�f�[�^�𑗎�M���܂�</summary>
    public void startConnect( int idx ) {
        StartCoroutine( phpConnect( idx ) );
    }

    private IEnumerator phpConnect( int idx ) {

        //post�Ƃ��ēn���f�[�^�̍쐬
        WWWForm post_data = new WWWForm( );
        //AddField��field�ɒl���i�[                
        post_data.AddField( "id", idx );

        //���N�G�X�g�̍쐬
        _request = UnityWebRequest.Post( url, post_data );

        //�ʐM�J�n
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

    /// <summary>DB�̃f�[�^���擾������f�[�^���X�V���܂��B�Ăяo���������� </summary>
    public QuizData getNewQuizData( ) {
        //json�œn�����f�[�^��QuizData�ɕϊ����ēn��
        return convartJsonToClient( _request );
    }

    /// <summary>
    /// �f�[�^���擾�o�������ۂ�
    /// </summary>
    /// <returns></returns>
    public bool isUpdated( ){
        return _is_updated;
    }
}