<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelImport.aspx.cs" Inherits="WebMain.Excel.ExcelImport" %>

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
    <h1>Excel导入-Excel字段管理</h1>
    <hr />
    <form runat="server" id="form1">
    <div class="form-group">
        <h3>
            导入Excel<asp:LinkButton ID="UploadFile" runat="server" OnClick="UploadExcel">
                <button type="button" class="btn btn-sm btn-primary" >
                生成SQL</button>
            </asp:LinkButton></h3>
        <label for="ExcelFile">
            Excel文件:</label>
        <asp:FileUpload ID="ExcelFile" runat="server" class="filestyle" />
    </div>
     <hr />
     <h3>参数配置<asp:LinkButton ID="SubmitBtn" runat="server" OnClick="SaveForm" >
                <button type="button" class="btn btn-sm btn-primary" >
                保存</button>
            </asp:LinkButton></h3>
    <div class="form-group">
        <label for="SqlConnectionString">
            数据库连接字符串:</label>
        <asp:TextBox ID="SqlConnectionString" runat="server" class="form-control" placeholder="数据库连接字符串"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="TableName">
            数据库表格名称:</label>
        <asp:TextBox ID="TableName" runat="server" class="form-control" placeholder="数据库表格名称"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="UploadPath">
            上传文件路径:</label>
        <asp:TextBox ID="UploadPath" runat="server" class="form-control" placeholder="上传文件路径"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="StartRow">
            表头起始行:</label>
        <asp:TextBox ID="StartRow" runat="server" class="form-control" placeholder="栏目信息起始行"></asp:TextBox>
    </div>
    </form>
    <h3>数据字段 <a href="/Excel/Edit.aspx" type="button" class="btn btn-primary btn-sm">添加</a></h3>
 
    <!-- 如果用户列表非空 -->
        <table class="table table-bordered table-striped">
            <tr>
                <th>字段中文名称</th>
                <%--<th>Excel中所在列</th>--%>
                <th>Excel中类型</th>
                <th>数据库中名称</th>
                <th>数据库中类型</th>
                <th>是否外键</th>
                <th>外键表名称</th>
                <th>外键表主键名称</th>
                <th>外键表对应字段</th>
                <th>操作</th>
            </tr>

            <asp:Repeater ID="RepeaterItem" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("FieldName")%>
                        </td>
                       <%-- <td>
                            <%#Eval("ExcelColomn")%>
                        </td>--%>
                        <td>
                            <%#Eval("ExcelFieldType")%>
                        </td>
                        <td>
                           <%#Eval("SQLField")%>
                        </td>
                        <td>
                            <%#Eval("SQLFieldType")%>
                        </td>
                        <td>
                            <%#Eval("IsForeignKey")%>
                        </td>
                        <td>
                            <%#Eval("ForeignTable")%>
                        </td>
                        <td>
                           <%#Eval("ForeignKey")%>
                        </td>
                        <td>
                            <%#Eval("LinkField")%>
                        </td>
                        <td>
                            <a href="/Excel/Edit.aspx?sqlfield=<%#Eval("SQLField")%>" type="button" class="btn btn-sm btn-warning">修改</a>
                            <a href="/Excel/Delete.aspx?sqlfield=<%#Eval("SQLField")%>" type="button" class="btn btn-sm btn-danger">删除</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr  id="noData" runat="server" align="center" visible="<%#RepeaterItem.Items==null||RepeaterItem.Items.Count==0 %>">
                        <td colspan="10">暂无数据</td>
                    </tr>
                </FooterTemplate>
            </asp:Repeater>
        </table>
</div>
 
<!-- jQuery文件。务必在bootstrap.min.js 之前引入 -->
<script src="//cdn.bootcss.com/jquery/1.11.3/jquery.min.js"></script>
 
<!-- 最新的 Bootstrap 核心 JavaScript 文件 -->
    <script src="//cdn.bootcss.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script src="/Script/bootstrap-filestyle.min.js" type="text/javascript"></script>
</body>
</html>
