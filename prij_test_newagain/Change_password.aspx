<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Change_password.aspx.cs" Inherits="prij_test_newagain.Change_password" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color:#32323D; font-family:verdana">

    <form id="form1" runat="server">

        <div style="width:500px; height:530px; margin-top:100px; margin:auto; background-color:#ffffff; text-align:center; border:medium; border-radius: 25px;">
            <div style="position:relative; top:125px">
                <h3 style="font-size:30px;color:#32323D">Change Password</h3>  
                <asp:PlaceHolder ID="resetPasswordPlaceHolder" runat="server">
                    <asp:TextBox ID="lastPassword" runat="server" placeholder="Enter your password" 
                                 Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 5px; font-family:verdana; border-color:#32323D" 
                                 TextMode="Password" />
                    <br /><br/>   
                    <asp:TextBox ID="newPassword" runat="server" placeholder="Enter your new password" 
                                 Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 5px; font-family:verdana; border-color:#32323D" 
                                 TextMode="Password" />
                    <br /><br/>
                    <asp:TextBox ID="newPasswordAgain" runat="server" placeholder="Re-enter new password" 
                                 Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 5px; font-family:verdana; border-color:#32323D" 
                                 TextMode="Password" />
                    <br /><br/>
                    <asp:Button ID="submitPassword" Text="Submit" runat="server" OnClick="Submit_New_Password_EventMethod" 
                                Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 10px; width:150px; background-color:#32323D; color:white; font-family:verdana" />
                </asp:PlaceHolder>
                <br/>
                <asp:Button ID="back_To_Login" Text="Log In" runat="server" OnClick="BackToLoginEventMethod" 
                            Style="font-size:10px; border:unset; background-color:white; color:#32323D" />
                <br />
                <asp:Label ID="Message" runat="server" />
                 <br/>
                <asp:Label ID="errorLabel" Text="" runat="server" Visible="false" 
                    Style="font-size:10px; font-family:verdana; color:red;" />
            </div>
        </div>
    </form>
</body>
</html>