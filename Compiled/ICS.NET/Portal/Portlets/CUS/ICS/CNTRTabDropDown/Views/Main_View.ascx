<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Main_View.ascx.cs" Inherits="CNTRTabDropDown.Main_View" %>

<div class="pSection">
    <div class="panel panel-default">
        <div class="panel-heading">Tab Drop Down Settings</div>
        <div class="panel-body form-horizontal">
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="cbxEnable" runat="server" /> 
                            <b>Enable Tab Drop Down?</b>
                        </label>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="cbxPages" runat="server" /> 
                            <b>Display Pages in Drop Down?</b>
                        </label>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="cbxSubSections" runat="server" /> 
                            <b>Display Sub Sections in Drop Down?</b>
                        </label>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="cbxSubPagesPortlets" runat="server" /> 
                            <b>Display Sub Pages and Portlets in a second tier menu structure?</b>
                        </label>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="cbxAlphaOrder" runat="server" /> 
                            <b>Display Drop Down Elements in Alphabetical Order?</b>
                            <br />
                            (Instead of the order the elements appear in the sidebar)
                        </label>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <asp:Button ID="btnSave" runat="server" Text="Save Settings" OnClick="btnSave_Click" AutoPostBack="true" CssClass="btn btn-primary"/>
                </div>
            </div>
        </div>
    </div>

    <div class="panel panel-default">
        <div class="panel-heading">Tab Names and Identifying Values</div>
        <div class="panel-body">
            <div class="alert alert-warning">
                <b>This is a listing of all the tabs in your portal, except for the Home and My Pages tabs.</b> 
                If the display name is not equal to the identifier, then a drop down will not display for that tab.
            </div>
            <table class="table table-striped">
                <thead>
                    <th>Display Name</th>
                    <th>Identifier</th>
                </thead>
                <asp:Literal ID="ltrlTabNames" runat="server" />
            </table>
        </div>
    </div>
</div>
