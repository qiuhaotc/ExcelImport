<%@ Page Title="" Language="C#" MasterPageFile="~/Excel/BootStrap.Master" AutoEventWireup="true" CodeBehind="StepResult.aspx.cs" Inherits="WebMain.Excel.StepResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    导出结果
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h1>
            Excel导入-生成SQL情况</h1>
        <hr />
          <h3>
        生成SQL情况 <button type="button" class="btn btn-sm btn-warning" onclick="javascript:history.go(-1);">
                上一步</button></h3>  
        <div class="form-group">
            <input class="form-control"  runat="server" id="sqlConnection" />
        </div>
        <div class="form-group">
            <textarea class="form-control" rows="40" runat="server" id="SQLResult"></textarea>
        </div>
</asp:Content>
