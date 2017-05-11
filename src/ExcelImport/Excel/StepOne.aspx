<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StepOne.aspx.cs" Inherits="WebMain.Excel.StepOne"
    MasterPageFile="~/Excel/BootStrap.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    数据字段
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        配置Excel-配置数据库对应关系</h1>
    <hr />
    <form action="/excel/StepResult.aspx" method="post">
    <h3>
        数据字段
        <input type="submit" class="btn btn-sm btn-success" onclick="initData()" value="下一步" />
        <button type="button" class="btn btn-sm btn-warning" onclick="javascript:history.go(-1);/*location.href='/excel/stepzero.aspx';*/">
            上一步</button></h3>
    <table class="table table-bordered table-striped">
        <tr>
            <th style="width: 20%;">
                Excel字段
            </th>
            <th style="width: 20%;">
                数据库中字段
            </th>
            <th style="width: 20%;">
                是外键
            </th>
            <th style="width: 20%;">
                外键表
            </th>
            <th style="width: 20%;">
                对应外键表字段
            </th>
        </tr>
        <asp:Repeater ID="RepeaterItem" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <select class="form-control FromField" id="FromField_<%#Container.ItemIndex%>">
                            <option selected="selected" value=""></option>
                            <%
                                System.Collections.Generic.List<String> listFrom = GetList();

                                for (int i = 0; i < listFrom.Count; i++)
                                {
                                    if (NowIndex == i)
                                    {
                            %><option value="<%=listFrom[i]%>" selected="selected"><%=listFrom[i]%></option>
                            <%
}
                                    else
                                    {
                            %><option value="<%=listFrom[i]%>"><%=listFrom[i]%></option>
                            <%
                                }
                                }
                                NowIndex++;
                            %>
                        </select>
                    </td>
                    <td>
                        <select class="form-control ToField" id="ToField_<%#Container.ItemIndex%>">
                            <option selected="selected"></option>
                            <%
                                System.Collections.Generic.List<DAL.TableColomns> listToField = GetColomn();

                                for (int i = 0; i < listToField.Count; i++)
                                {
                            %><option value="<%=listToField[i].ColomnName%>"><%=listToField[i].ColomnName + "(" + listToField[i].Description + ")"%></option>
                            <%
}
                            %>
                        </select>
                    </td>
                    <td>
                        <select class=" form-control IsForeign" id="IsForeign_<%#Container.ItemIndex%>">
                            <option selected="selected" value="false">否</option>
                            <option value="true">是</option>
                        </select>
                    </td>
                    <td>
                        <select class="form-control ForeignTable" id="ForeignTable_<%#Container.ItemIndex%>"
                            onchange="getSelectToForeignField(<%#Container.ItemIndex%>);">
                            <option selected="selected" value=""></option>
                            <%
                                System.Collections.Generic.List<String> listForeignTable = GetTables();

                                for (int i = 0; i < listForeignTable.Count; i++)
                                {
                            %><option value="<%=listForeignTable[i]%>"><%=listForeignTable[i]%></option>
                            <%
}
                            %>
                        </select>
                    </td>
                    <td>
                        <select class="form-control ForeignField" id="ForeignField_<%#Container.ItemIndex%>">
                        </select>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr id="noData" runat="server" align="center" visible="<%#RepeaterItem.Items==null||RepeaterItem.Items.Count==0 %>">
                    <td colspan="10">
                        暂无数据
                    </td>
                </tr>
            </FooterTemplate>
        </asp:Repeater>
    </table>
    <input type="hidden" name="FromField" id="FromField" />
    <input type="hidden" name="ToField" id="ToField" />
    <input type="hidden" name="IsForeign" id="IsForeign" />
    <input type="hidden" name="ForeignField" id="ForeignField" />
    <input type="hidden" name="ForeignTable" id="ForeignTable" />
    </form>
    <script type="text/javascript">
        function getSelectToForeignField(index) {

            var tableName = $("#ForeignTable_" + index).val();

            if (tableName != "") {
                $.post("HandlerAjax.ashx?method=colomn&tablename=" + tableName, null, function (data) {

                    $("#ForeignField_" + index).empty();

                    for (var i = 0; i < data.length; i++) {
                        $("#ForeignField_" + index).append("<option value='" + data[i].ColomnName + "'>" + data[i].ColomnName + "(" + data[i].Description + ")" + "</option>");
                    }

                }, 'json');
            }
            else {
                $("#ForeignField_" + index).empty();
            }
        }

        function initData() {
            var $itemsFromField = $("select.FromField");
            var $itemsToField = $("select.ToField");
            var $itemsIsForeign = $("select.IsForeign");
            var $itemsForeignField = $("select.ForeignField");
            var $itemsForeignTable = $("select.ForeignTable");

            var value1 = "", value2 = "", value3 = "", value4 = "", value5 = "";

            for (var i = 0; i < $itemsFromField.length; i++) {
                if (value1 == "") {
                    value1 = ($itemsFromField.eq(i).val() == null ? "" : $itemsFromField.eq(i).val());
                    value2 = ($itemsToField.eq(i).val() == null ? "" : $itemsToField.eq(i).val());
                    value3 = ($itemsIsForeign.eq(i).val() == null ? "" : $itemsIsForeign.eq(i).val());
                    value4 = ($itemsForeignField.eq(i).val() == null ? "" : $itemsForeignField.eq(i).val());
                    value5 = ($itemsForeignTable.eq(i).val() == null ? "" : $itemsForeignTable.eq(i).val());
                }
                else {
                    value1 += "," + ($itemsFromField.eq(i).val() == null ? "" : $itemsFromField.eq(i).val());
                    value2 += "," + ($itemsToField.eq(i).val() == null ? "" : $itemsToField.eq(i).val());
                    value3 += "," + ($itemsIsForeign.eq(i).val() == null ? "" : $itemsIsForeign.eq(i).val());
                    value4 += "," + ($itemsForeignField.eq(i).val() == null ? "" : $itemsForeignField.eq(i).val());
                    value5 += "," + ($itemsForeignTable.eq(i).val() == null ? "" : $itemsForeignTable.eq(i).val());
                }
            }

            $("#FromField").val(value1);
            $("#ToField").val(value2);
            $("#IsForeign").val(value3);
            $("#ForeignField").val(value4);
            $("#ForeignTable").val(value5);

        }
    </script>
</asp:Content>