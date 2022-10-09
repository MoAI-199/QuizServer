
<?php
    //Unityから取得
    $post_id = $_POST['id'];
    // データベースから値を取得
    $user = 'root';
    $pwd = 'moai0919';
    $host = 'localhost';
    $db_name = 'quizdata';
    $table_name = "quize_table";
    // dsnは以下の形でどのデータベースかを指定する
    // portが元は8889だが3306に変更
    $dsn = "mysql:charset=UTF8;host={$host};port=3306;dbname={$db_name};";

    // PDOでデータベースのコネクションを生成
    // 第一引数dsn, 第二引数user, 第三引数password
    // newでインスタンス化して使う
    $conn = new PDO($dsn, $user, $pwd);

    // $dsnでdbnameを指定しているのでdb名は省略できる
    // 右コマンドで実行されるのでそれを変数に格納する $conn->query('select * from テーブル名');
    $pst = $conn->query("select * from {$table_name}");

    // 実行結果を取得する(連想配列と配列データが被って出力される)
    // $result = $pst->fetchAll();
    // fetchAll()だけだと被ったデータがでるので連想配列のみに絞る
    // $result = $pst->fetchAll(PDO::FETCH_ASSOC);
    $result = $pst->fetchAll(PDO::FETCH_ASSOC);

    //連想配列をJSON型に変換(エンコード)する
    //第二引数一覧
    //改行とインデント：JSON_TRETY_PRINT
    //文字列をUnicodeにしない：JSON_UNESCAPED_UNICODE
    //「\」の削除：JSON_UNESCAPED_SLASHES
    //$json_result = json_encode( $result[$post_id], JSON_PRETTY_PRINT|JSON_UNESCAPED_UNICODE| JSON_UNESCAPED_SLASHES);
    $json_result = json_encode( $result[$post_id]);
    
    /*デバッグ用データ*/
    /*
    // preタグで囲んで見やすく出力する
    echo '<pre>';
    print_r($json_result);
    echo '</pre>';
    
    //jsonデコード
    $re_result = json_decode($json_result);

    echo '<pre>';
    print_r($re_result[0]);
    echo '</pre>';
    */

    //Unityに送信
    echo $json_result;

    // データ取得後は、コネクションを破棄する
    // PDOは以下は書かなくてもいいけど、プログラムによっては必要なので癖付ける
    $conn = null;
?>