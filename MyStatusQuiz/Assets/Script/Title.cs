using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour {
    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _call;
    [SerializeField] private GameObject _bg;
    [SerializeField] private DataManager _data_manager;

    void Start( ) {
        initTitleScene( );
    }

    void Update( ) {
        
        switch( _data_manager.getNowScene( ) ) {
            case COMMON.SCENE.TITLE:
                initTitleScene( );
                if( Input.GetMouseButtonDown( 0 ) ) {
                    _data_manager.doNextScene( );
                }
                break;
            case COMMON.SCENE.GAME:
                hideScene( );
                break;
            case COMMON.SCENE.RESULT:
                intiResultScene( );
                if( Input.GetMouseButtonDown( 0 ) ) {
                    _data_manager.doNextScene( );
                }
                break;
        }
    }
    private void initTitleScene( ) {
        _bg.SetActive( true );
        _text.SetActive( true );
        _call.SetActive( true );
        _text.GetComponent<Text>( ).text = "Title";
        _call.GetComponent<Text>( ).text = "Screen Touch";
    }
    private void intiResultScene( ) {
        _bg.SetActive( true );
        _text.SetActive( true );
        _call.SetActive( true );
        _text.GetComponent<Text>().text = "Thenky for Playing!";
        _call.GetComponent<Text>( ).text = "Screen Touch";

    }

    private void hideScene( ) {
        _bg.SetActive( false );
        _text.SetActive( false );
        _call.SetActive( false );
    }
}
