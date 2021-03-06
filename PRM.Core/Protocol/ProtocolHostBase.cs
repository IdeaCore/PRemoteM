﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PRM.Core.Protocol
{
    public abstract class ProtocolHostBase : UserControl
    {
        public readonly ProtocolServerBase ProtocolServer;
        public Window ParentWindow { get; set; } = null;

        protected ProtocolHostBase(ProtocolServerBase protocolServer, bool canFullScreen = false)
        {
            ProtocolServer = protocolServer;
            CanFullScreen = canFullScreen;
        }


        public string ConnectionId
        {
            get
            {
                if (ProtocolServer.OnlyOneInstance)
                    return ProtocolServer.Id.ToString();
                else
                    return ProtocolServer.Id.ToString() + "_" + this.GetHashCode().ToString();
            }
        }

        public bool CanFullScreen { get; protected set; }

        /// <summary>
        /// since resizing when rdp is connecting would not tiger the rdp size change event
        /// then I let rdp host return false when rdp is on connecting to prevent TabWindow resize or maximize.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanResizeNow()
        {
            return true;
        }
        public Action OnCanResizeNowChanged { get; set; } = null;


        public abstract void Conn();
        public abstract void DisConn();
        public abstract void GoFullScreen();
        public abstract bool IsConnected();
        public abstract bool IsConnecting();


        
        protected static readonly object MakeItFocusLocker1 = new object();
        protected static readonly object MakeItFocusLocker2 = new object();

        /// <summary>
        /// call to focus the AxRdp or putty
        /// </summary>
        public virtual void MakeItFocus()
        {
            // do nothing
        }



        public Action<string> OnClosed = null;
        public Action<string> OnFullScreen2Window = null;
    }
}
