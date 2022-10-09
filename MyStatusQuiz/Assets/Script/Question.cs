using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;
public class Question : MonoBehaviour {
    [SerializeField] DataManager _data_manager;

    Text text = null;
    string _show_text;
    int _now_idx;
    private void Start( ) {
        text = this.GetComponent<Text>( );
    }

    private void Update( ) {
        reset( );
        updateText( );
    }

    //��肪�ς�������ɌĂ΂�鏈���B�ŏ��͍X�V���Ȃ�
    void updateText( ) {
        switch( _data_manager.getFase() ) {
            case COMMON.FASE.START:
                setText( "���ł��B" );
                break;
            case COMMON.FASE.CONNECT:
                //�ʐM���钆�ɕ\�����������Ƃ�����Ώ������A��P�t���[�������Ă΂��B9/1����
                break;
            case COMMON.FASE.CHOICE:
                setText( _data_manager.getQuestion( ) );
                break;
            case COMMON.FASE.ANSWER:
                if( _data_manager.getUserAnswer( ) == _data_manager.getAnswer( ) ){
                    setText( "������ˁI��������I�I" );
                } else {
                    setText( "�c�O�B�s��������E�E�E" );
                }
                break;
        }
    }

    private void reset( ){
        if( _data_manager.getNowScene( ) == COMMON.SCENE.RESULT ){
            setText( "���ł��B" );
        }
    }
    private void setText( string str ) {
        text.text = str;
    }
}
