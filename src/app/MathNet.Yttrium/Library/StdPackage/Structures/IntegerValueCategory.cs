#region Copyright 2001-2006 Christoph Daniel R�egg [GPL]
//Math.NET Symbolics: Yttrium, part of Math.NET
//Copyright (c) 2001-2006, Christoph Daniel Rueegg, http://cdrnet.net/.
//All rights reserved.
//This Math.NET package is available under the terms of the GPL.

//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using MathNet.Symbolics.Core;
using MathNet.Symbolics.Backend;
using MathNet.Symbolics.Backend.Properties;

namespace MathNet.Symbolics.StdPackage.Structures
{
    /// <summary>
    /// The category Z: ordered set of all signed integers
    /// </summary>
    public class IntegerValueCategory : Category
    {
        protected IntegerValueCategory()
            : base("IntegerValueExpression", "Std") { }

        public override ECategoryMembership IsMember(Signal signal, bool ignoreCache)
        {
            if(signal == null)
                throw new ArgumentNullException("signal");

            if(signal.Value == null)
                return ECategoryMembership.Unknown;
            if(signal.Value is IntegerValue)
                return ECategoryMembership.Member;
            return ECategoryMembership.NotMember;
        }

        public override ECategoryMembership IsMember(Port port)
        {
            return EvaluateJointChildMembership(port.InputSignals);
        }

        #region Singleton
        private static IntegerValueCategory _instance;
        public static IntegerValueCategory Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new IntegerValueCategory();
                return _instance;
            }
        }
        #endregion

        public static bool IsIntegerValueMember(Port port)
        {
            return (Instance.IsMember(port) == ECategoryMembership.Member);
        }
        public static bool IsNotIntegerValueMember(Port port)
        {
            return (Instance.IsMember(port) == ECategoryMembership.NotMember);
        }

        #region Serialization
        protected override void InnerSerialize(XmlWriter writer)
        {
        }
        public static IntegerValueCategory InnerDeserialize(Context context, XmlReader reader)
        {
            return Instance;
        }
        #endregion
    }
}
