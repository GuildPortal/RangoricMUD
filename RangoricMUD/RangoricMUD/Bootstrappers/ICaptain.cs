﻿#region License

// RangoricMUD is licensed under the Open Game License.
// The original code and assets provided in this repository are Open Game Content,
// The name RangoricMUD is product identity, and can only be used as a part of the code,
//   or in reference to this project.
// 
// More details and the full text of the license are available at:
//   https://github.com/Rangoric/RangoricMUD/wiki/Open-Game-License
// 
// RangoricMUD's home is at: https://github.com/Rangoric/RangoricMUD

#endregion

#region References

using System;
using System.Collections.Generic;

#endregion

namespace RangoricMUD.Bootstrappers
{
    public interface ICaptain : IDisposable
    {
        void InspectCrew(IEnumerable<ICrew> tCrew);
        object Resolve(Type tServiceType);
        IEnumerable<object> ResolveAll(Type tServiceType);
        void Release(object tObject);
        void Register(Type tServiceType, Func<object> tActivator);
    }
}