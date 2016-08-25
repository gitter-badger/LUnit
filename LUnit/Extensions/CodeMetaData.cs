using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using LCore.Extensions;
using LCore.Interfaces;

namespace LCore.LUnit
    {

    public class CodeCoverageMetaData : CodeMetaData
        {
        public MethodCoverage Coverage { get; set; }

        public CodeCoverageMetaData(MemberInfo Member, string[] CustomCommentTags) : base(Member, CustomCommentTags)
            {
            if (Member is MethodInfo)
                this.Coverage = new MethodCoverage((MethodInfo)Member);
            }
        }
    }