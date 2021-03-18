<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calculadora.aspx.cs" Inherits="CalculadoraPrestamo.Calculadora" ValidateRequest="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Calculadora Préstamos</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.0.0.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="padding-top: 5%;">
            <div class="row">
                <div class="col-md"></div>
                <div class="col-md-9">
                    <div class="card">
                        <div class="card-header bg-primary">
                            <div class="card-title font-weight-bold text-center">
                                Calculadora de Préstamos Método Francés
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="form-group row">
                                <label for="inputEmail3" class="col-sm-2 col-form-label">Monto Crédito</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="txtMontoCredito" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                        ControlToValidate="txtMontoCredito" runat="server"
                                        ErrorMessage="Solo se permite números"
                                        ValidationExpression="^\d+(\.\d+)?$">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="inputEmail3" class="col-sm-2 col-form-label">Plazo Meses:</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="txtPlazo" CssClass="form-control" TextMode="Number">
                                        
                                    </asp:TextBox>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                        ControlToValidate="txtPlazo" runat="server"
                                        ErrorMessage="Solo se permite números enteros."
                                        ValidationExpression="^\d+">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="inputEmail3" class="col-sm-2 col-form-label">Tasa de Interés</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="txtInterés" CssClass="form-control"></asp:TextBox>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                        ControlToValidate="txtInterés" runat="server"
                                        ErrorMessage="Solo se permite números"
                                        ValidationExpression="^\d+(\,\d+)?$">
                                    </asp:RegularExpressionValidator>
                                    
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-12 text-center">
                                    <asp:Button runat="server" ID="btnCalcular" OnClick="btnCalcular_Click" Text="Generar" CssClass="btn btn-success" Width="20%" />
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <div class="row text-center"><div class="col-md-12">Plan de Pago</div></div>
                            <div class="form-group row text-center font-weight-bold">
                                <div class="col-md-2">Principal</div>
                                <div class="col-md-2"><asp:Label runat="server" ID="lblPrincipal" CssClass="form-control"></asp:Label></div>
                                <div class="col-md-2">Intereses</div>
                                <div class="col-md-2"><asp:Label runat="server" ID="lblIntereses" CssClass="form-control"></asp:Label></div>
                                <div class="col-md-2">Total a Pagar</div>
                                <div class="col-md-2"><asp:Label runat="server" ID="lblTotal" CssClass="form-control"></asp:Label></div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView runat="server" AllowPaging="true" ID="grvPlan" OnPageIndexChanged="grvPlan_PageIndexChanged" OnPageIndexChanging="grvPlan_PageIndexChanging" PageSize="10" CssClass="table table-hover table-bordered" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="NumeroCuota" HeaderText="No." />
                                            <asp:BoundField DataField="Principal" HeaderText="Principal" DataFormatString="{0:N2}"/>
                                            <asp:BoundField DataField="Interes" DataFormatString="{0:N2}" HeaderText="Interes" />
                                            <asp:BoundField DataField="Formalizacion" DataFormatString="{0:N2}" HeaderText="Formalizacion" />
                                            <asp:BoundField DataField="TotalCuota" HeaderText="Cuota Total" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:N2}" />
                                        </Columns>
                                        <HeaderStyle CssClass="bg-info font-weight-bold" />
                                        <PagerSettings Visible="true" Position="TopAndBottom" Mode="Numeric" />
                                        
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="col-md"></div>
            </div>
        </div>
    </form>
</body>
</html>
