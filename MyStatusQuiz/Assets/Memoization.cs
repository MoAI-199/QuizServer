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
        /// �����������ꍇ�̎��Ԍv��
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
        /// ���������Ȃ������ꍇ�̎��Ԍv��
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
        /// ���������Ă��郁�\�b�h
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int MemoFibonacci( int index ) {
            if( index < 0 )
                throw new System.ArgumentException( );
            //�L���b�V���̗L���ɂ���Ē��ɓ������̂�ς���
            int result = 0;
            //�L���b�V������Ă���΂����Ԃ�
            if( memoDic.ContainsKey( index ) )
                result = memoDic[ index ];
            //�������f���Ƀ��\�b�h������
            else {
                result = index < 2 ? index : MemoFibonacci( index - 1 ) + MemoFibonacci( index - 2 );
                memoDic[ index ] = result;
            }
            return result;
        }

        /// <summary>
        /// ���������Ă��Ȃ����\�b�h
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