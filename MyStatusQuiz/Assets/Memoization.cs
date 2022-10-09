using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ConsoleApp6 {
    class Memoization : MonoBehaviour{
        private Dictionary<int, int> memoDic = new Dictionary<int, int>( );

        private void Start( ) {
            MemoTime( "Use Memo", 40 );
            NotMemoTime( "NO Memo", 40 );
        }


        /// <summary>
        /// メモ化した場合の時間計測
        /// </summary>
        /// <param name="title"></param>
        /// <param name="index"></param>
        public void MemoTime( string title, int index ) {
            Debug.Log( $"[{title}]" );
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch( );
            stopwatch.Start( );
            int result = MemoFibonacci( index );
            stopwatch.Stop( );
            Debug.Log( $"Index: {index}" );
            Debug.Log( $"Answer: {result}" );
            Debug.Log( $"Time: {stopwatch.Elapsed}" );
            Debug.Log( "" );
        }

        /// <summary>
        /// メモ化しなかった場合の時間計測
        /// </summary>
        /// <param name="title"></param>
        /// <param name="index"></param>
        public void NotMemoTime( string title, int index ) {
            Debug.Log( $"[{title}]" );
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch( );
            stopwatch.Start( );
            int result = NotMemoFibonacci( index );
            stopwatch.Stop( );
            Debug.Log( $"Index: {index}" );
            Debug.Log( $"Answer: {result}" );
            Debug.Log( $"Time: {stopwatch.Elapsed}" );
            Debug.Log( "" );
        }

        /// <summary>
        /// メモ化しているメソッド
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int MemoFibonacci( int index ) {
            if( index < 0 )
                throw new System.ArgumentException( );
            //キャッシュの有無によって中に入れるものを変える
            int result = 0;
            //キャッシュされていればそれを返す
            if( memoDic.ContainsKey( index ) )
                result = memoDic[ index ];
            //無けりゃ素直にメソッド動かす
            else {
                result = index < 2 ? index : MemoFibonacci( index - 1 ) + MemoFibonacci( index - 2 );
                memoDic[ index ] = result;
            }
            return result;
        }

        /// <summary>
        /// メモ化していないメソッド
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int NotMemoFibonacci( int index ) {
            if( index < 0 )
                throw new System.ArgumentException( );
            return index < 2 ? index : NotMemoFibonacci( index - 1 ) + NotMemoFibonacci( index - 2 );
        }
    }
}