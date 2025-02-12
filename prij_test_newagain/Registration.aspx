<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="prij_test_newagain.Registration" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
</head>
<body style="background-color:#32323D; font-family:verdana">
    <form id="form1" runat="server">
        <div style="width:500px; height:580px; margin-top:100px; margin:auto; background-color:#ffffff; text-align:center; border:medium; border-radius: 25px;">
            <div style="position:relative; top:56px; left:0px;">
                <h3 style="font-size:30px; color:#32323D">Register</h3>
                
                <asp:TextBox ID="firstNameTextBox" runat="server" placeholder="Enter your name" 
                    Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 5px; font-family:verdana; border-color:#32323D" />
                <br /><br />
                
                <asp:TextBox ID="lastNameTextBox" runat="server" placeholder="Enter your last name" 
                    Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 5px; font-family:verdana; border-color:#32323D" />
                <br /><br />
                
                <asp:TextBox ID="userNameTextBox" runat="server" placeholder="Enter new username" 
                    Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 5px; font-family:verdana; border-color:#32323D" />
                <br /><br />
                
                <asp:TextBox ID="passWordTextBox" runat="server" placeholder="Enter new password" 
                    Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 5px; font-family:verdana; border-color:#32323D" TextMode="Password" />
                <br /><br />
                
                <asp:TextBox ID="rePasswordTextBox" runat="server" placeholder="Re-enter password" 
                    Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 5px; font-family:verdana; border-color:#32323D" TextMode="Password" />
                <br /><br />
                
                <asp:TextBox ID="emailTextBox" runat="server" placeholder="Enter your email" 
                    Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 5px; font-family:verdana; border-color:#32323D" TextMode="SingleLine" />
                <br /><br />
                
                <asp:Button ID="registerButton" Text="Submit" runat="server" OnClick="RegisterEventMethod" 
                    Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 10px; width:125px; background-color:#32323D; color:white; font-family:verdana" />
                <br />
                
                <asp:Button ID="logInButton" Text="Log In" runat="server" OnClick="BackToLogInEventMethod" 
                    Style="font-size:11px; border:unset; background-color:white; color:#32323D;" />
                <br />
                
                <asp:Label ID="errorLabel" Text="" runat="server" Visible="false" 
                    Style="font-size:10px; font-family:verdana; color:red;" />
            </div>
        </div>
    </form>
</body>
</html>