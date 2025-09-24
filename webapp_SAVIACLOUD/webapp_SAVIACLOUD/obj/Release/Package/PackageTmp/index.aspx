<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="webapp_SAVIACLOUD.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>SAVIA Cloud UdL v.0.03 -debug tool- REST API Oauth2  </title>
    <style type="text/css">
        .auto-style1 {
            width: 181px;
        }
        .auto-style2 {
            width: 80px;
        }
        .auto-style3 {
            width: 268px;
        }
        .auto-style4 {
            width: 181px;
            height: 31px;
        }
        .auto-style5 {
            width: 80px;
            height: 31px;
        }
        .auto-style6 {
            width: 268px;
            height: 31px;
        }
        .auto-style7 {
            width: 181px;
            height: 54px;
        }
        .auto-style8 {
            width: 80px;
            height: 54px;
        }
        .auto-style9 {
            width: 268px;
            height: 54px;
        }
    </style>
</head>
<body>
    <p>
        <br />
    </p>
    <form id="form1" runat="server">
    <p>
        &nbsp;<asp:Label ID="lbTITLE" runat="server" Text="Label"></asp:Label>
        </p>
    <div style="height: 244px; margin-top: 0px; margin-bottom: 56px;">
    
        <table style="width: 100%; margin-bottom: 0px; height: 313px;">
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label1" runat="server" Text="Codi Entidad Empresa"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="txCodiEntidad" runat="server">1</asp:TextBox>
                </td>
                <td class="auto-style3">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label2" runat="server" Text="Codi treballador"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="txCodieEmple" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style3">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Button ID="btGetDadesPers" runat="server" OnClick="btGetDadesPers_Click" Text="Consultar dades Personals" />
                </td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">
                    <asp:Label ID="Label3" runat="server" Text="Número versió dades laborals"></asp:Label>
                </td>
                <td class="auto-style8">
                    <asp:TextBox ID="txVersio" runat="server"></asp:TextBox>
                    <asp:CheckBox ID="ckDarrera" runat="server" Text="La més actual." Width="133px" />
                </td>
                <td class="auto-style9"></td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:Button ID="btGetDadesLab" runat="server" OnClick="btGetDadesLab_Click" Text="Consultar dades Laborals" />
                </td>
                <td class="auto-style5"></td>
                <td class="auto-style6"></td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="Label4" runat="server" Text="Departament 1r nivell"></asp:Label>
                </td>
                <td class="auto-style5">
                    <asp:TextBox ID="txDpot1" runat="server"></asp:TextBox>
                    </td>
                <td class="auto-style6">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="Label5" runat="server" Text="Departament 2on nivell"></asp:Label>
                </td>
                <td class="auto-style5">
                    <asp:TextBox ID="txDpot2" runat="server" OnTextChanged="txVersio1_TextChanged"></asp:TextBox>
                    </td>
                <td class="auto-style6">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:Button ID="btGetDptos" runat="server" OnClick="btGetDptos_Click" Text="Consultar departaments" />
                </td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
            </tr>
        </table>
        <asp:TextBox ID="txResult" runat="server" Height="259px" style="margin-top: 54px" TextMode="MultiLine" Width="715px"></asp:TextBox>
        <br />
        <br />
&nbsp;
        <br />
&nbsp;
        <br />
    
    </div>
    </form>
</body>
</html>
