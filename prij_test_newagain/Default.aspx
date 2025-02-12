<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="prij_test_newagain.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color:#32323D; font-family:verdana">
    <!-- username: webuser
        password: webuserniv-->
    <form id="form1" runat="server">

            <div style =" width:500px; height:500px; margin-top:100px;margin:auto; background-color:#ffffff; text-align:center; border:medium; border-radius: 25px;">
                 <div style="position:relative;top:125px">
                     <h3 style="font-size:30px;color:#32323D">Login</h3>                     
                        <asp:TextBox ID="userEmailTextBox" runat="server" placeholder="Enter user email here" Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 5px; font-family:verdana; border-color:#32323D" />
                        <br /><br/>
                         <asp:TextBox ID="passWordTextBox" runat="server" placeholder="Enter password here" Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 5px;font-family:verdana; border-color:#32323D" TextMode="Password" />                         
                         <br /><br/>
                         <asp:Button ID="logInButton" Text="Log In" runat="server" OnClick="LogInEventMethod" Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 10px;width:150px; background-color:#32323D; color:white; font-family:verdana" />  
                         <br/>
                         <asp:Button ID="forgotPassword" Text="Forgot password?" runat="server" OnClick="ForgotPasswordEventMethod" Style="font-size:10px; border:unset;background-color:white;color:#32323D " />
                         <br />
                         <asp:Button ID="registerButton" Text="Register" runat="server" OnClick="RegisterEventMethod" Style="font-size:10px; border:unset;background-color:white;color:#32323D " />
                         <br /><br/>
                         <asp:label ID="Message" runat="server" />                     
                 </div>
           </div>
    </form>
</body>
</html>
