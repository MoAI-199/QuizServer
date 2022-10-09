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

    //問題が変わった時に呼ばれる処理。最初は更新しない
    void updateText( ) {
        switch( _data_manager.getFase() ) {
            case COMMON.FASE.START:
                setText( "問題です。" );
                break;
            case COMMON.FASE.CONNECT:
                //通信する中に表示したいことがあれば書くが、約１フレームしか呼ばれん。9/1現在
                break;
            case COMMON.FASE.CHOICE:
                setText( _data_manager.getQuestion( ) );
                break;
            case COMMON.FASE.ANSWER:
                if( _data_manager.getUserAnswer( ) == _data_manager.getAnswer( ) ){
                    setText( "やったね！正解だよ！！" );
                } else {
                    setText( "残念。不正解だよ・・・" );
                }
                break;
        }
    }

    private void reset( ){
        if( _data_manager.getNowScene( ) == COMMON.SCENE.RESULT ){
            setText( "問題です。" );
        }
    }
    private void setText( string str ) {
        text.text = str;
    }
}
