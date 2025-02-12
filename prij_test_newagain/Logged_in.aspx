<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logged_in.aspx.cs" Inherits="prij_test_newagain.Logged_in" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Dashboard</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f9;
        }

        .navbar {
            background-color: #007BFF;
            color: white;
            padding: 1rem;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .navbar .title {
            font-size: 1.5rem;
            font-weight: bold;
        }

        .navbar button {
            background-color: white;
            color: #007BFF;
            border: none;
            padding: 0.5rem 1rem;
            border-radius: 5px;
            cursor: pointer;
        }

        .navbar button:hover {
            background-color: #0056b3;
            color: white;
        }

        .container {
            max-width: 1200px;
            margin: 2rem auto;
            padding: 1rem;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .actions {
            display: flex;
            justify-content: space-between;
            margin-bottom: 1.5rem;
        }

        .actions button {
            background-color: #007BFF;
            color: white;
            border: none;
            padding: 0.7rem 1.5rem;
            border-radius: 5px;
            font-size: 1rem;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        .actions button:hover {
            background-color: #0056b3;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 1rem;
        }

        table, th, td {
            border: 1px solid #ddd;
        }

        th, td {
            padding: 0.8rem;
            text-align: left;
        }

        th {
            background-color: #007BFF;
            color: white;
        }
    </style>

    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <script>
        // Reset form inputs when modal is opened
        $(document).on('show.bs.modal', function (e) {
            $(e.target).find('input').val('');
        });
        $(document).on('hidden.bs.modal', function (e) {
            $('#deleteMessage').text(''); // Clear error message
            $('#addMessage').text('');
            $('#updateMessage').text('');
        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navbar -->
        <div class="navbar">
            <div class="title">Admin Dashboard</div>
            <asp:Button ID="btnChangePassword" Text="Change Password" runat="server" OnClick="ChangePasswordEventMethod" />
            <asp:Button ID="logOutButton" Text="Log out" runat="server" OnClick="LogOutEventMethod" />
        </div>

        <!-- Main Content -->
        <div class="container">
            <div>
                <label for="userlabel">Hello </label>
                <asp:label ID="userlabel" Text="no user" runat="server" />
            </div>

            <div class="actions">
                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addCustomerModal">Add Customer</button>
                <button type="button" class="btn btn-success" data-toggle="modal" data-target="#updateCustomerModal">Update Customer</button>
                <button type="button" class="btn btn-danger" data-toggle="modal" data-target="#deleteCustomerModal">Delete Customer</button>
            </div>

            <!--<table>
                <thead>
                    <tr>
                        <th>Customer ID</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Phone</th>
                        <th>Address</th>
                        <th>Package Type</th>
                        <th>Package Price</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>-->


    <!-- 
    Security-enhanced GridView that implements multiple layers of XSS protection:
    - Explicit column definitions prevent unauthorized data exposure
    - HtmlEncode attributes ensure proper content encoding
    - RowDataBound event provides additional security checks
     -->
<asp:GridView ID="CustomerGridView" runat="server" 
    AutoGenerateColumns="False"                     
    OnRowDataBound="CustomerGridView_RowDataBound"  
    CssClass="table table-striped"
    HtmlEncode="True"                              
    EnableViewState="True">                         
    <Columns>
        <asp:BoundField DataField="customer_ID" HeaderText="Customer ID" HtmlEncode="True" />
        <asp:BoundField DataField="name" HeaderText="Name" HtmlEncode="True" />
        <asp:BoundField DataField="email" HeaderText="Email" HtmlEncode="True" />
        <asp:BoundField DataField="phone" HeaderText="Phone" HtmlEncode="True" />
        <asp:BoundField DataField="address" HeaderText="Address" HtmlEncode="True" />
        <asp:BoundField DataField="package_type" HeaderText="Package Type" HtmlEncode="True" />
        <asp:BoundField DataField="package_price" HeaderText="Package Price" HtmlEncode="True" />
    </Columns>
</asp:GridView>

        <!-- Add Customer Modal -->
        <div class="modal fade" id="addCustomerModal" tabindex="-1" role="dialog" aria-labelledby="addCustomerModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="addCustomerModalLabel">Add Customer</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="txtAddCustomerID" runat="server" Placeholder="Customer ID" CssClass="form-control"></asp:TextBox><br />
                        <asp:TextBox ID="txtAddCustomerName" runat="server" Placeholder="Name" CssClass="form-control"></asp:TextBox><br />
                        <asp:TextBox ID="txtAddCustomerEmail" runat="server" Placeholder="Email" CssClass="form-control"></asp:TextBox><br />
                        <asp:TextBox ID="txtAddCustomerPhone" runat="server" Placeholder="Phone" CssClass="form-control"></asp:TextBox><br />
                        <asp:TextBox ID="txtAddCustomerAddress" runat="server" Placeholder="Address" CssClass="form-control"></asp:TextBox><br />
                        <asp:TextBox ID="txtAddPackageType" runat="server" Placeholder="Package Type" CssClass="form-control"></asp:TextBox><br />
                        <asp:TextBox ID="txtAddPackagePrice" runat="server" Placeholder="Package Price" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSaveCustomer" runat="server" Text="Add" CssClass="btn btn-success" OnClick="SaveCustomerEventMethod" CausesValidation="true"/>
                        <!--<button ID="btnSaveCustomer" type="button" class="btn btn-secondary" onclick="SaveCustomerEventMethod">Add</button>-->
                       <!-- <button type="button" cssclass="btn btn-danger" data-dismiss="modal"/>-->
                        <asp:Button cssclass="btn btn-danger"  runat="server" data-dismiss="modal"/>
                    </div>
                    <div>
                          <asp:Label ID="addMessage" Text="" runat="server" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Update Customer Modal -->
        <div class="modal fade" id="updateCustomerModal" tabindex="-1" role="dialog" aria-labelledby="updateCustomerModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="updateCustomerModalLabel">Update Customer</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="txtUpdateCustomerID" runat="server" Placeholder="Customer ID" CssClass="form-control"></asp:TextBox><br />
                        <asp:TextBox ID="txtUpdateCustomerName" runat="server" Placeholder="Name" CssClass="form-control"></asp:TextBox><br />
                        <asp:TextBox ID="txtUpdateCustomerEmail" runat="server" Placeholder="Email" CssClass="form-control"></asp:TextBox><br />
                        <asp:TextBox ID="txtUpdateCustomerPhone" runat="server" Placeholder="Phone" CssClass="form-control"></asp:TextBox><br />
                        <asp:TextBox ID="txtUpdateCustomerAddress" runat="server" Placeholder="Address" CssClass="form-control"></asp:TextBox><br />
                        <asp:TextBox ID="txtUpdatePackageType" runat="server" Placeholder="Package Type" CssClass="form-control"></asp:TextBox><br />
                        <asp:TextBox ID="txtUpdatePackagePrice" runat="server" Placeholder="Package Price" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                       <asp:Button ID="btnUpdateCustomer" runat="server" Text="Update" cssclass="btn btn-success" OnClick="UpdateCustomerEventMethod" CausesValidation="true" />
                       <!-- <button ID="btnUpdateCustomer" type="button" class="btn btn-secondary" onclick="UpdateCustomerEventMethod">Update</button>-->
                        <!--<button type="button" cssclass="btn btn-danger" data-dismiss="modal"/>-->
                        <asp:Button cssclass="btn btn-danger"  runat="server" data-dismiss="modal"/>

                    </div>
                    <div>
                          <asp:Label ID="updateMessage" Text="" runat="server" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Delete Customer Modal -->
        <div class="modal fade" id="deleteCustomerModal" tabindex="-1" role="dialog" aria-labelledby="deleteCustomerModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="deleteCustomerModalLabel">Delete Customer</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="txtDeleteCustomerID" runat="server" Placeholder="Customer ID" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnDeleteCustomer" runat="server" Text="Delete" cssclass="btn btn-success" OnClick="DeleteCustomerEventMethod" CausesValidation="true"/>
                        <!--<button ID="btnDeleteCustomer" type="button" class="btn btn-secondary" onclick="DeleteCustomerEventMethod">Delete</button>-->
                        <!--<button type="button" cssclass="btn btn-danger" data-dismiss="modal"/>-->
                         <asp:Button cssclass="btn btn-danger"  runat="server" data-dismiss="modal"/>
                    </div>
                    <div>
                          <asp:Label ID="deleteMessage" Text="" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
