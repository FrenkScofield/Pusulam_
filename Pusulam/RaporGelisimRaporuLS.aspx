﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RaporGelisimRaporuLS.aspx.cs" Inherits="Pusulam.RaporGelisimRaporuLS" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sınav Kağıdı Önizleme</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ReportToolbar ID="ReportToolbar1" runat="server" ReportViewerID="ReportViewer1" ShowDefaultButtons="False">
            <Items>
                <dx:ReportToolbarButton ItemKind="Search" />
                <dx:ReportToolbarSeparator />
                <dx:ReportToolbarButton ItemKind="PrintReport" />
                <dx:ReportToolbarButton ItemKind="PrintPage" />
                <dx:ReportToolbarSeparator />
                <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                <dx:ReportToolbarLabel ItemKind="PageLabel" />
                <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                </dx:ReportToolbarComboBox>
                <dx:ReportToolbarLabel ItemKind="OfLabel" />
                <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                <dx:ReportToolbarButton ItemKind="NextPage" />
                <dx:ReportToolbarButton ItemKind="LastPage" />
                <dx:ReportToolbarSeparator />
                <dx:ReportToolbarButton ItemKind="SaveToDisk" />
                <dx:ReportToolbarButton ItemKind="SaveToWindow" />
                <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                    <Elements>
                        <dx:ListElement Value="pdf" />
                        <dx:ListElement Value="xls" />
                        <dx:ListElement Value="xlsx" />
                        <dx:ListElement Value="rtf" />
                        <dx:ListElement Value="mht" />
                        <dx:ListElement Value="html" />
                        <dx:ListElement Value="txt" />
                        <dx:ListElement Value="csv" />
                        <dx:ListElement Value="png" />
                    </Elements>
                </dx:ReportToolbarComboBox>
            </Items>
            <Styles>
                <LabelStyle>
                <Margins MarginLeft="3px" MarginRight="3px" />
                </LabelStyle>
            </Styles>
        </dx:ReportToolbar>
        <dx:ReportViewer ID="ReportViewer1" runat="server">
        </dx:ReportViewer>

        </div>
    </form>
</body>
</html>

