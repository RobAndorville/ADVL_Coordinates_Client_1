﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System.Runtime.Serialization

Namespace ServiceReference1
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute(Name:="clsConnection.AppTypes", [Namespace]:="http://schemas.datacontract.org/2004/07/ADVL_ApplicationNetwork")>  _
    Public Enum clsConnectionAppTypes As Integer
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        Application = 0
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        MainNode = 1
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        Node = 2
    End Enum
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="ServiceReference1.IMsgService", CallbackContract:=GetType(ServiceReference1.IMsgServiceCallback))>  _
    Public Interface IMsgService
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IMsgService/Connect", ReplyAction:="http://tempuri.org/IMsgService/ConnectResponse")>  _
        Function Connect(ByVal appName As String, ByVal connectionName As String, ByVal projectName As String, ByVal projectPath As String, ByVal appType As ServiceReference1.clsConnectionAppTypes, ByVal getAllWarnings As Boolean, ByVal getAllMessages As Boolean) As String
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IMsgService/Connect", ReplyAction:="http://tempuri.org/IMsgService/ConnectResponse")>  _
        Function ConnectAsync(ByVal appName As String, ByVal connectionName As String, ByVal projectName As String, ByVal projectPath As String, ByVal appType As ServiceReference1.clsConnectionAppTypes, ByVal getAllWarnings As Boolean, ByVal getAllMessages As Boolean) As System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IMsgService/SendMessage", ReplyAction:="http://tempuri.org/IMsgService/SendMessageResponse")>  _
        Sub SendMessage(ByVal connName As String, ByVal message As String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IMsgService/SendMessage", ReplyAction:="http://tempuri.org/IMsgService/SendMessageResponse")>  _
        Function SendMessageAsync(ByVal connName As String, ByVal message As String) As System.Threading.Tasks.Task
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IMsgService/SendAllMessage", ReplyAction:="http://tempuri.org/IMsgService/SendAllMessageResponse")>  _
        Sub SendAllMessage(ByVal message As String, ByVal SenderName As String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IMsgService/SendAllMessage", ReplyAction:="http://tempuri.org/IMsgService/SendAllMessageResponse")>  _
        Function SendAllMessageAsync(ByVal message As String, ByVal SenderName As String) As System.Threading.Tasks.Task
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IMsgService/SendMainNodeMessage", ReplyAction:="http://tempuri.org/IMsgService/SendMainNodeMessageResponse")>  _
        Sub SendMainNodeMessage(ByVal message As String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IMsgService/SendMainNodeMessage", ReplyAction:="http://tempuri.org/IMsgService/SendMainNodeMessageResponse")>  _
        Function SendMainNodeMessageAsync(ByVal message As String) As System.Threading.Tasks.Task
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IMsgService/GetConnectionList", ReplyAction:="http://tempuri.org/IMsgService/GetConnectionListResponse")>  _
        Sub GetConnectionList()
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IMsgService/GetConnectionList", ReplyAction:="http://tempuri.org/IMsgService/GetConnectionListResponse")>  _
        Function GetConnectionListAsync() As System.Threading.Tasks.Task
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IMsgService/Disconnect", ReplyAction:="http://tempuri.org/IMsgService/DisconnectResponse")>  _
        Function Disconnect(ByVal connName As String) As Boolean
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IMsgService/Disconnect", ReplyAction:="http://tempuri.org/IMsgService/DisconnectResponse")>  _
        Function DisconnectAsync(ByVal connName As String) As System.Threading.Tasks.Task(Of Boolean)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IMsgService/IsAlive", ReplyAction:="http://tempuri.org/IMsgService/IsAliveResponse")>  _
        Function IsAlive() As Boolean
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IMsgService/IsAlive", ReplyAction:="http://tempuri.org/IMsgService/IsAliveResponse")>  _
        Function IsAliveAsync() As System.Threading.Tasks.Task(Of Boolean)
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface IMsgServiceCallback
        
        <System.ServiceModel.OperationContractAttribute(IsOneWay:=true, Action:="http://tempuri.org/IMsgService/OnSendMessage")>  _
        Sub OnSendMessage(ByVal message As String)
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface IMsgServiceChannel
        Inherits ServiceReference1.IMsgService, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class MsgServiceClient
        Inherits System.ServiceModel.DuplexClientBase(Of ServiceReference1.IMsgService)
        Implements ServiceReference1.IMsgService
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext)
            MyBase.New(callbackInstance)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String)
            MyBase.New(callbackInstance, endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(callbackInstance, endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, binding, remoteAddress)
        End Sub
        
        Public Function Connect(ByVal appName As String, ByVal connectionName As String, ByVal projectName As String, ByVal projectPath As String, ByVal appType As ServiceReference1.clsConnectionAppTypes, ByVal getAllWarnings As Boolean, ByVal getAllMessages As Boolean) As String Implements ServiceReference1.IMsgService.Connect
            Return MyBase.Channel.Connect(appName, connectionName, projectName, projectPath, appType, getAllWarnings, getAllMessages)
        End Function
        
        Public Function ConnectAsync(ByVal appName As String, ByVal connectionName As String, ByVal projectName As String, ByVal projectPath As String, ByVal appType As ServiceReference1.clsConnectionAppTypes, ByVal getAllWarnings As Boolean, ByVal getAllMessages As Boolean) As System.Threading.Tasks.Task(Of String) Implements ServiceReference1.IMsgService.ConnectAsync
            Return MyBase.Channel.ConnectAsync(appName, connectionName, projectName, projectPath, appType, getAllWarnings, getAllMessages)
        End Function
        
        Public Sub SendMessage(ByVal connName As String, ByVal message As String) Implements ServiceReference1.IMsgService.SendMessage
            MyBase.Channel.SendMessage(connName, message)
        End Sub
        
        Public Function SendMessageAsync(ByVal connName As String, ByVal message As String) As System.Threading.Tasks.Task Implements ServiceReference1.IMsgService.SendMessageAsync
            Return MyBase.Channel.SendMessageAsync(connName, message)
        End Function
        
        Public Sub SendAllMessage(ByVal message As String, ByVal SenderName As String) Implements ServiceReference1.IMsgService.SendAllMessage
            MyBase.Channel.SendAllMessage(message, SenderName)
        End Sub
        
        Public Function SendAllMessageAsync(ByVal message As String, ByVal SenderName As String) As System.Threading.Tasks.Task Implements ServiceReference1.IMsgService.SendAllMessageAsync
            Return MyBase.Channel.SendAllMessageAsync(message, SenderName)
        End Function
        
        Public Sub SendMainNodeMessage(ByVal message As String) Implements ServiceReference1.IMsgService.SendMainNodeMessage
            MyBase.Channel.SendMainNodeMessage(message)
        End Sub
        
        Public Function SendMainNodeMessageAsync(ByVal message As String) As System.Threading.Tasks.Task Implements ServiceReference1.IMsgService.SendMainNodeMessageAsync
            Return MyBase.Channel.SendMainNodeMessageAsync(message)
        End Function
        
        Public Sub GetConnectionList() Implements ServiceReference1.IMsgService.GetConnectionList
            MyBase.Channel.GetConnectionList
        End Sub
        
        Public Function GetConnectionListAsync() As System.Threading.Tasks.Task Implements ServiceReference1.IMsgService.GetConnectionListAsync
            Return MyBase.Channel.GetConnectionListAsync
        End Function
        
        Public Function Disconnect(ByVal connName As String) As Boolean Implements ServiceReference1.IMsgService.Disconnect
            Return MyBase.Channel.Disconnect(connName)
        End Function
        
        Public Function DisconnectAsync(ByVal connName As String) As System.Threading.Tasks.Task(Of Boolean) Implements ServiceReference1.IMsgService.DisconnectAsync
            Return MyBase.Channel.DisconnectAsync(connName)
        End Function
        
        Public Function IsAlive() As Boolean Implements ServiceReference1.IMsgService.IsAlive
            Return MyBase.Channel.IsAlive
        End Function
        
        Public Function IsAliveAsync() As System.Threading.Tasks.Task(Of Boolean) Implements ServiceReference1.IMsgService.IsAliveAsync
            Return MyBase.Channel.IsAliveAsync
        End Function
    End Class
End Namespace
