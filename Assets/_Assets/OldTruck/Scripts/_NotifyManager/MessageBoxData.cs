using System;

public class MessageBoxData
{
    public string Title = string.Empty;
    public string Message = string.Empty;
    public bool UseCancelButton = false;

    public Action OnClickOk;
    public Action OnClickCancel;

    public string YesText = String.Empty;
    public string CancelText = String.Empty;
    //public UniTaskCompletionSource<bool> Tcs;
}