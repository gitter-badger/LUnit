﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using LCore.Tools;
using LCore.Extensions;

namespace LCore.Dynamic
    {
    [ExcludeFromCodeCoverage]
    internal class CodeExplodeExtend : CodeExplode
        {
        public override string ExplodeCode(Lists<string, MemberInfo> Members)
            {
            return L.Exploder.LogicMemberInfoToExtensionStrings(Members, "", arg3: null);
            }

        public override bool ExplodeMember(MemberInfo Member)
            {
            return Member.HasAttribute(typeof(CodeExplodeExtensionMethod), IncludeBaseClasses: true);
            }

        public CodeExplodeExtend(Type T)
            : base(T)
            {
            this.CodeRegionTitle = T.FullName;
            }

        public CodeExplodeExtend(string CodeRegionTitle, string CodeFileName, string CodeNamespace) :
            base(CodeRegionTitle, CodeFileName, CodeNamespace) {}
        }
    }