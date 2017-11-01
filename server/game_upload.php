<?php
    // 获取用户名、关卡和时间
    $username = $_POST["username"];
    $level = $_POST["level"];
    $score = $_POST["score"];

    // 防止注入
    $username = mysqli_real_escape_string($data, $username);

    // 连接数据库
    $data = mysqli_connect("localhost", "root", "123456");

    if(mysqli_connect_errno())
    {
        echo mysqli_connect_error();
        return;
    }

    // 选择数据库
    mysqli_query($data, "set names utf8");
    mysqli_select_db($data, "game");

    // 插入数据
    $sql = "INSERT INTO rank VALUES(NULL, '$username', '$level', '$score')";
    mysqli_query($data, $sql);

    // 关闭数据库
    mysqli_close($data);

    echo 'upload -> ' . $username . " : " . $level . " : " . $score;
?>