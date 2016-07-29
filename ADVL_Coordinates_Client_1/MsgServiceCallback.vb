Imports System.ServiceModel
'<ServiceBehavior(ConcurrencyMode:=ConcurrencyMode.Reentrant, UseSynchronizationContext:=False)> _
'<CallbackBehavior(ConcurrencyMode:=ConcurrencyMode.Reentrant, UseSynchronizationContext:=False)> _
'<CallbackBehavior(ConcurrencyMode:=ConcurrencyMode.Multiple, UseSynchronizationContext:=False)> _
Public Class MsgServiceCallback
    Implements ServiceReference1.IMsgServiceCallback



    Public Sub OnSendMessage(message As String) Implements ServiceReference1.IMsgServiceCallback.OnSendMessage
        'A message has been received.
        'Main.rtbMessagesReceived_Old.AppendText(message & vbCrLf)

        'If the XmsgInstructions window is open, display the XMessage.
        'If IsNothing(Main.XmsgInstructions) Then
        'Else
        '    Main.XmsgInstructions.rtbInstructions.AppendText(message & vbCrLf)
        'End If

        'Set the InstrReceived property value to the XMessage. This will also apply the instructions in the XMessage.
        Main.InstrReceived = message
    End Sub

End Class
