<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="WebMain.Excel.Result" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
 <title>Excel字段管理</title>
 
    <!-- 新 Bootstrap 核心 CSS 文件 -->
 <link rel="stylesheet" href="//cdn.bootcss.com/bootstrap/3.3.5/css/bootstrap.min.css">
 
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="//cdn.bootcss.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src="//cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <div class="container">
        <h1>
            Excel导入-生成SQL情况</h1>
             <button type="button" class="btn btn-sm btn-success" onclick="location.href='/Excel/ExcelImport.aspx'">
                返回</button>
        <hr />
        <div class="form-group">
            <input class="form-control"  runat="server" id="sqlConnection" />
        </div>
        <div class="form-group">
            <textarea class="form-control" rows="40" runat="server" id="SQLResult"></textarea>
        </div>
    </div>
</body>
</html>
