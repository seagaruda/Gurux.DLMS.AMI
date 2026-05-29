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

using Gurux.Service.Orm;
using System;
using System.ComponentModel;

namespace Gurux.DLMS.AMI.Messages.DB
{
    /// <summary>
    /// Records which scheduler instance claimed a given (schedule, tick-minute) pair.
    /// The table acts as a distributed lock: the first INSERT wins (unique constraint
    /// on ScheduleId + TickMinute), subsequent inserts from other instances fail and
    /// those instances skip task creation for that tick.
    /// Old rows are safe to prune after a few minutes; they are never read after the tick.
    /// </summary>
    [Description("Scheduler tick claim record.")]
    public class GXSchedulerTick : IUnique<UInt64>
    {
        /// <summary>
        /// Auto-increment primary key.
        /// </summary>
        [AutoIncrement]
        [Description("Primary key.")]
        public UInt64 Id { get; set; }

        /// <summary>
        /// The schedule this claim belongs to.
        /// </summary>
        [ForeignKey(typeof(GXSchedule), OnDelete = ForeignKeyDelete.Cascade)]
        [Description("Schedule ID.")]
        public UInt64 ScheduleId { get; set; }

        /// <summary>
        /// The minute-truncated timestamp of the scheduler tick being claimed.
        /// Combined with ScheduleId this forms the unique lock key.
        /// </summary>
        [Description("Tick minute (truncated to the minute).")]
        public DateTime TickMinute { get; set; }

        /// <summary>
        /// The scheduler instance ID (GUID string) that won the claim.
        /// </summary>
        [Description("Winning scheduler instance ID.")]
        public string InstanceId { get; set; }

        /// <summary>
        /// When the claim was created. Used for pruning old rows.
        /// </summary>
        [Description("Claim creation time.")]
        public DateTime ClaimedAt { get; set; }
    }
}
