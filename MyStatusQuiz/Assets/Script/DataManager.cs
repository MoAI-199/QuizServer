using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour {
    [SerializeField] private Connect _connect;

    QuizData _data;
    int _now_question_idx = 0;
    int _user_answer = -1;
    int _answer_num = 0;
    bool _id_update = false;
    bool _do_next_fase = false;
    bool _do_next_scene = false;
    COMMON.FASE _fase = COMMON.FASE.START;
    COMMON.SCENE _scene = COMMON.SCENE.TITLE;

    private void Update( ) {
        updateFase( );
        updateNextFase( );
        updateNextScene( );
    }
    private void updateData( ) {
        _data = _connect.getNewQuizData( );
    }

    private void updateNextFase( ){
        if( !_do_next_fase ){
            return;
        }
        _do_next_fase = false;
        switch( _fase ) {
            case COMMON.FASE.START:
                setFase( COMMON.FASE.CONNECT );
                break;
            case COMMON.FASE.CONNECT:
                setFase( COMMON.FASE.CHOICE );
                break;
            case COMMON.FASE.CHOICE:
                setFase( COMMON.FASE.ANSWER );
                break;
            case COMMON.FASE.ANSWER:
                setFase( COMMON.FASE.START);
                break;
        }
    }
    private void updateFase( ){
        switch( _fase ) {
            case COMMON.FASE.START:
                if( _answer_num >= 3 ) {
                    doNextScene( );
                    _answer_num = 0;
                }
                break;
            case COMMON.FASE.CONNECT:
                _connect.startConnect( _now_question_idx );
                if( _connect.isUpdated( ) ) {
                    setFase( COMMON.FASE.CHOICE );
                    _id_update = true;
                }
                break;
            case COMMON.FASE.CHOICE:
                updateData( );
                if( _id_update ) {
                    _id_update = false;
                    _now_question_idx++;
                    _now_question_idx %= 3;//現状問題が３問なので、後にDBからMaxIndexを取得するようにする
                    _answer_num++;
                }
                break;
            case COMMON.FASE.ANSWER:
                
                break;
            default:
                break;
        }
    }
    private void updateNextScene( ){
        if( !_do_next_scene ){
            return;
        }
        _do_next_scene = false;
        switch( _scene ) {
            case COMMON.SCENE.TITLE:
                setScene( COMMON.SCENE.GAME );
                break;
            case COMMON.SCENE.GAME:
                setScene( COMMON.SCENE.RESULT );
                break;
            case COMMON.SCENE.RESULT:
                setScene( COMMON.SCENE.TITLE );
                setFase( COMMON.FASE.START );
                break;
            default:
                break;
        };
    }

    public int getId( ) {
        if( _data == null ) {
            return -1;
        }
        return _data.id;
    }

    public string getQuestion( ) {
        if( _data == null ){
            return "接続エラー";
        }
        return _data.question;
    }
    public string getChoice1( ) {
        if( _data == null ) {
            return "接続エラー";
        }
        return _data.choice1;
    }

    public string getChoice2( ) {
        if( _data == null ) {
            return "接続エラー";
        }
        return _data.choice2;
    }
    public string getChoice3( ) {
        if( _data == null ) {
            return "接続エラー";
        }
        return _data.choice3;
    }

    public string getChoice4( ) {
        if( _data == null ) {
            return "接続エラー";
        }
        return _data.choice4;
    }
    public int getAnswer( ) {
        if( _data == null ) {
            return -1;
        }
        return _data.answer;
    }
    public void doNextFase( ){
        _do_next_fase = true;
    }
    private void setFase( COMMON.FASE fase ) {
        _fase = fase;
    }
    public void doNextScene(){
        _do_next_scene = true;
    }

    public COMMON.FASE getFase( ) {
        return _fase;
    }
    public int getUserAnswer( ){
        return _user_answer;
    }
    public void setUserAnswer( int idx ){
        _user_answer = idx;
    }

    private void setScene( COMMON.SCENE scene ){
        _scene = scene;
    }

    public COMMON.SCENE getNowScene(){
        return _scene;
    }
}
