<%@ Page Language="C#" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WebMain.Excel.Edit" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Excel字段编辑</title>
    <!-- 新 Bootstrap 核心 CSS 文件 -->
    <link rel="stylesheet" href="//cdn.bootcss.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <link href="/CSS/bootstrap-select.min.css" rel="stylesheet" type="text/css" />
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
            Excel字段编辑</h1>
        <hr />
        <form runat="server" id="form1">
        <div class="form-group">
            <label for="ExcelColomn">
                <%--Excel中所在列--%>字段排序值:</label>
            <asp:TextBox ID="ExcelColomn" runat="server" class="form-control" placeholder="字段排序值"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="FieldName">
                字段中文名称:</label>
            <asp:TextBox ID="FieldName" runat="server" class="form-control" placeholder="字段中文名称"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="ExcelFieldType">
                Excel中类型:</label>
            <asp:DropDownList ID="ExcelFieldType" runat="server" class="selectpicker show-tick form-control">
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="SQLField">
                数据库中名称:</label>
            <asp:TextBox ID="SQLField" runat="server" class="form-control" placeholder="数据库中名称"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="SQLFieldType">
               数据库中类型:</label>
               <asp:DropDownList ID="SQLFieldType" runat="server" class="selectpicker show-tick form-control">
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="IsForeignKey">
                是否外键:</label>
            <asp:DropDownList ID="IsForeignKey" runat="server" class="selectpicker show-tick form-control">
                <asp:ListItem  Value="0" Text="否"/>
                <asp:ListItem  Value="1" Text="是"/>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="ForeignTable">
                外键表名称:</label>
            <asp:TextBox ID="ForeignTable" runat="server" class="form-control" placeholder="外键表名称"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="ForeignKey">
                外键表主键名称:</label>
            <asp:TextBox ID="ForeignKey" runat="server" class="form-control" placeholder="外键表主键名称"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="LinkField">
                外键表对应字段:</label>
            <asp:TextBox ID="LinkField" runat="server" class="form-control" placeholder="外键表对应字段"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:LinkButton ID="SubmitBtn" runat="server" OnClick="SaveForm" >
                <button type="button" class="btn btn-sm btn-success" >
                提交</button>
            </asp:LinkButton>
           <a type="button" class="btn btn-sm btn-warning" href="/Excel/ExcelImport.aspx">
                返回</a>
        </div>
        </form>
    </div>
    <!-- jQuery文件。务必在bootstrap.min.js 之前引入 -->
    <script src="//cdn.bootcss.com/jquery/1.11.3/jquery.min.js"></script>
    <!-- 最新的 Bootstrap 核心 JavaScript 文件 -->
    <script src="//cdn.bootcss.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script src="/Script/bootstrap-select.min.js" type="text/javascript"></script>
</body>
</html>