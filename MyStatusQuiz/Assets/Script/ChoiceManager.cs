using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour {
    private class Choice {
        private DataManager _data_manger;
        private GameObject _go;
        private Text _text;
        private Button _btn;
        private int _btn_idx;

        public Choice( DataManager data_manger, GameObject go, int idx ) {
            _data_manger = data_manger;
            _go = go;
            _btn_idx = idx;
            _text = _go.GetComponentInChildren<Text>( );
            _btn = _go.GetComponent<Button>( );
            setText( "" );
            _btn.onClick.AddListener( doClick );
        }

        public void update( ){
            switch( _data_manger.getFase( ) ) {
                case COMMON.FASE.START:
                    setText( "開始" );
                    break;
                case COMMON.FASE.CONNECT:
                    break;
                case COMMON.FASE.CHOICE:
                    updateTextChoice( );
                    break;
                case COMMON.FASE.ANSWER:
                    updateTextAnswer( );
                    break;
            }
        }

        private void updateTextChoice( ){
            switch( _btn_idx ){
                case 1:
                    setText( _data_manger.getChoice1( ) );
                    break;
                case 2:
                    setText( _data_manger.getChoice2( ) );
                    break;
                case 3:
                    setText( _data_manger.getChoice3( ) );
                    break;
                case 4:
                    setText( _data_manger.getChoice4( ) );
                    break;
                default:
                    setText( "idx error" ); //idxが未設定です。 
                    break;
            }
        }

        private void updateTextAnswer( ){
            if( _data_manger.getUserAnswer( ) == _data_manger.getAnswer( ) ){
                setText( "正解です" );
            } else {
                setText( "不正解です" );
            }
        }

        private void doClick( ) {
            Debug.Log( "ボタンが入力されました" );
            clickAction( );
        }

        /// <summary>
        /// フェーズの切り替えを行う
        /// </summary>
        private void clickAction( ){
            _data_manger.doNextFase( );
            switch( _data_manger.getFase( ) ) {
                case COMMON.FASE.START:
                    break;
                case COMMON.FASE.CONNECT:
                    break;
                case COMMON.FASE.CHOICE:
                    _data_manger.setUserAnswer( _btn_idx );
                    break;
                case COMMON.FASE.ANSWER:
                    break;
            }
        }
        private void setText( string str ){
            _text.text = str;
        }
    }

    [ SerializeField ] private GameObject _button1;
    [ SerializeField ] private GameObject _button2;
    [ SerializeField ] private GameObject _button3;
    [ SerializeField ] private GameObject _button4;
    [ SerializeField ] private DataManager _data_manger;

    private List<Choice> _choice_list = new List<Choice>( );
    private void Start( ) {
        initButton( );
    }

    private void Update( ) {
        foreach( var choice in _choice_list ) {
            choice.update( );
        }
        if( _data_manger.getNowScene( ) == COMMON.SCENE.TITLE ) {
            initButton( );
            initButton( );
        }
    }

    private void initButton( ) {
        _choice_list.Clear( );
        _choice_list.Add( new Choice( _data_manger, _button1, 1 ) );
        _choice_list.Add( new Choice( _data_manger, _button2, 2 ) );
        _choice_list.Add( new Choice( _data_manger, _button3, 3 ) );
        _choice_list.Add( new Choice( _data_manger, _button4, 4 ) );
    }
}

