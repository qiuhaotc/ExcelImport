<%@ Page Title="" Language="C#" MasterPageFile="~/Excel/BootStrap.Master" AutoEventWireup="true" CodeBehind="StepZero.aspx.cs" Inherits="WebMain.Excel.StepZero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    选择Excel文件 配置字段信息
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h1>选择Excel文件-配置字段信息</h1>
    <hr />
    <form runat="server" id="form1">
    <div class="form-group">
        <h3>
            导入Excel<asp:LinkButton ID="UploadFile" runat="server" OnClick="UploadExcel">
                <button type="button" class="btn btn-sm btn-success" >
                下一步</button>
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
</asp:Content>
