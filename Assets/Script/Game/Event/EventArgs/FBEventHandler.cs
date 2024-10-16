using System;

public delegate bool FBEventHandler<TEventArgs>(string eventDefine, object sender, TEventArgs msg)
    where TEventArgs : EventArgs;