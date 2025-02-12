<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forgot_password.aspx.cs" Inherits="prij_test_newagain.Forgot_password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color:#32323D; font-family:verdana">

    <form id="form1" runat="server">

            <div style =" width:500px; height:430px; margin-top:100px;margin:auto; background-color:#ffffff; text-align:center; border:medium; border-radius: 25px;">
                 <div style="position:relative;top:125px">
                     <h3 style="font-size:30px;color:#32323D">Reset Password</h3>  
                        <asp:PlaceHolder ID="TextBoxPlaceHolder" runat="server">
                        <asp:TextBox ID="mailTextBox" runat="server" placeholder="Enter your mail here" Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 5px; font-family:verdana; border-color:#32323D" />
                        </asp:PlaceHolder>             
                        <asp:TextBox ID="verifyTextBox" runat="server" placeholder="Enter your code" Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 5px; font-family:verdana; border-color:#32323D" Visible="false" />
                        <br /><br/> 
                        <asp:PlaceHolder ID="ButtonPlaceHolder" runat="server">
                        <asp:Button ID="forgot_Button" Text="Send" runat="server" OnClick="Forgot_EventMethod" Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 10px;width:150px; background-color:#32323D; color:white; font-family:verdana" />  
                        </asp:PlaceHolder> 
                        <asp:Button ID="verifyButton" Text="Verify" runat="server" OnClick="Verify_EventMethod" Style="border: 1px solid #c8c8c8; border-radius: 10px; padding: 10px;width:150px; background-color:#32323D; color:white; font-family:verdana" Visible="false" />                               
                        <br/>
                        <asp:Button ID="back_To_Login" Text="Log In" runat="server" OnClick="BackToLoginEventMethod" Style="font-size:10px; border:unset;background-color:white;color:#32323D " />
                        <br />
                        <asp:label ID="Message" runat="server" />                     
                 </div>
           </div>
    </form>
</body>
</html>
