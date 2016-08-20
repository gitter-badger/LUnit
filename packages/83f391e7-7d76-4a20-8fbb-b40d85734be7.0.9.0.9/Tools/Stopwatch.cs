﻿using System;
using LCore.Extensions;

namespace LCore.Tools
    {
    /// <summary>
    /// Simple class to time actions.
    /// </summary>
    public class StopWatch
        {
        private DateTime _StartTime;

        /// <summary>
        /// Create a new StopWatch.
        /// </summary>
        public StopWatch()
            {
            this.Start();
            }

        /// <summary>
        /// Start the StopWatch timer.
        /// </summary>
        public void Start()
            {
            this._StartTime = DateTime.Now;
            }

        /// <summary>
        /// Stops the timer and returns the duration in milliseconds.
        /// </summary>
        /// <returns>The duration in milliseconds</returns>
        public double Stop()
            {
            return (DateTime.Now.Ticks - this._StartTime.Ticks) * L.Date.TicksToMilliseconds;
            }
        }
    }