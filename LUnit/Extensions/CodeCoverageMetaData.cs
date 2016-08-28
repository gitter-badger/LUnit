using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using LCore.Extensions;

namespace LCore.LUnit
    {
    /// <summary>
    /// Extends <see cref="CodeMetaData"/> to add code coverage information as well.
    /// </summary>
    public class CodeCoverageMetaData : CodeMetaData
        {
        // TODO Better support for type coverage
        /// <summary>
        /// Method coverage for the given member.
        /// </summary>
        public MethodCoverage Coverage { get; set; }

        /// <summary>
        /// Create a new CodeCoverageMetaData from a <see cref="MemberInfo"/>.
        /// 
        /// Optionally include any custom comment tags you'd like to track.
        /// </summary>
        public CodeCoverageMetaData(MemberInfo Member, string[] CustomCommentTags = null) : base(Member, CustomCommentTags)
            {
            if (Member is MethodInfo)
                this.Coverage = new MethodCoverage((MethodInfo)Member);
            }
        }
    }