<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CsvVeiculosExcecao._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-6">
            <asp:Label Text="Data Inicio" runat="server" />
            <asp:TextBox ID="dtInicio" runat="server" class="form-control" type="date" />
        </div>
        <div class="col-md-6 form-group">
            <asp:Label Text="Data Fim" runat="server" />
            <asp:TextBox ID="dtFim" runat="server" CssClass="form-control" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-4 form-group">
            <asp:Label Text="Usuario Responsavel" runat="server" />
            <asp:TextBox runat="server" ID="txtUsuario" CssClass="form-control" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-4 form-group">
            <asp:DropDownList runat="server" ID="dplTipoRelatorio" CssClass="form-control">
            </asp:DropDownList>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
        <asp:Button ID  ="btnGerarRelatorio" Text="Gerar Relatório" runat="server" OnClick="btnGerarRelatorio_Click"/>
            </div>
        <div class="col-md-4">
            <asp:FileUpload ID="arquivo" runat="server" />
            <asp:Button Text="Capturar arquivo" runat="server" ID="btnCapturarCsv" OnClick="btnCapturarCsv_Click" />
        </div>
    </div>
</asp:Content>
