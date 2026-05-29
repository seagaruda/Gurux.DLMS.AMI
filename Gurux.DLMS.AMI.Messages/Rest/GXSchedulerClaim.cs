//
// --------------------------------------------------------------------------
//  Gurux Ltd
//
// Copyright (c) Gurux Ltd
//
//---------------------------------------------------------------------------
//
// This file is a part of Gurux Device Framework.
//
// Gurux Device Framework is Open Source software; you can redistribute it
// and/or modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; version 2 of the License.
//
// This code is licensed under the GNU General Public License v2.
// Full text may be retrieved at http://www.gnu.org/licenses/gpl-2.0.txt
//---------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace Gurux.DLMS.AMI.Messages.Rest
{
    /// <summary>
    /// Request to atomically claim a scheduler tick for a given schedule.
    /// Only the first API instance to claim a (ScheduleId, TickMinute) pair
    /// will proceed to create tasks; all others receive Claimed=false.
    /// </summary>
    [DataContract]
    public class ClaimScheduleTickRequest
    {
        /// <summary>
        /// The schedule being evaluated.
        /// </summary>
        [DataMember]
        public UInt64 ScheduleId { get; set; }

        /// <summary>
        /// The minute-truncated timestamp of the current scheduler tick.
        /// </summary>
        [DataMember]
        public DateTime TickMinute { get; set; }

        /// <summary>
        /// Unique ID of the scheduler instance making the claim.
        /// </summary>
        [DataMember]
        public string InstanceId { get; set; }
    }

    /// <summary>
    /// Response to a ClaimScheduleTick request.
    /// </summary>
    [DataContract]
    public class ClaimScheduleTickResponse
    {
        /// <summary>
        /// True if this instance successfully claimed the tick and should
        /// proceed to create tasks. False if another instance already claimed it.
        /// </summary>
        [DataMember]
        public bool Claimed { get; set; }
    }
}
